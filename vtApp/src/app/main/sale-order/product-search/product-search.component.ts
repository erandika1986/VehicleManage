import { Component, Inject, Input, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { MatColors } from '@fuse/mat-colors';
import { DropDownModel } from 'app/models/common/drop-down.modal';
import { DropdownService } from 'app/services/common/dropdown.service';
import { SalesOrderService } from 'app/services/sales-order/sales-order.service';

@Component({
  selector: 'app-product-search',
  templateUrl: './product-search.component.html',
  styleUrls: ['./product-search.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ProductSearchComponent implements OnInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';
  
  action: string;
  productSearchForm: FormGroup;
  dialogTitle: string;
  presetColors = MatColors.presets;
  isReadOnly:boolean;

  productCategories:DropDownModel[]=[];
  productSubCategories:DropDownModel[]=[];
  products:DropDownModel[]=[];


  eqDisplayedColumns = ['action', 'image', 'imageName', "toogle"];

  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  @Input() accept = 'image/*';
  fileName: string = '';
  
  constructor(public matDialogRef: MatDialogRef<ProductSearchComponent>,
    private dropdownSevice:DropdownService,
    private salesOrderService:SalesOrderService,
    private _snackBar: MatSnackBar,
    private _fuseProgressBarService: FuseProgressBarService,
    @Inject(MAT_DIALOG_DATA) private _data: any) { 
      this.productCategories = _data.productCategories;
      this.productSearchForm = this.createProductForm();
    }

  ngOnInit(): void {
  }


  createProductForm(): FormGroup {
    return new FormGroup({

      productCategoryId  :new FormControl({ value: this.productCategories[0].id }, Validators.required),
      productSubCategoryId  :new FormControl({ value: null }, Validators.required),
      productId  :new FormControl({ value: null }, Validators.required)

    });
  }

  onProductCategoryChanged(categoryId:number)
  {
    this._fuseProgressBarService.show();
    this.dropdownSevice.getProductSubCategories(categoryId)
      .subscribe(response=>{
        response.shift();
        this.productSubCategories = response;
        if(response.length>0)
        {
          this.productSearchForm.patchValue({'productSubCategoryId':this.productSubCategories[0].id}); 
        }
        this._fuseProgressBarService.hide();
      },error=>{
        this._fuseProgressBarService.hide();
      });
  }

  onProductSubCategoryChanged(subCategoryId:number)
  {
    this._fuseProgressBarService.show();
    this.dropdownSevice.getProducts(subCategoryId)
      .subscribe(response=>{
        response.shift();
        this.products = response;
        if(response.length>0)
        {
          this.productSearchForm.patchValue({'productId':this.products[0].id}); 
        }
        this._fuseProgressBarService.hide();
      },error=>{
        this._fuseProgressBarService.hide();
      });
  }

  clearPreviousInventoryDetails()
  {
    
  }

  searchInventory()
  {

  }

}
