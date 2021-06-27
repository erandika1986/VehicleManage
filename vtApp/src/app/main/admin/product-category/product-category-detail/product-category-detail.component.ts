import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatColors } from '@fuse/mat-colors';
import { ProductCategoryModel } from 'app/models/product/product.category.model';

@Component({
  selector: 'app-product-category-detail',
  templateUrl: './product-category-detail.component.html',
  styleUrls: ['./product-category-detail.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ProductCategoryDetailComponent implements OnInit {

  action: string;
  category: ProductCategoryModel;
  categoryForm: FormGroup;
  dialogTitle: string;
  presetColors = MatColors.presets;
  isReadOnly:boolean;

  constructor(public matDialogRef: MatDialogRef<ProductCategoryDetailComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: any) { 
      this.category = _data.category;
      this.action = _data.action;
      this.isReadOnly = _data.isReadOnly;

      if (this.action === 'edit') {
        this.dialogTitle = "Edit Product Category";
      }
      else {
        this.dialogTitle = 'New Product Category';
        /*         this.event = new CalendarEventModel({
                    start: _data.date,
                    end  : _data.date
                }); */
      }
  
      this.categoryForm = this.createCategoryForm();
    }

  ngOnInit(): void {
  }

  createCategoryForm() {
    return new FormGroup({
      id: new FormControl({ value: this.category.id, disabled: true }),
      name: new FormControl({ value: this.category.name, disabled: this.isReadOnly } , Validators.required),
      description:new FormControl({ value: this.category.description, disabled: this.isReadOnly }, Validators.required),
      isActive: new FormControl(this.category.isActive),
    });
  }

}
