import { Component, Inject, Input, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { MatColors } from '@fuse/mat-colors';
import { DropDownModel } from 'app/models/common/drop-down.modal';
import { Upload } from 'app/models/common/upload';
import { ProductImageModel } from 'app/models/product/product.image.model';
import { ProductModel } from 'app/models/product/product.model';
import { ProductService } from 'app/services/product/product.service';
import { EMPTY, Observable } from 'rxjs';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ProductDetailComponent implements OnInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';
  
  action: string;
  product: ProductModel;
  productForm: FormGroup;
  dialogTitle: string;
  presetColors = MatColors.presets;
  isReadOnly:boolean;

  productCategories:DropDownModel[]=[];
  productSubCategories:DropDownModel[]=[];
  suppliers:DropDownModel[]=[];

  eqDataSource: ProductImageModel[];
  eqDisplayedColumns = ['action', 'image', 'imageName', "toogle"];

  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  @Input() accept = 'image/*';
  fileName: string = '';

  constructor(public matDialogRef: MatDialogRef<ProductDetailComponent>,
    private _productService:ProductService,
    private _snackBar: MatSnackBar,
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
      minOrderQty :new FormControl({ value: this.product.minOrderQty  , disabled: this.isReadOnly } , Validators.required),
      maxOrderQty :new FormControl({ value: this.product.maxOrderQty  , disabled: this.isReadOnly } , Validators.required),
      supplierId :new FormControl({ value: this.product.supplierId, disabled: this.isReadOnly }, Validators.required),
      productCategoryId  :new FormControl({ value: this.product.productCategoryId , disabled: this.isReadOnly }, Validators.required),
      productSubCategoryId  :new FormControl({ value: this.product.productSubCategoryId , disabled: this.isReadOnly }, Validators.required),
      description   :new FormControl({ value: this.product.description  , disabled: this.isReadOnly }, Validators.required),
      isActive: new FormControl(this.product.isActive),
    });
  }

  ngOnInit(): void {
    this.getSuppliers();
    if(this.product.id>0)
    {
      this.getProductImages();
    }
  }

  getSuppliers()
  {
    this._fuseProgressBarService.show();
    this._productService.getSuppliers()
      .subscribe(response=>{
        response.shift();
        this._fuseProgressBarService.hide();
        this.suppliers = response;
        if(response.length>0)
        {
          this.productForm.patchValue({'supplierId':this.suppliers[0].id});
          
        }
        this.getProductCategory();
      },error=>{
        this._fuseProgressBarService.hide();
      });
  }

  getProductCategory()
  {
    this._fuseProgressBarService.show();
    this._productService.getProductCategories()
      .subscribe(response=>{
        response.shift();
        this.productCategories = response;
        this._fuseProgressBarService.hide();
        if(response.length>0 )
        {
          if(!this.productCategoryId)
          {
            this.productForm.patchValue({'productCategoryId':this.productCategories[0].id});
          }
    
        }
        this.getProductSubCategories(this.productCategoryId);
      },error=>{
        this._fuseProgressBarService.hide();
      });
  }

  getProductSubCategories(categoryId:number)
  {
    
    this._fuseProgressBarService.show();
    this._productService.getProductSubCategories(categoryId)
      .subscribe(response=>{
        response.shift();
        this.productSubCategories = response;
        if(response.length>0)
        {
          if(!this.productSubCategoryId)
          {
            this.productForm.patchValue({'productSubCategoryId':this.productSubCategories[0].id});
          }    
        }
        this._fuseProgressBarService.hide();
      },error=>{

      });
  }

  productCategoryChanged(item:any)
  {
    this._fuseProgressBarService.show();
    this._productService.getProductSubCategories(this.productCategoryId)
      .subscribe(response=>{
        response.shift();
        this.productSubCategories = response;
        if(response.length>0)
        {
          this.productForm.patchValue({'productSubCategoryId':this.productSubCategories[0].id});
          
        }

        this._fuseProgressBarService.hide();
      },error=>{

      });
  }


  getProductImages()
  {
    this._fuseProgressBarService.show();
    this._productService.getAllProductImages(this.product.id)
      .subscribe(response=>{

          this.eqDataSource = response;
          this._fuseProgressBarService.hide();
      },error=>{
        this._fuseProgressBarService.hide();
      });
  }

  upload$: Observable<Upload> = EMPTY;
  precentage:any;
  onFileChange(event: any)
  {
    let fi = event.srcElement;
    const formData = new FormData();
    formData.set("id",this.product.id.toString());
    
    if(fi.files.length>0)
    {
        this._fuseProgressBarService.show();
        for (let index = 0; index < fi.files.length; index++) {
          
          formData.append('file', fi.files[index], fi.files[index].name);
        }
        this._productService.uploadProductImage(formData).subscribe(res=>
          {
            this.precentage =res;
            if(res.state=="DONE")
            {
              //item.isUploading=false;
              this._fuseProgressBarService.hide();
              this._productService.onProductImageUploaded.next(true);
              this.getProductImages();
              this._snackBar.open("Image has been uploaded successfully", 'Success', {
                duration: 2500,
                horizontalPosition: this.horizontalPosition,
                verticalPosition: this.verticalPosition,
              });
            }
            //progress
          },error=>{
            this._fuseProgressBarService.hide();
            //item.isUploading=false;
            this._snackBar.open("Network error has been occured. Please try again.", 'Error', {
              duration: 2500,
              horizontalPosition: this.horizontalPosition,
              verticalPosition: this.verticalPosition,
            });
          });
/*         this._quotationService.uploadQuotationFiles(formData)
          .subscribe(response=>{
 
          },error=>{
            console.log("Error occured");
            
          }); */
    } 
  }


  deleteImage(item:ProductImageModel)
  {

  }

  onDefaultChangeEvent(event: any, item: ProductImageModel) {
    this._fuseProgressBarService.show();
    for (let index = 0; index < this.eqDataSource.length; index++) {
      if (item.id != this.eqDataSource[index].id) {
        this.eqDataSource[index].isDefault = false;
      }
    }

    this._productService.makeDefaultImage(item)
      .subscribe(response => {
        this._fuseProgressBarService.hide();
        this._productService.onProductImageUploaded.next(true);
        this._snackBar.open(response.message, 'OK', {
          verticalPosition: 'top',
          duration: 2000
        });
      }, error => {
        this._fuseProgressBarService.hide();
      })

  }



  get productCategoryId()
  {
    return this.productForm.get("productCategoryId").value;
  }

  get productSubCategoryId()
  {
    return this.productForm.get("productSubCategoryId").value;
  }

}
