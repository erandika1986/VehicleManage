import { HttpEventType } from '@angular/common/http';
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
import { ProductImageModel } from 'app/models/product/product.image.model';
import { ProductModel } from 'app/models/product/product.model';
import { ProductService } from 'app/services/product/product.service';
import { EMPTY, Observable, Subject } from 'rxjs';
import { ProductDetailComponent } from '../product-detail/product-detail.component';

@Component({
  selector: 'product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss'],
  animations   : fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class ProductListComponent implements OnInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  dialogRef: any;
  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;

  dataSource = new MatTableDataSource([]);
  displayedColumns = ['buttons', 'defaultImage', 'name','productCode','availableQty','minOrderQty','maxOrderQty', 'unitPrice', 'categoryName',"subCategoryName"];

  @ViewChild(MatPaginator, {static: true})
  paginator: MatPaginator;

  @ViewChild(MatSort, {static: true})
  sort: MatSort;

  @ViewChild('filter', {static: true})
  filter: ElementRef;

  categories:DropDownModel[]=[];
  selectedCategoryId:number=0;
  subCategories:DropDownModel[]=[];
  selectedSubCategoryId:number=0;

  // Private
  private _unsubscribeAll: Subject<any>;
  
  constructor(private _route: ActivatedRoute,
    private _productService: ProductService,
    private _fuseProgressBarService: FuseProgressBarService,
    private _matDialog: MatDialog,
    private _snackBar: MatSnackBar,
    public _router: Router) { 

    }

  ngOnInit(): void {
    this.loadList();
    this.getCategories();
    this._productService.onProductImageUploaded.subscribe(response=>{
      this.loadList();
    });
  }

  edit(item:ProductModel)
  {
    this.dialogRef = this._matDialog.open(ProductDetailComponent, {
      panelClass: 'product-form-dialog',
      data: {
        product: item,
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

  view(item:ProductModel)
  {
    this.dialogRef = this._matDialog.open(ProductDetailComponent, {
      panelClass: 'product-form-dialog',
      data: {
        product: item,
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

  addNew()
  {
    let product: ProductModel = new ProductModel();
    product.id = 0;
    //product.productCategoryId=0;
    //product.productSubCategoryId=0;
    //product.supplierId =0;
    product.name = "";
    product.description="";
    product.isActive = true;
    

    this.dialogRef = this._matDialog.open(ProductDetailComponent, {
      panelClass: 'product-form-dialog',
      data: {
        product: product,
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
    this._productService.getAllProducts(this.selectedSubCategoryId)
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

  save(product: ProductModel) {
    this._fuseProgressBarService.show();
    this._productService.saveProduct(product)
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

  delete(product: ProductModel) {
    this.confirmDialogRef = this._matDialog.open(FuseConfirmDialogComponent, {
      disableClose: false
    });

    this.confirmDialogRef.componentInstance.confirmMessage = 'Are you sure you want to delete this record?';

    this.confirmDialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._fuseProgressBarService.show();
        this._productService. deleteProductSubCategory(product.id)
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

  getCategories()
  {
    this._productService.getProductCategories().subscribe(response=>{
      this.categories = response;
      this.getSubCategories();
    },error=>{

    });
  }

  productCategoryChanged(item:any)
  {
    this.getSubCategories()
  }

  getSubCategories()
  {
    this._productService.getProductSubCategories(this.selectedCategoryId)
      .subscribe(response=>{
        this.subCategories = response;
        this.selectedSubCategoryId=0;
        this.loadList();

      },error=>{

      })
  }

  filterChanged(item:any)
  {
    this.loadList();
  }

  upload$: Observable<Upload> = EMPTY;
  precentage:any;
  onFileChange(event: any,item:ProductModel)
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
        this._productService.uploadProductImage(formData).subscribe(res=>
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


  downloadPercentage:number=0;
  isDownloading:boolean;
  downloadFile(item:ProductImageModel)
  {
    this._fuseProgressBarService.show();
    this.isDownloading=true;
    this._productService.downloadProductImage(item.id)
      .subscribe(response=>{

        console.log(response);
        
        if (response.type === HttpEventType.DownloadProgress) {
          this.downloadPercentage = Math.round(100 * response.loaded / response.total);
        }
        
        if (response.type === HttpEventType.Response) {
          if(response.status == 204)
          {
            this.isDownloading=false;
            this.downloadPercentage=0;
            this._fuseProgressBarService.hide();
          }
          else
          {
            const objectUrl: string = URL.createObjectURL(response.body);
            const a: HTMLAnchorElement = document.createElement('a') as HTMLAnchorElement;
    
            a.href = objectUrl;
            a.download = item.imageName;
            document.body.appendChild(a);
            a.click();
    
            document.body.removeChild(a);
            URL.revokeObjectURL(objectUrl);
            this.isDownloading=false;
            this.downloadPercentage=0;
            this._fuseProgressBarService.hide();
          }

        }




      },error=>{
        this._fuseProgressBarService.hide();
        this.isDownloading=false;
        this.downloadPercentage=0;
      });
  }
}
