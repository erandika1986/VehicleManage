import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { MatColors } from '@fuse/mat-colors';
import { DropDownModel } from 'app/models/common/drop-down.modal';
import { ProductSubCategoryModel } from 'app/models/product/product.sub.category.model';
import { ProductService } from 'app/services/product/product.service';

@Component({
  selector: 'app-product-sub-category-detail',
  templateUrl: './product-sub-category-detail.component.html',
  styleUrls: ['./product-sub-category-detail.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ProductSubCategoryDetailComponent implements OnInit {

  action: string;
  subcategory: ProductSubCategoryModel;
  subcategoryForm: FormGroup;
  dialogTitle: string;
  presetColors = MatColors.presets;
  isReadOnly:boolean;

  productCategories:DropDownModel[]=[];

  constructor(public matDialogRef: MatDialogRef<ProductSubCategoryDetailComponent>,
    private _productServoce:ProductService,
    private _fuseProgressBarService: FuseProgressBarService,
    @Inject(MAT_DIALOG_DATA) private _data: any) { 
      this.subcategory = _data.subcategory;
      this.action = _data.action;
      this.isReadOnly = _data.isReadOnly;

      if (this.action === 'edit') {
        this.dialogTitle = "Edit Product Sub Category";
      }
      else {
        this.dialogTitle = 'New Product Sub Category';
        /*         this.event = new CalendarEventModel({
                    start: _data.date,
                    end  : _data.date
                }); */
      }
  
      this.subcategoryForm = this.createSubCategoryForm();
    }

  ngOnInit(): void {

    this.getProductCategories();
  }

  createSubCategoryForm() {

    console.log(this.subcategory);
    
    return new FormGroup({
      id: new FormControl({ value: this.subcategory.id, disabled: true }),
      productCategoryId:new FormControl({ value: this.subcategory.productCategoryId, disabled: this.isReadOnly } , Validators.required), 
      name: new FormControl({ value: this.subcategory.name, disabled: this.isReadOnly } , Validators.required),
      description:new FormControl({ value: this.subcategory.description, disabled: this.isReadOnly }, Validators.required),
      isActive: new FormControl(this.subcategory.isActive),
    });
  }

  getProductCategories()
  {
    this._fuseProgressBarService.show();
      this._productServoce.getProductCategories().subscribe(response=>{
          response.shift();
          this.productCategories=response;
          if(this._productServoce.selectedCategoryId!=0)
          {
            this.subcategoryForm.patchValue({'productCategoryId':this._productServoce.selectedCategoryId});
          }

          this._fuseProgressBarService.hide();
      },error=>{
        this._fuseProgressBarService.hide();
      })
  }

}
