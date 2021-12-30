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
            var response = new List<BasicSalesOrderDetailViewModel>();

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

            foreach (var item in salesOrderList)
            {
                var salesOrder = new BasicSalesOrderDetailViewModel()
                {
                    CreatedBy = string.Format("{0} {1}", item.CreatedBy.FirstName, item.CreatedBy.LastName),
                    CreatedOn = item.CreatedOn.ToString("MM/dd/yyyy"),
                    OrderDate = item.OrderDate.ToString("MM/dd/yyyy"),
                    OrderNumber = item.OrderNumber,
                    OwnerAddress = item.Owner != null ? item.Owner.Address : string.Empty,
                    OwnerName = item.Owner!=null? item.Owner.Name:string.Empty,
                    Route = item.Owner!=null? item.Owner.Route.Name:string.Empty,
                    Status = item.StatusNavigation.Name,
                    Id = item.Id,
                    Total = item.TotalAmount,
                    TotalQty = item.SalesOrderItems.Sum(x => x.Qty),
                    UpdatedBy = string.Format("{0} {1}", item.UpdatedBy.FirstName, item.UpdatedBy.LastName),
                    UpdatedOn = item.UpdatedOn.ToString("MM/dd/yyyy")

                };

                response.Add(salesOrder);
            }

            return response;
        }

        public List<BasicSalesOrderDetailViewModel> GetMySalesOrders(SalesOrderFilter filters, User loggedInUser)
        {
            var response = new List<BasicSalesOrderDetailViewModel>();

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

            foreach (var item in salesOrderList)
            {
                var salesOrder = new BasicSalesOrderDetailViewModel()
                {
                    CreatedBy = string.Format("{0} {1}", item.CreatedBy.FirstName, item.CreatedBy.LastName),
                    CreatedOn = item.CreatedOn.ToString("MM/dd/yyyy"),
                    OrderDate = item.OrderDate.ToString("MM/dd/yyyy"),
                    OrderNumber = item.OrderNumber,
                    OwnerAddress = item.Owner.Address,
                    OwnerName = item.Owner.Name,
                    Route = item.Owner.Route.Name,
                    Status = item.StatusNavigation.Name,
                    Id = item.Id,
                    Total = item.TotalAmount,
                    TotalQty = item.SalesOrderItems.Sum(x => x.Qty),
                    UpdatedBy = string.Format("{0} {1}", item.UpdatedBy.FirstName, item.UpdatedBy.LastName),
                    UpdatedOn = item.UpdatedOn.ToString("MM/dd/yyyy")

                };

                response.Add(salesOrder);
            }

            return response;
        }

        public SalesOrderMasterDataViewModel GetSalesOrderMasterData()
        {
            var response = new SalesOrderMasterDataViewModel();

            response.Statuses = _db.SalesOrderStatuses.Where(x => x.IsActive == true).Select(s => new DropDownViewModal() { Id = s.Id, Name = s.Name }).ToList();

            response.SalesPerson = _db.UserRoles.Where(x => x.RoleId == 4)
              .Select(u => new DropDownViewModal()
              {
                  Id = u.User.Id,
                  Name = string.Format("{0} {1}", u.User.FirstName, u.User.LastName)
              }).ToList();

            response.Customers = _db.Clients.Where(x => x.IsActive == true).OrderBy(c => c.Name).Select(c => new DropDownViewModal() { Id = c.Id, Name = c.Name }).ToList();

            response.Routes = _db.Routes.Where(x => x.IsActive == true).Select(r => new DropDownViewModal() { Id = r.Id, Name = r.Name }).ToList();

            response.ProductCategories = _db.ProductCategories.Where(x => x.IsActive == true).OrderBy(o => o.Name).Select(c => new DropDownViewModal() { Id = c.Id, Name = c.Name }).ToList();

            return response;
        }

        public List<DropDownViewModal> GetCustomersByRouteId(int routeId)
        {
            return _db.Clients.Where(x => x.IsActive == true && x.RouteId == routeId).Select(c => new DropDownViewModal { Id = c.Id, Name = c.Name }).ToList();
        }

        public async Task<ResponseViewModel> SaveSalesOrder(SalesOrderViewModel vm, User loggedInUser)
        {
            var response = new ResponseViewModel();

            try
            {
                var salesOrder = _db.SalesOrders.FirstOrDefault(x => x.Id == vm.Id);
                if (salesOrder == null)
                {
                    salesOrder.IsActive = true;
                    salesOrder.CreatedById = loggedInUser.Id;
                    salesOrder.CreatedOn = DateTime.UtcNow;
                    salesOrder.Discount = vm.Discount;
                    salesOrder.OrderDate = vm.OrderDate;
                    salesOrder.OrderNumber = vm.OrderNumber;
                    salesOrder.OwnerId = vm.OwnerId;
                    salesOrder.ShippingCharge = vm.ShippingCharge;
                    salesOrder.Status = vm.Status;
                    salesOrder.SubTotal = vm.SubTotal;
                    salesOrder.TaxRate = vm.TaxRate;
                    salesOrder.TotalTaxAmount = vm.TotalTaxAmount;
                    salesOrder.TotalAmount = vm.TotalAmount;
                    salesOrder.UpdatedById = loggedInUser.Id;
                    salesOrder.UpdatedOn = DateTime.UtcNow;

                    salesOrder.SalesOrderItems = new HashSet<SalesOrderItem>();

                    AddNewSalesOrderItems(salesOrder, vm.Items);

                    _db.SalesOrders.Add(salesOrder);

                    response.Message = "New sales order has been added successfully.";
                }
                else
                {
                    salesOrder.Discount = vm.Discount;
                    salesOrder.OrderDate = vm.OrderDate;
                    salesOrder.OwnerId = vm.OwnerId;
                    salesOrder.ShippingCharge = vm.ShippingCharge;
                    salesOrder.Status = vm.Status;
                    salesOrder.SubTotal = vm.SubTotal;
                    salesOrder.TaxRate = vm.TaxRate;
                    salesOrder.TotalTaxAmount = vm.TotalTaxAmount;
                    salesOrder.TotalAmount = vm.TotalAmount;
                    salesOrder.UpdatedById = loggedInUser.Id;
                    salesOrder.UpdatedOn = DateTime.UtcNow;

                    var existingItems = salesOrder.SalesOrderItems.ToList();

                    //Add newly added sales order items
                    var newlyAddedItems = (from u in vm.Items where !existingItems.Any(x => x.Id == u.Id) select u).ToList();

                    AddNewSalesOrderItems(salesOrder, newlyAddedItems);

                    //Updated existing order items
                    var updateItems = (from u in vm.Items where existingItems.Any(x => x.Id == u.Id) select u).ToList();

                    foreach (var item in updateItems)
                    {
                        var existingSalesOrderItem = existingItems.FirstOrDefault(x => x.Id == item.Id);

                        existingSalesOrderItem.ProductId = item.ProductId;
                        existingSalesOrderItem.Qty = item.Qty;
                        existingSalesOrderItem.Total = item.Total;
                        existingSalesOrderItem.UnitPrice = item.UnitPrice;

                        _db.SalesOrderItems.Update(existingSalesOrderItem);
                    }

                    //Delete deleted items
                    var deletedItems = (from d in existingItems where !vm.Items.Any(x => x.Id == d.Id) select d).ToList();
                    foreach (var item in deletedItems)
                    {
                        _db.SalesOrderItems.Remove(item);
                    }

                    response.Message = string.Format("Existing sales order ({0}) has been updated successfully.", salesOrder.OrderNumber);
                }

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
                salesOrder.DeliveredDate = vm.DeliverDate;
                salesOrder.OwnerId = vm.OwnerId;
                salesOrder.Status = vm.Status;

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
            throw new NotImplementedException();
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
            response.ShippingCharge = salesOrder.ShippingCharge;
            response.Status = salesOrder.Status;
            response.SubTotal = salesOrder.SubTotal;
            response.TaxRate = salesOrder.TaxRate;
            response.TotalAmount = salesOrder.TotalAmount;
            response.TotalTaxAmount = salesOrder.TotalTaxAmount;

            foreach (var item in salesOrder.SalesOrderItems)
            {
                var salesOrderItem = new SalesOrderItemViewModel()
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    ProductName = item.Product.ProductName,
                    CategoryName = item.Product.SubProductCategory.ProductCategory.Name,
                    SubCategoryName = item.Product.SubProductCategory.Name,
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
                    salesOrderItem.Total = productDetail.Qty * productDetail.UntiPrice;

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

        public async Task<ResponseViewModel> DeleteSingleProductRoSalesOrder(SalesOrderProduct productDetail, User loggedInUser)
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

    }
}
