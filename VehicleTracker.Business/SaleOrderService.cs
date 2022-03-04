using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Data;
using VehicleTracker.Model;
using VehicleTracker.Model.Enums;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.SalesOrder;

namespace VehicleTracker.Business
{
    public class SaleOrderService : ISaleOrderService
    {
        #region Member variable
        private readonly VMDBContext _db;
        private readonly IUserService _userService;
        private readonly IAppSettingService _appSettingService;
        private readonly ILogger<ISaleOrderService> _logger;

        #endregion

        public SaleOrderService(VMDBContext db, IUserService userService, IAppSettingService appSettingService, ILogger<ISaleOrderService> logger)
        {
            this._db = db;
            this._userService = userService;
            this._appSettingService = appSettingService;
            this._logger = logger;
        }

        public async Task<long> CreateNewSalesOrder(User loggedInUser)
        {
            try
            {
                var salesOrder = new SalesOrder()
                {
                    CreatedById = loggedInUser.Id,
                    CreatedOn = DateTime.UtcNow,
                    OrderDate=DateTime.UtcNow,
                    Discount = 0,
                    IsActive = true,
                    OrderNumber = await GenerateSalesOrderNumber(),
                    Status = (int)VehicleTracker.Model.Enums.SalesOrderStatus.New,
                    SubTotal = 0,
                    TaxRate = Convert.ToDecimal(_appSettingService.GetAppSettingValue(Model.Enums.AppSettingInfor.TaxRate)),
                    TotalTaxAmount = 0,
                    ShippingCharge = 0,
                    TotalAmount = 0,
                    UpdatedById = loggedInUser.Id,
                    UpdatedOn = DateTime.UtcNow
                };

                _db.SalesOrders.Add(salesOrder);
                await _db.SaveChangesAsync();

                return salesOrder.Id;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());

                return 0;
            }

        }

        public async Task<ResponseViewModel> DeleteSalesOrder(int id, User loggedInUser)
        {
            var response = new ResponseViewModel();

            try
            {
                var salesOrder = _db.SalesOrders.FirstOrDefault(x => x.Id == id);

                salesOrder.Status = (int)VehicleTracker.Model.Enums.SalesOrderStatus.Cancelled;
                salesOrder.IsActive = false;
                salesOrder.UpdatedById = loggedInUser.Id;
                salesOrder.UpdatedOn = DateTime.UtcNow;

                _db.SalesOrders.Update(salesOrder);

                await _db.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Selected sales order has been deleted successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured while deleting the sales order.";
            }

            return response;
        }

        public List<BasicSalesOrderDetailViewModel> GetAllSalesOrders(SalesOrderFilter filters)
        {
            var allSalesOrders = _db.SalesOrders.Where(x => x.IsActive == true).OrderByDescending(s => s.CreatedOn);

            if (filters.SelectedRouteId > 0)
            {
                allSalesOrders = allSalesOrders.Where(x => x.Owner.RouteId == filters.SelectedRouteId).OrderByDescending(s => s.CreatedOn);
            }

            if (filters.SelectedCustomerId > 0)
            {
                allSalesOrders = allSalesOrders.Where(x => x.OwnerId == filters.SelectedCustomerId).OrderByDescending(s => s.CreatedOn);
            }

            if (filters.SelectedSalesPersonId > 0)
            {
                allSalesOrders = allSalesOrders.Where(x => x.CreatedById == filters.SelectedSalesPersonId).OrderByDescending(s => s.CreatedOn);
            }

            if (filters.SelectedStatus > 0)
            {
                allSalesOrders = allSalesOrders.Where(x => x.Status == filters.SelectedStatus).OrderByDescending(s => s.CreatedOn);
            }

            var salesOrderList = allSalesOrders.ToList();

            var response = GenerateBasicSalesOrderList(salesOrderList);

            return response;
        }

        public List<BasicSalesOrderDetailViewModel> GetMySalesOrders(SalesOrderFilter filters, User loggedInUser)
        {


            var allSalesOrders = _db.SalesOrders.Where(x => x.IsActive == true && x.OwnerId == loggedInUser.Id).OrderByDescending(s => s.CreatedOn);

            if (filters.SelectedRouteId > 0)
            {
                allSalesOrders = allSalesOrders.Where(x => x.Owner.RouteId == filters.SelectedRouteId).OrderByDescending(s => s.CreatedOn);
            }

            if (filters.SelectedCustomerId > 0)
            {
                allSalesOrders = allSalesOrders.Where(x => x.OwnerId == filters.SelectedCustomerId).OrderByDescending(s => s.CreatedOn);
            }

            if (filters.SelectedSalesPersonId > 0)
            {
                allSalesOrders = allSalesOrders.Where(x => x.CreatedById == filters.SelectedSalesPersonId).OrderByDescending(s => s.CreatedOn);
            }

            if (filters.SelectedStatus > 0)
            {
                allSalesOrders = allSalesOrders.Where(x => x.Status == filters.SelectedStatus).OrderByDescending(s => s.CreatedOn);
            }

            var salesOrderList = allSalesOrders.ToList();

            var response = GenerateBasicSalesOrderList(salesOrderList);

            return response;
        }

        public SalesOrderMasterDataViewModel GetSalesOrderMasterData()
        {
            var response = new SalesOrderMasterDataViewModel();

            response.Statuses = _db.SalesOrderStatuses.Where(x => x.IsActive == true).Select(s => new DropDownViewModel() { Id = s.Id, Name = s.Name }).ToList();

            response.SalesPerson = _db.UserRoles.Where(x => x.RoleId == 4)
              .Select(u => new DropDownViewModel()
              {
                  Id = u.User.Id,
                  Name = string.Format("{0} {1}", u.User.FirstName, u.User.LastName)
              }).ToList();

            response.Customers = _db.Clients.Where(x => x.IsActive == true).OrderBy(c => c.Name).Select(c => new DropDownViewModel() { Id = c.Id, Name = c.Name }).ToList();

            response.Routes = _db.Routes.Where(x => x.IsActive == true).Select(r => new DropDownViewModel() { Id = r.Id, Name = r.Name }).ToList();

            response.ProductCategories = _db.ProductCategories.Where(x => x.IsActive == true).OrderBy(o => o.Name).Select(c => new DropDownViewModel() { Id = c.Id, Name = c.Name }).ToList();

            return response;
        }

        public List<DropDownViewModel> GetCustomersByRouteId(int routeId)
        {
            return _db.Clients.Where(x => x.IsActive == true && x.RouteId == routeId).Select(c => new DropDownViewModel { Id = c.Id, Name = c.Name }).ToList();
        }

        public async Task<ResponseViewModel> SaveSalesOrder(SalesOrderViewModel vm, User loggedInUser)
        {
            var response = new ResponseViewModel();

            try
            {
                var salesOrder = _db.SalesOrders.FirstOrDefault(x => x.Id == vm.Id);
                salesOrder.Discount = vm.Discount;
                salesOrder.OrderDate = new DateTime(vm.OrderDateYear, vm.DeliverDateMonth, vm.OrderDateDay, vm.OrderDateHour, vm.OrderDateMin, 0);
                if(vm.DeliverDate.HasValue)
                {
                    salesOrder.DeliveredDate = new DateTime(vm.DeliverDateYear, vm.DeliverDateMonth, vm.DeliverDateDay, 0, 0, 0);
                }
                salesOrder.OwnerId = vm.OwnerId;
                salesOrder.ShippingCharge = vm.ShippingCharge;
                salesOrder.Status = (int)VehicleTracker.Model.Enums.SalesOrderStatus.New;
                salesOrder.SubTotal = vm.SubTotal;
                salesOrder.TaxRate = vm.TaxRate;
                salesOrder.TotalTaxAmount = vm.TotalTaxAmount;
                salesOrder.TotalAmount = vm.TotalAmount;
                salesOrder.UpdatedById = loggedInUser.Id;
                salesOrder.UpdatedOn = DateTime.UtcNow;

                _db.SalesOrders.Update(salesOrder);

                response.Message = string.Format("Existing sales order ({0}) has been submitted successfully.", salesOrder.OrderNumber);

                await _db.SaveChangesAsync();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured while saving the sales order.";
            }

            return response;
        }

        public async Task<ResponseViewModel> SaveSalesOrderStep1(SalesOrderStep1ViewModel vm, User loggedInUser)
        {
            var response = new ResponseViewModel();

            try
            {
                var salesOrder = _db.SalesOrders.FirstOrDefault(x => x.Id == vm.Id);
                salesOrder.OrderDate = new DateTime(vm.OrderDateYear, vm.DeliverDateMonth, vm.OrderDateDay, vm.OrderDateHour, vm.OrderDateMin, 0);
                if (vm.DeliverDate.HasValue)
                {
                    salesOrder.DeliveredDate = new DateTime(vm.DeliverDateYear, vm.DeliverDateMonth, vm.DeliverDateDay, 0, 0, 0);
                }
                salesOrder.OwnerId = vm.OwnerId;
                salesOrder.Status = vm.Status;
                salesOrder.UpdatedById = loggedInUser.Id;
                
                salesOrder.UpdatedOn = DateTime.UtcNow;

                _db.SalesOrders.Update(salesOrder);

                await _db.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Sales order has been saved.";
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured while saving the sales order data.";
            }

            return response;
        }

        public async Task<ResponseViewModel> SaveSalesOrderStep3(SalesOrderStep3ViewModel vm, User loggedInUser)
        {
            var response = new ResponseViewModel();

            try
            {
                var salesOrder = _db.SalesOrders.FirstOrDefault(x => x.Id == vm.Id);
                salesOrder.Discount = vm.Discount;
                salesOrder.TaxRate = vm.TaxRate;
                salesOrder.TotalAmount = vm.TotalAmount;
                salesOrder.ShippingCharge = vm.ShippingCharge;
                salesOrder.TotalTaxAmount = vm.TotalTaxAmount;
                salesOrder.UpdatedById = loggedInUser.Id;
                salesOrder.UpdatedOn = DateTime.UtcNow;
                salesOrder.Remarks = vm.Remarks;

                _db.SalesOrders.Update(salesOrder);

                await _db.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Sales order has been saved.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured while saving the sales order data.";
            }

            return response;
        }

        public async Task<SalesOrderNumber> GetSalesOrderNumber()
        {
            var so = new SalesOrderNumber()
            {
                Number = await GenerateSalesOrderNumber()
            };
            return so;
        }

        public SalesOrderViewModel GetSalesOrderById(long id)
        {
            var response = new SalesOrderViewModel();

            var salesOrder = _db.SalesOrders.FirstOrDefault(x => x.Id == id);

            response.DeliverDate = salesOrder.DeliveredDate;
            response.Discount = salesOrder.Discount;
            response.Id = salesOrder.Id;
            response.IsActive = true;
            response.OrderDate = salesOrder.OrderDate;
            response.OrderNumber = salesOrder.OrderNumber;
            response.OwnerId = salesOrder.OwnerId.HasValue? salesOrder.OwnerId.Value:0;
            response.RouteId = salesOrder.OwnerId.HasValue ? salesOrder.Owner.RouteId.Value : 0;
            response.ShippingCharge = salesOrder.ShippingCharge;
            response.Status = salesOrder.Status;
            response.SubTotal = salesOrder.SubTotal;
            response.Discount = salesOrder.Discount;
            response.TaxRate = salesOrder.TaxRate;
            response.TotalAmount = salesOrder.TotalAmount;
            response.TotalTaxAmount = salesOrder.TotalTaxAmount;
            response.Remarks = salesOrder.Remarks;

            foreach (var item in salesOrder.SalesOrderItems)
            {
                var salesOrderItem = new SalesOrderItemViewModel()
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    ProductName = item.Product.ProductName,
                    CategoryName = item.Product.SubProductCategory.ProductCategory.Name,
                    SelectedCategoryId = item.Product.SubProductCategory.ProductCategoryId,
                    SubCategoryName = item.Product.SubProductCategory.Name,
                    SelectedSubCategoryId = item.Product.SubProductCategoryId,
                    SalesOrderId = item.OrderId,
                    Qty = item.Qty,
                    Total = item.Total,
                    UnitPrice = item.UnitPrice
                };

                response.Items.Add(salesOrderItem);
            }


            return response;
        }

        public List<ProductAvailabilityViewModel> GetWarehouseProductAvailability(int productId,int salesOrderId)
        {
            var response = new List<ProductAvailabilityViewModel>();

            var product = _db.Products.FirstOrDefault(x => x.Id == productId);

            var inventoryies= _db.ProductInventories.Where(x => x.ProductId == productId).GroupBy(x => x.WarehouseId)
                .Select(x => new 
                {
                    WarehouseId = x.Key,
                    ProductCount = x.Sum(p=>p.ReceivedQty)
                }).ToList();

            foreach (var inventory in inventoryies)
            {
                var warehourse = _db.Wharehouses.FirstOrDefault(w => w.Id == inventory.WarehouseId);

                var soldQty = _db.ProductInventoryOrders.Where(x => x.IsActive == true && x.ProductId == productId && x.WarehouseId == inventory.WarehouseId).Sum(s => s.Qty);

                var orderedQty = _db.ProductInventoryOrders.Where(x => x.OrderId == salesOrderId && x.WarehouseId == inventory.WarehouseId && x.ProductId == productId && x.IsActive == true);

                var warehouseAvailability = new ProductAvailabilityViewModel()
                {
                    AvailableQty = inventory.ProductCount - soldQty,
                    OrderedQty= orderedQty.Sum(x=>x.Qty),
                    ProductId=productId,
                    UnitPrice = product.UnitPrice,
                    ProductName = product.ProductName,
                    WarehouseId = inventory.WarehouseId,
                    WarhouseName = warehourse.Name
                };

                response.Add(warehouseAvailability);

            }

            return response;
        }

        public async Task<ResponseViewModel> AddProductToSalesOrder(SalesOrderProduct productDetail, User loggedInUser)
        {
            var response = new ResponseViewModel();

            try
            {
                var salesOrder = _db.SalesOrders.FirstOrDefault(x => x.Id == productDetail.SalesOrderId);

                var salesOrderItem = salesOrder.SalesOrderItems.FirstOrDefault(x => x.ProductId == productDetail.ProductId);

                var productInventoryOrders = salesOrder.ProductInventoryOrders
                    .FirstOrDefault(x => x.IsActive == true &&
                    x.WarehouseId == productDetail.WarehouseId &&
                    x.ActionType == (int)ProductInventoryOrderActionType.SalesOrder);

                if (salesOrderItem == null)
                {
                    salesOrderItem = new SalesOrderItem()
                    {
                        ProductId = productDetail.ProductId,
                        Qty = productDetail.Qty,
                        UnitPrice = productDetail.UntiPrice,
                        Total = productDetail.Qty * productDetail.UntiPrice
                    };

                    salesOrder.SalesOrderItems.Add(salesOrderItem);

                    AddNewProductInventoryOrder(salesOrder, productDetail, loggedInUser);
                }
                else
                {
                    salesOrderItem.Qty = salesOrderItem.Qty + productDetail.Qty;
                    salesOrderItem.UnitPrice = productDetail.UntiPrice;
                    salesOrderItem.Total = salesOrderItem.Qty * productDetail.UntiPrice;

                    if (productInventoryOrders == null)
                    {
                        AddNewProductInventoryOrder(salesOrder, productDetail, loggedInUser);
                    }
                    else
                    {
                        productInventoryOrders.Qty = productInventoryOrders.Qty + productDetail.Qty;
                        productInventoryOrders.UpdatedById = loggedInUser.Id;
                        productInventoryOrders.UpdatedOn = DateTime.UtcNow;

                        _db.ProductInventoryOrders.Update(productInventoryOrders);
                    }

                    _db.SalesOrderItems.Update(salesOrderItem);
                }

                salesOrder.SubTotal = salesOrder.CalculateSubTotal();
                salesOrder.TotalTaxAmount = salesOrder.CalculateTaxAmount();
                salesOrder.TotalAmount = salesOrder.CulculateTotalAmount();
                salesOrder.UpdatedById = loggedInUser.Id;
                salesOrder.UpdatedOn = DateTime.UtcNow;

                _db.SalesOrders.Update(salesOrder);
                await _db.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Product Quantity has been added to your sales order.";
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured while adding product quantity to sales order.";
            }


            

            return response;
        }

        public async Task<ResponseViewModel> DeleteSingleProductFromSalesOrder(SalesOrderProduct productDetail, User loggedInUser)
        {
            var response = new ResponseViewModel();

            try
            {
                var salesOrder = _db.SalesOrders.FirstOrDefault(x => x.Id == productDetail.SalesOrderId);

                var salesOrderProduct = salesOrder.SalesOrderItems.FirstOrDefault(x => x.ProductId == productDetail.ProductId);

                if(salesOrderProduct.Qty>1)
                {
                    salesOrderProduct.Qty = salesOrderProduct.Qty - 1;
                    salesOrderProduct.Total = salesOrderProduct.Qty * salesOrderProduct.UnitPrice;
                    _db.SalesOrderItems.Update(salesOrderProduct);
                }
                else if(salesOrderProduct.Qty==1)
                {
                    _db.SalesOrderItems.Remove(salesOrderProduct);
                }


                salesOrder.SubTotal = salesOrder.CalculateSubTotal();
                salesOrder.TotalTaxAmount = salesOrder.CalculateTaxAmount();
                salesOrder.TotalAmount = salesOrder.CulculateTotalAmount();
                salesOrder.UpdatedById = loggedInUser.Id;
                salesOrder.UpdatedOn = DateTime.UtcNow;

                _db.SalesOrders.Update(salesOrder);

                
                var productInventoryDetail = salesOrder.ProductInventoryOrders.FirstOrDefault(x => x.ProductId == productDetail.ProductId && x.WarehouseId==productDetail.WarehouseId );
                if(productInventoryDetail.Qty>1)
                {
                    productInventoryDetail.Qty = productInventoryDetail.Qty - 1;
                    productInventoryDetail.UpdatedById = loggedInUser.Id;
                    productInventoryDetail.UpdatedOn = DateTime.UtcNow;

                    _db.ProductInventoryOrders.Update(productInventoryDetail);
                }
                else if(productInventoryDetail.Qty==1)
                {
                    _db.ProductInventoryOrders.Remove(productInventoryDetail);
                }


                await _db.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Single product has been removed from the Quantity.";
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured while deleting the record.";
            }

            return response;
        }

        public async Task<ResponseViewModel> DeleteAllProductFromSalesOrder(int productId, int salesOrderId)
        {
            var response = new ResponseViewModel();

            try
            {
                var salesOrder = _db.SalesOrders.FirstOrDefault(x => x.Id == salesOrderId);

                var salesOrderProduct = salesOrder.SalesOrderItems.Where(x => x.ProductId == productId);

                var productInventoryOrders = salesOrder.ProductInventoryOrders.Where(x => x.ProductId == productId);

                foreach (var item in salesOrderProduct)
                {
                    _db.SalesOrderItems.Remove(item);
                }

                foreach (var item in productInventoryOrders)
                {
                    _db.ProductInventoryOrders.Remove(item);
                }

                await _db.SaveChangesAsync();

                salesOrder.SubTotal = salesOrder.CalculateSubTotal();
                salesOrder.TotalTaxAmount = salesOrder.CalculateTaxAmount();
                salesOrder.TotalAmount = salesOrder.CulculateTotalAmount();

                _db.SalesOrders.Update(salesOrder);

                await _db.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Product has been deleted from sales order.";
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());

                response.IsSuccess = false;
                response.Message = "Error has been occured while deleting the product from the sales order.";
            }

            return response;
        }

        public List<BasicSalesOrderDetailViewModel> GetNewSalesOrdersForSelectedDailyBeat(long dailyBeatId, User loggedInUser)
        {

            var route = _db.DailyVehicleBeats.FirstOrDefault(x => x.Id == dailyBeatId);

            var newSalesOrders = _db.SalesOrders.Where(x => x.IsActive == true && 
                x.Status == (int)Model.Enums.SalesOrderStatus.New && x.Owner.RouteId== route.RouteId)
                .OrderByDescending(s => s.CreatedOn);

            var salesOrderList = newSalesOrders.ToList();

            var response = GenerateBasicSalesOrderList(salesOrderList);

            return response;
        }

        public async Task<ResponseViewModel> AddSalesOrderToSelectedDailyBeat(long salesOrderId, long dailyBeatId, User loggedInUser)
        {
            var response = new ResponseViewModel();

            try
            {
                var salesOrder = _db.SalesOrders.FirstOrDefault(x => x.Id == salesOrderId);
                salesOrder.Status = (int)Model.Enums.SalesOrderStatus.PlannedForDelivery;
                salesOrder.UpdatedOn = DateTime.Now;
                salesOrder.UpdatedById = loggedInUser.Id;

                salesOrder.DailyVehicleBeatOrders.Add(new DailyVehicleBeatOrder()
                {
                    AssignedById = loggedInUser.Id,
                    DailyVehicleBeatId = dailyBeatId,
                    OrderId = salesOrderId,
                    AssignedDate = DateTime.UtcNow,
                });

                _db.SalesOrders.Update(salesOrder);
                
                //var dailyVehicleBeatOrder = 

                //_db.DailyVehicleBeatOrders.Add(dailyVehicleBeatOrder);
                await _db.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Sales order has been successfully added to the selected daily beat for deliver";

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Failuer to add sales order to selected daily beat. Please try again.";
            }


            return response;
        }

        public List<BasicSalesOrderDetailViewModel> GetSalesOrdersForSelectedDailyBeat(long dailyBeatId, User loggedInUser)
        {
            var dailyBeat = _db.DailyVehicleBeats.FirstOrDefault(x => x.Id == dailyBeatId);

            var assignedSalesOrders = _db.DailyVehicleBeatOrders.Where(x => x.DailyVehicleBeatId == dailyBeat.Id)
                .Select(x=>x.Order)
                .OrderByDescending(s => s.CreatedOn);

            var salesOrderList = assignedSalesOrders.ToList();

            var response = GenerateBasicSalesOrderList(salesOrderList);

            return response;
        }

        public async Task<ResponseViewModel> DeleteSaleOrderFromDailyBeat(int id,User loggedInUser)
        {
            var response = new ResponseViewModel();

            try
            {
                var dailyVehicleBeatOrder = _db.DailyVehicleBeatOrders.FirstOrDefault(x => x.Id == id);

                dailyVehicleBeatOrder.Order.Status = (int)Model.Enums.SalesOrderStatus.New;

                _db.DailyVehicleBeatOrders.Remove(dailyVehicleBeatOrder);

                await _db.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Selected sales order has been deleted from selected daily beat.";
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error has been occured while deleting sales order from daily beat.";
            }

            return response;
        }



        private void AddNewSalesOrderItems(SalesOrder salesOrder, List<SalesOrderItemViewModel> items)
        {
            foreach (var item in items)
            {
                var sitem = new SalesOrderItem()
                {
                    ProductId = item.ProductId,
                    Qty = item.Qty,
                    UnitPrice = item.UnitPrice,
                    Total = item.Qty * item.UnitPrice
                };

                salesOrder.SalesOrderItems.Add(sitem);
            }
        }

        private async Task<string> GenerateSalesOrderNumber()
        {
            string newPO = string.Empty;
            var currentPO = _db.AppSettings.FirstOrDefault(x => x.Key == "SaleOrderNumber");

            if (string.IsNullOrEmpty(currentPO.Value))
            {
                currentPO.Value = "000001";
                _db.AppSettings.Update(currentPO);
                await _db.SaveChangesAsync();
            }
            else
            {
                var value = int.Parse(currentPO.Value);
                value++;
                currentPO.Value = value.ToString().PadLeft(6, '0');
                _db.AppSettings.Update(currentPO);
                await _db.SaveChangesAsync();
            }

            newPO = $"SO-{currentPO.Value}";
            return newPO;
        }

        private void AddNewProductInventoryOrder(SalesOrder salesOrder, SalesOrderProduct productDetail, User loggedInUser)
        {
            var productInventoryOrders = new ProductInventoryOrder()
            {
                ProductId = productDetail.ProductId,
                WarehouseId = productDetail.WarehouseId,
                Qty = productDetail.Qty,
                ActionType = (int)ProductInventoryOrderActionType.SalesOrder,
                CreatedOn = DateTime.UtcNow,
                CreatedById = loggedInUser.Id,
                UpdatedOn = DateTime.UtcNow,
                UpdatedById = loggedInUser.Id,
                IsActive = true
            };

            salesOrder.ProductInventoryOrders.Add(productInventoryOrders);
        }

        private List<BasicSalesOrderDetailViewModel> GenerateBasicSalesOrderList(List<SalesOrder> salesOrderList)
        {
            var response = new List<BasicSalesOrderDetailViewModel>();

            foreach (var item in salesOrderList)
            {
                var vm = item.ToBasicVM();

                vm.DailyVehicleBeatOrderId = item.DailyVehicleBeatOrders.FirstOrDefault() != null ? item.DailyVehicleBeatOrders.FirstOrDefault().Id : 0; ;
                response.Add(vm);
            }

            return response;
        }

    }
}
