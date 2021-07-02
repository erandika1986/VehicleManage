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
import { Upload } from 'app/models/common/upload';
import { ProductCategoryModel } from 'app/models/product/product.category.model';
import { ProductService } from 'app/services/product/product.service';
import { EMPTY, Observable } from 'rxjs';
import { ProductCategoryDetailComponent } from '../product-category-detail/product-category-detail.component';

@Component({
  selector: 'product-category-list',
  templateUrl: './product-category-list.component.html',
  styleUrls: ['./product-category-list.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class ProductCategoryListComponent implements OnInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  dataSource = new MatTableDataSource([]);


  dialogRef: any;
  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;

  displayedColumns = ["buttons", "picture", "name","description"];

  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  @ViewChild('input') input: ElementRef;
  
  constructor(private _route: ActivatedRoute,
    private _productService: ProductService,
    private _fuseProgressBarService: FuseProgressBarService,
    private _matDialog: MatDialog,
    private _snackBar: MatSnackBar,
    public _router: Router) { }

  ngOnInit(): void {
    this.loadProductCategories();
  }

  editProductCategory(category: ProductCategoryModel): void {
    this.dialogRef = this._matDialog.open(ProductCategoryDetailComponent, {
      panelClass: 'category-form-dialog',
      data: {
        category: category,
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
            this.saveProductCategory(formData.getRawValue());


            break;
          /**
           * Delete
           */
          case 'delete':

            this.deleteProductCategory(formData.getRawValue());

            break;
        }
      });

  }

  view(item:ProductCategoryModel)
  {
    console.log(item);
    
    this.dialogRef = this._matDialog.open(ProductCategoryDetailComponent, {
      panelClass: 'category-form-dialog',
      data: {
        category: item,
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

  addNewProductCategory(): void {

    let category: ProductCategoryModel = new ProductCategoryModel();
    category.id = 0;
    category.name = "";
    category.description="";
    category.picture="";
    category.isActive = true;
    

    this.dialogRef = this._matDialog.open(ProductCategoryDetailComponent, {
      panelClass: 'category-form-dialog',
      data: {
        category: category,
        action: "add"
      }
    });

    this.dialogRef.afterClosed()
      .subscribe(response => {
        if (!response) {
          return;
        }

        const formData: FormGroup = response;
        this.saveProductCategory(formData.getRawValue());

      });
  }


  loadProductCategories() {
    this._fuseProgressBarService.show();
    this._productService.getAllProductCategories()
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


  saveProductCategory(vm: ProductCategoryModel) {
    this._fuseProgressBarService.show();
    this._productService.saveProductCategory(vm)
      .subscribe(response => {

        this._fuseProgressBarService.hide();
        if (response.isSuccess) {
          this._snackBar.open(response.message, 'Success', {
            duration: 2500,
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition,
          });

          this.loadProductCategories();;
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


  deleteProductCategory(category: ProductCategoryModel) {
    this.confirmDialogRef = this._matDialog.open(FuseConfirmDialogComponent, {
      disableClose: false
    });

    this.confirmDialogRef.componentInstance.confirmMessage = 'Are you sure you want to delete this record?';

    this.confirmDialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._fuseProgressBarService.show();
        this._productService. deleteProductCategory(category.id)
          .subscribe(response => {

            this._fuseProgressBarService.hide();
            if (response.isSuccess) {
              this._snackBar.open(response.message, 'Success', {
                duration: 2500,
                horizontalPosition: this.horizontalPosition,
                verticalPosition: this.verticalPosition,
              });

              this.loadProductCategories();
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

  viewProductSubCategory(category: ProductCategoryModel)
  {
    this._productService.selectedCategoryId = category.id;
    this._productService.selectedSubCategoryId=0;
    this._router.navigate(['/admin/product-sub-category/list']);
  }

  upload$: Observable<Upload> = EMPTY;
  precentage:any;
  onFileChange(event: any,item:ProductCategoryModel)
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
        this._productService.uploadProductCategoryImage(formData).subscribe(res=>
          {
            this.precentage =res;
            if(res.state=="DONE")
            {
              //item.isUploading=false;
              this._fuseProgressBarService.hide();
              this.loadProductCategories();
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
