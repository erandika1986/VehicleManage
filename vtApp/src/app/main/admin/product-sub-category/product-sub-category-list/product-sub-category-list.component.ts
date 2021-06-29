import { Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseConfirmDialogComponent } from '@fuse/components/confirm-dialog/confirm-dialog.component';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { DropDownModel } from 'app/models/common/drop-down.modal';
import { Upload } from 'app/models/common/upload';
import { ProductSubCategoryModel } from 'app/models/product/product.sub.category.model';
import { ProductService } from 'app/services/product/product.service';
import { EMPTY, Observable } from 'rxjs';
import { ProductSubCategoryDetailComponent } from '../product-sub-category-detail/product-sub-category-detail.component';

@Component({
  selector: 'product-sub-category-list',
  templateUrl: './product-sub-category-list.component.html',
  styleUrls: ['./product-sub-category-list.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class ProductSubCategoryListComponent implements OnInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  dataSource = new MatTableDataSource([]);


  dialogRef: any;
  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;

  displayedColumns = ["buttons", "picture", "name","description","productCategoryName"];

  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  @ViewChild('input') input: ElementRef;

  categories:DropDownModel[]=[];
  selectedCategoryId:number=0;
  
  constructor(private _route: ActivatedRoute,
    private _productService: ProductService,
    private _fuseProgressBarService: FuseProgressBarService,
    private _matDialog: MatDialog,
    private _snackBar: MatSnackBar,
    public _router: Router) { }

  ngOnInit(): void {

    this.loadList();
    this.getCategories();
  }

  edit(item: ProductSubCategoryModel): void {
    this.dialogRef = this._matDialog.open(ProductSubCategoryDetailComponent, {
      panelClass: 'category-sub-form-dialog',
      data: {
        subcategory: item,
        action: "edit",
        isReadOnly:false
      }
    });

    this.dialogRef.afterClosed()
      .subscribe(response => {
        if (!response) {
          return;
        }
        const actionType: string = response[0];
        const formData: FormGroup = response[1];
        switch (actionType) {
          /**
           * Save
           */
          case 'save':
            this.save(formData.getRawValue());


            break;
          /**
           * Delete
           */
          case 'delete':

            this.delete(formData.getRawValue());

            break;
        }
      });

  }

  view(item:ProductSubCategoryModel)
  {
    console.log(item);
    
    this.dialogRef = this._matDialog.open(ProductSubCategoryDetailComponent, {
      panelClass: 'category-sub-form-dialog',
      data: {
        subcategory: item,
        action: "edit",
        isReadOnly:true
      }
    });

    this.dialogRef.afterClosed()
      .subscribe(response => {
        if (!response) {
          return;
        }
        const actionType: string = response[0];
        let formData: FormGroup = response[1];
        switch (actionType) {

          case 'save':


            break;

          case 'delete':



            break;
        }
      });
  }

  addNew(): void {

    let subCategory: ProductSubCategoryModel = new ProductSubCategoryModel();
    subCategory.id = 0;
    subCategory.name = "";
    subCategory.description="";
    subCategory.picture="";
    subCategory.isActive = true;
    

    this.dialogRef = this._matDialog.open(ProductSubCategoryDetailComponent, {
      panelClass: 'category-sub-form-dialog',
      data: {
        subcategory: subCategory,
        action: "add"
      }
    });

    this.dialogRef.afterClosed()
      .subscribe(response => {
        if (!response) {
          return;
        }

        const formData: FormGroup = response;
        this.save(formData.getRawValue());

      });
  }


  loadList() {
    this._fuseProgressBarService.show();
    this._productService.getAllByCategoryId(this.selectedCategoryId)
      .subscribe(response => {
        this._fuseProgressBarService.hide();
        this.dataSource = new MatTableDataSource(response);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      }, error => {
        this._fuseProgressBarService.hide();
      })
  }


  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }


  save(subcategory: ProductSubCategoryModel) {
    this._fuseProgressBarService.show();
    this._productService.saveProductSubCategory(subcategory)
      .subscribe(response => {

        this._fuseProgressBarService.hide();
        if (response.isSuccess) {
          this._snackBar.open(response.message, 'Success', {
            duration: 2500,
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition,
          });

          this.loadList();;
        }
        else {
          this._snackBar.open(response.message, 'Error', {
            duration: 2500,
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition,
          });
        }
      }, error => {
        this._fuseProgressBarService.hide();
        this._snackBar.open("Network error has been occured. Please try again.", 'Error', {
          duration: 2500,
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
        });
      });
  }


  delete(subcategory: ProductSubCategoryModel) {
    this.confirmDialogRef = this._matDialog.open(FuseConfirmDialogComponent, {
      disableClose: false
    });

    this.confirmDialogRef.componentInstance.confirmMessage = 'Are you sure you want to delete this record?';

    this.confirmDialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._fuseProgressBarService.show();
        this._productService. deleteProductSubCategory(subcategory.id)
          .subscribe(response => {

            this._fuseProgressBarService.hide();
            if (response.isSuccess) {
              this._snackBar.open(response.message, 'Success', {
                duration: 2500,
                horizontalPosition: this.horizontalPosition,
                verticalPosition: this.verticalPosition,
              });

              this.loadList();
            }
            else {
              this._snackBar.open(response.message, 'Error', {
                duration: 2500,
                horizontalPosition: this.horizontalPosition,
                verticalPosition: this.verticalPosition,
              });
            }
          }, error => {
            this._fuseProgressBarService.hide();
            this._snackBar.open("Network error has been occured. Please try again.", 'Error', {
              duration: 2500,
              horizontalPosition: this.horizontalPosition,
              verticalPosition: this.verticalPosition,
            });
          });
      }
      this.confirmDialogRef = null;
    });
  }

  viewProduct(subCategory: ProductSubCategoryModel)
  {

  }

  getCategories()
  {
    this._productService.getProductCategories().subscribe(response=>{
      this.categories = response;
    },error=>{

    });
  }

  categoryChanged(item:any)
  {
    this.loadList();
  }

  upload$: Observable<Upload> = EMPTY;
  precentage:any;
  onFileChange(event: any,item:ProductSubCategoryModel)
  {
    let fi = event.srcElement;
    const formData = new FormData();
    formData.set("id",item.id.toString());
    
    if(fi.files.length>0)
    {
        this._fuseProgressBarService.show();
        for (let index = 0; index < fi.files.length; index++) {
          
          formData.append('file', fi.files[index], fi.files[index].name);
        }
        this._productService.uploadSubProductCategoryImage(formData).subscribe(res=>
          {
            this.precentage =res;
            if(res.state=="DONE")
            {
              //item.isUploading=false;
              this._fuseProgressBarService.hide();
              this.loadList();
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

}
