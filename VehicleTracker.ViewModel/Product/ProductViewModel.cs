using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracker.ViewModel
{
  public class ProductViewModel
  {

    public ProductViewModel()
    {
      Images = new List<ProductImageViewModel>();
    }
    public int Id { get; set; }
    public int ProductCategoryId { get; set; }
    public int ProductSubCategoryId { get; set; }
    public string Name { get; set; }
    public string ProductCode { get; set; }
    public decimal UnitPrice { get; set; }
    public int AvailableQty { get; set; }
    public int SupplierId { get; set; }

    public string Description { get; set; }
    public string DefaultImage { get; set; }
    public bool IsActive { get; set; }

    public string SupplierName { get; set; }
    public string CategoryName { get; set; }
    public string SubCategoryName { get; set; }


    public List<ProductImageViewModel> Images { get; set; }
  }

  public class ProductImageViewModel
  {
    public int Id { get; set; }
    public string ImageName { get; set; }
    public string ImageUrl { get; set; }
    public int ProductId { get; set; }
  }
}
