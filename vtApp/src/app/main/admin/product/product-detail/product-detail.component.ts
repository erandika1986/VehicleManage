import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { MatColors } from '@fuse/mat-colors';
import { DropDownModel } from 'app/models/common/drop-down.modal';
import { ProductModel } from 'app/models/product/product.model';
import { ProductService } from 'app/services/product/product.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ProductDetailComponent implements OnInit {

  action: string;
  product: ProductModel;
  productForm: FormGroup;
  dialogTitle: string;
  presetColors = MatColors.presets;
  isReadOnly:boolean;

  productCategories:DropDownModel[]=[];
  productSubCategories:DropDownModel[]=[];
  suppliers:DropDownModel[]=[];

  constructor(public matDialogRef: MatDialogRef<ProductDetailComponent>,
    private _productServoce:ProductService,
    private _fuseProgressBarService: FuseProgressBarService,
    @Inject(MAT_DIALOG_DATA) private _data: any) {
      this.product = _data.product;
      this.action = _data.action;
      this.isReadOnly = _data.isReadOnly;

      if (this.action === 'edit') {
        this.dialogTitle = "Edit Product";
      }
      else {
        this.dialogTitle = 'New Product';
        /*         this.event = new CalendarEventModel({
                    start: _data.date,
                    end  : _data.date
                }); */
      }
  
      this.productForm = this.createProductForm();
     }
  createProductForm(): FormGroup {
    return new FormGroup({
      id: new FormControl({ value: this.product.id, disabled: true }),
      name: new FormControl({ value: this.product.name, disabled: this.isReadOnly } , Validators.required),
      productCode: new FormControl({ value: this.product.productCode, disabled: this.isReadOnly } , Validators.required),
      unitPrice :new FormControl({ value: this.product.unitPrice  , disabled: this.isReadOnly } , Validators.required), 
      availableQty :new FormControl({ value: this.product.availableQty  , disabled: this.isReadOnly } , Validators.required), 
      supplierId :new FormControl({ value: this.product.supplierId, disabled: this.isReadOnly }, Validators.required),
      productCategoryId  :new FormControl({ value: this.product.productCategoryId , disabled: this.isReadOnly }, Validators.required),
      productSubCategoryId  :new FormControl({ value: this.product.productSubCategoryId , disabled: this.isReadOnly }, Validators.required),
      description   :new FormControl({ value: this.product.description  , disabled: this.isReadOnly }, Validators.required),
      isActive: new FormControl(this.product.isActive),
    });
  }

  ngOnInit(): void {
    this.getSuppliers();
  }

  getSuppliers()
  {
    this._fuseProgressBarService.show();
    this._productServoce.getSuppliers()
      .subscribe(response=>{
        this._fuseProgressBarService.hide();
        this.suppliers = response;
        this.getProductCategory();
      },error=>{
        this._fuseProgressBarService.hide();
      });
  }

  getProductCategory()
  {
    this._fuseProgressBarService.show();
    this._productServoce.getProductCategories()
      .subscribe(response=>{
        this.productCategories = response;
        this._fuseProgressBarService.hide();
        this.getProductSubCategories(this.product.productCategoryId);
      },error=>{
        this._fuseProgressBarService.hide();
      });
  }

  productCategoryChanged(item:any)
  {
    this.getProductSubCategories(this.productCategoryId);
  }

  getProductSubCategories(categoryId:number)
  {
    this._fuseProgressBarService.show();
    this._productServoce.getProductSubCategories(categoryId)
      .subscribe(response=>{
        this.productSubCategories = response;
        this._fuseProgressBarService.hide();
      },error=>{

      });
  }

  get productCategoryId()
  {
    return this.productForm.get("productCategoryId").value;
  }

}
