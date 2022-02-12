using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Common;
using VehicleTracker.Data;
using VehicleTracker.Model;
using VehicleTracker.Model.Enums;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.ProductReturn;

namespace VehicleTracker.Business
{
    public class ProductReturnService : IProductReturnService
    {
        #region Member variable
        private readonly VMDBContext _db;
        private readonly IUserService _userService;
        private readonly IAppSettingService _appSettingService;
        private readonly ILogger<IProductReturnService> _logger;
        private readonly IDropDownService _dropDownService;

        #endregion

        public ProductReturnService(VMDBContext db, IUserService userService,IDropDownService dropDownService, IAppSettingService appSettingService, 
            ILogger<IProductReturnService> logger)
        {
            this._db = db;
            this._userService = userService;
            this._dropDownService = dropDownService;
            this._appSettingService = appSettingService;
            this._logger = logger;
        }


        public async Task<ResponseViewModel> DeleteProductReturn(int id, User loggedInUser)
        {
            var response = new ResponseViewModel();

            try
            {
                var model = _db.ProductReturns.FirstOrDefault(x => x.Id == id);

                if(model.Status == (int)ReturnProductStatus.ReturnToInventory)
                {
                    foreach (var item in model.ProductInventories)
                    {
                        _db.ProductInventories.Remove(item);
                    }
                }

                _db.ProductReturns.Remove(model);

                await _db.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Return product has been deleted.";
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured whil deleting the return product.";
            }

            return response;
        }

        public PaginatedItemsViewModel<BasicProductReturnViewModel> GetAllVehicleReturnRecord(ProductReturnFilterViewModel filters, 
            User loggedInUser)
        {
            int totalRecordCount = 0;
            int totalPageCount = 0;

            var query = _db.ProductReturns.OrderByDescending(x=>x.UpdatedOn);

            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(loggedInUser.TimeZone.TimeZoneId);

            if (filters.SelectedClientId>0)
            {
                query = query.Where(x => x.ClientId == filters.SelectedClientId).OrderByDescending(x => x.UpdatedOn);
            }

            if(filters.SelectedProductId>0)
            {
                query = query.Where(x => x.ProductId == filters.SelectedProductId).OrderByDescending(x => x.UpdatedOn);
            }

            if(filters.SelectedProductReturnStatus>0)
            {
                query = query.Where(x => x.Status == filters.SelectedProductReturnStatus).OrderByDescending(x => x.UpdatedOn);
            }

            var itemList = query.ToList();

            var data = new List<BasicProductReturnViewModel>();

            totalRecordCount = query.Count();

            totalPageCount = (int)Math.Ceiling((Convert.ToDecimal(totalRecordCount) / filters.PageSize));

            var pageData = query.Skip((filters.CurrentPage - 1) * filters.PageSize).Take(filters.PageSize).ToList();

            pageData.ForEach(p =>
            {
                var vm = p.ToBasicVM();
                data.Add(vm);
            });

            var response = new PaginatedItemsViewModel<BasicProductReturnViewModel>(filters.CurrentPage, 
                filters.PageSize, totalPageCount, totalRecordCount, data);

            return response;
        }

        public ProductReturnViewModel GetProductReturn(int id)
        {
            var productReturn = _db.ProductReturns.FirstOrDefault(x => x.Id == id);

            return productReturn.ToVM();
        }

        public ProductReturnMasterDataViewModel GetProductReturnMasterData()
        {
            var masterData = new ProductReturnMasterDataViewModel();

            masterData.ProductCategories.AddRange(_dropDownService.GetProductCategories());

            foreach (ReturnProductStatus value in Enum.GetValues(typeof(ReturnProductStatus)))
            {
                masterData.ProductReturnStatus.Add(new DropDownViewModal() 
                { Id = (int)value, Name = EnumHelper.GetEnumDescription(value) });
            }

            foreach (ReturnReason value in Enum.GetValues(typeof(ReturnReason)))
            {
                masterData.ProductReturnReasonCodes.Add(new DropDownViewModal() 
                { Id = (int)value, Name = EnumHelper.GetEnumDescription(value) });
            }

            return masterData;
        }

        public List<DropDownViewModal> GetSalesOrderListForSelectedClient(int clientId)
        {
            var salesOrders = _db.SalesOrders.Where(s => s.Status == (int)Model.Enums.SalesOrderStatus.Completed)
                .OrderByDescending(x=>x.OrderDate)
                .Select(x => new DropDownViewModal() { Id = x.Id, Name = x.OrderNumber }).ToList();

            return salesOrders;
        }

        public async Task<ResponseViewModel> SaveProductReturn(ProductReturnViewModel vm, User loggedInUser)
        {
            var response = new ResponseViewModel();

            try
            {
                var productReturn = _db.ProductReturns.FirstOrDefault(x => x.Id == vm.Id);

                if(productReturn==null)
                {
                    productReturn = vm.ToModel();
                    productReturn.CreatedById = loggedInUser.Id;
                    productReturn.UpdatedById = loggedInUser.Id;

                    productReturn.ProductInventories = new HashSet<ProductInventory>();

                    var inventory = new ProductInventory()
                    {
                        ProductId = vm.SelectedProductId,
                        WarehouseId = vm.SelectedWarehouseId,
                        ReceivedQty = vm.Qty,
                        CreatedById = loggedInUser.Id,
                        CreatedOn = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow,
                        UdatedById = loggedInUser.Id,
                        Action = 0,
                        IsActive = true
                    };

                    if (!string.IsNullOrEmpty(vm.BatchNo))
                    {
                        inventory.BatchNo = vm.BatchNo;
                    }

                    if(vm.DateOfExpiration!=null)
                    {
                        inventory.DateOfExpiration = vm.DateOfExpiration;
                    }

                    if(vm.DateOfManufacture!=null)
                    {
                        inventory.DateOfManufacture = vm.DateOfManufacture;
                    }

                    productReturn.ProductInventories.Add(inventory);

                    _db.ProductReturns.Add(productReturn);

                    response.IsSuccess = true;
                    response.Message = "New product return record has been added.";
                }
                else
                {
                    productReturn.ProductId = vm.SelectedProductId;
                    productReturn.ClientId = vm.SelectedClientId;
                    if(vm.SelectedSaleOrderId>0)
                    {
                        productReturn.SaleOrderId = vm.SelectedSaleOrderId;
                    }
                    else
                    {
                        productReturn.SaleOrderId = (long?)null;
                    }


                    productReturn.Qty = vm.Qty;
                    productReturn.ReturnDate = vm.ReturnDate;
                    productReturn.Status = (int)vm.Status;
                    productReturn.ReasonCode = (int)vm.ReasonCode;
                    productReturn.Reason = vm.Reason;
                    productReturn.UpdatedById = loggedInUser.Id;
                    productReturn.UpdatedOn = DateTime.UtcNow;

                    var inventory = productReturn.ProductInventories.FirstOrDefault();

                    if(inventory != null)
                    {
                        inventory.ProductId = vm.SelectedProductId;
                        inventory.ReceivedQty = vm.Qty;
                        inventory.WarehouseId = vm.SelectedWarehouseId;
                        inventory.UpdatedOn = DateTime.UtcNow;
                        inventory.UdatedById = loggedInUser.Id;

                        if (!string.IsNullOrEmpty(vm.BatchNo))
                        {
                            inventory.BatchNo = vm.BatchNo;
                        }

                        if (vm.DateOfExpiration != null)
                        {
                            inventory.DateOfExpiration = vm.DateOfExpiration;
                        }

                        if (vm.DateOfManufacture != null)
                        {
                            inventory.DateOfManufacture = vm.DateOfManufacture;
                        }
                    }

                    _db.ProductReturns.Update(productReturn);

                    response.IsSuccess = true;
                    response.Message = "Product return record has been updated.";
                };

                await _db.SaveChangesAsync();

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured while saving the product return data.";
            }

            return response;
        }
    }
}
