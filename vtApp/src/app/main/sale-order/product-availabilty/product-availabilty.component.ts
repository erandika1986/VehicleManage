import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { MatColors } from '@fuse/mat-colors';
import { DropDownModel } from 'app/models/common/drop-down.modal';
import { ProductAvailabilityModel } from 'app/models/sales-order/product.availability.model';
import { SalesOrderProductModel } from 'app/models/sales-order/sales.order.product.model';
import { DropdownService } from 'app/services/common/dropdown.service';
import { SalesOrderService } from 'app/services/sales-order/sales-order.service';

@Component({
  selector: 'app-product-availabilty',
  templateUrl: './product-availabilty.component.html',
  styleUrls: ['./product-availabilty.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ProductAvailabiltyComponent implements OnInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  salesOrderId:number;
  categoryId:number;
  subCategoryId:number;
  productId :number;

  action: string;
  //customer: CustomerModel;
  productSearchForm: FormGroup;
  dialogTitle: string;
  presetColors = MatColors.presets;
  
  productCategories: DropDownModel[]=[];
  productSubCategories:DropDownModel[]=[];
  products:DropDownModel[]=[];

  productAvailabilities:ProductAvailabilityModel[]=[];

  constructor(public matDialogRef: MatDialogRef<ProductAvailabiltyComponent>,
    private _fuseProgressBarService: FuseProgressBarService,
    private _snackBar: MatSnackBar,
    private salesOrderService:SalesOrderService,
    @Inject(MAT_DIALOG_DATA) private _data: any,
    private dropdownService:DropdownService) { 
      this.salesOrderId = this._data.salesOrderId;
      this.categoryId=this._data.categoryId;
      this.subCategoryId=this._data.subCategoryId;
      this.productId=this._data.productId;

    }

  ngOnInit(): void {
    this.productSearchForm = this.createSearchForm();
    this.getProductCategory();
  }

  createSearchForm()
  {
    return new FormGroup({
      selectedCategoryId: new FormControl(this.categoryId==0?null:this.categoryId, Validators.required),
      selectedSubCategoryId: new FormControl(this.subCategoryId==0?null:this.subCategoryId, Validators.required),
      selectedProudctId: new FormControl(this.productId==0?null:this.productId, Validators.required)
    });


  }

  getProductCategory()
  {
    this.dropdownService.getProductCategories()
      .subscribe(response=>{

        this.productCategories = response;
        if(response.length>0)
        {
          if(this.categoryId==0)
          {
            this.productSearchForm.get('selectedCategoryId').setValue(response[0].id);
          }
          this.getProductSubCategory();
        }
        else
        {
          this.productAvailabilities=[];
        }


      },error=>{

      });
  }

  getProductSubCategory(fistTimeLoad:boolean=true)
  {
    this.dropdownService.getProductSubCategories(this.productSearchForm.get('selectedCategoryId').value)
      .subscribe(response=>{

        this.productSubCategories = response;
        if(response.length>0)
        {
          if(!fistTimeLoad)
          {
            this.productSearchForm.get('selectedSubCategoryId').setValue(response[0].id);
          }

          this.getProducts(fistTimeLoad);
        }
        else
        {
          this.productAvailabilities=[];
        }


      },error=>{

      });
  }

  getProducts(fistTimeLoad:boolean=true)
  {
    this.dropdownService.getProducts(this.productSearchForm.get('selectedSubCategoryId').value)
      .subscribe(response=>{

        this.products = response;
        if(response.length>0)
        {
          if(!fistTimeLoad)
          {
            this.productSearchForm.get('selectedProudctId').setValue(response[0].id);
          }
          this.getproductInventoryDetails();
        }
        else
        {
          this.productSearchForm.get('selectedProudctId').setValue(null);
          this.productAvailabilities=[];
        }


      },error=>{

      });
  }

  onProductCategoryChanged(item:any)
  {
    this.getProductSubCategory(false)
  }

  onProductSubCategoryChanged(item:any)
  {
    this.getProducts(false);
  }

  onProductChanged(item:any)
  {
    this.getproductInventoryDetails();
  }

  getproductInventoryDetails()
  {
    this.salesOrderService.getWarehouseProductAvailability(this.selectedProductId,this.salesOrderId)
      .subscribe(response=>{
        this.productAvailabilities = response;        

      },error=>{

      });
  }

  addProducts(item:ProductAvailabilityModel)
  {
    if(item.availableQty>=item.newlyAddedQty)
    {
      let orderProduct = new SalesOrderProductModel();
      orderProduct.productId = item.productId;
      orderProduct.qty = item.newlyAddedQty;
      orderProduct.salesOrderId = this.salesOrderId;
      orderProduct.untiPrice = item.unitPrice;
      orderProduct.warehouseId = item.warehouseId;
  
      this._fuseProgressBarService.show();
      this.salesOrderService.addProductToSalesOrder(orderProduct)
        .subscribe(response=>{
          this._fuseProgressBarService.hide();
          this._snackBar.open(response.message, response.isSuccess? 'Success':'Error', {
            duration: 2500,
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition,
          });
          this.salesOrderService.onSalesOrderChanged.next(true);
          this.getproductInventoryDetails();
        },error=>{
          this._fuseProgressBarService.hide();
          this._snackBar.open("Error has been occured in client application.", 'Error', {
            duration: 2500,
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition,
          });
        });
    }
    else
    {

        this._snackBar.open("Enable to add the product quanitty to sales order since requested quantity is not available in selected warehouse.", 'Success', {
          duration: 2500,
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
        });
    }

  }

  addSingleProductToOrder(item:ProductAvailabilityModel)
  {
    if(item.availableQty>=1)
    {
      let orderProduct = new SalesOrderProductModel();
      orderProduct.productId = item.productId;
      orderProduct.qty = 1;
      orderProduct.salesOrderId = this.salesOrderId;
      orderProduct.untiPrice = item.unitPrice;
      orderProduct.warehouseId = item.warehouseId;
  
      this._fuseProgressBarService.show();
      this.salesOrderService.addProductToSalesOrder(orderProduct)
        .subscribe(response=>{
          this._fuseProgressBarService.hide();
          this._snackBar.open(response.message, response.isSuccess? 'Success':'Error', {
            duration: 2500,
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition,
          });
          this.salesOrderService.onSalesOrderChanged.next(true);
          this.getproductInventoryDetails();
        },error=>{
          this._fuseProgressBarService.hide();
          this._snackBar.open("Error has been occured in client application.", 'Error', {
            duration: 2500,
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition,
          });
        });
    }
    else
    {

        this._snackBar.open("Enable to add the product quanitty to sales order since requested quantity is not available in selected warehouse.", 'Success', {
          duration: 2500,
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
        });
    }
  }

  removeSingleProductFromOrder(item:ProductAvailabilityModel)
  {
    let orderProduct = new SalesOrderProductModel();
    orderProduct.productId = item.productId;
    orderProduct.qty = 1;
    orderProduct.salesOrderId = this.salesOrderId;
    orderProduct.untiPrice = item.unitPrice;
    orderProduct.warehouseId = item.warehouseId;

    this._fuseProgressBarService.show();
    this.salesOrderService.deleteSingleProductRoSalesOrder(orderProduct)
      .subscribe(response=>{
        this._fuseProgressBarService.hide();
        this._snackBar.open("Product Quantity has been added to the order successfully.", 'Success', {
          duration: 2500,
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
        });
        this.salesOrderService.onSalesOrderChanged.next(true);
        this.getproductInventoryDetails();
      },error=>{
        this._fuseProgressBarService.hide();
        this._snackBar.open("Error has been occured in client application.", 'Error', {
          duration: 2500,
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
        });
      });
  }

  get selectedCategoryId():number
  {
    return this.productSearchForm.get('selectedCategoryId').value;
  }

  get selectedSubCategoryId():number
  {
    return this.productSearchForm.get('selectedSubCategoryId').value;
  }

  get selectedProductId():number
  {
    return this.productSearchForm.get('selectedProudctId').value;
  }

}