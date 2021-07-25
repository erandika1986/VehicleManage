import { HttpEventType } from '@angular/common/http';
import { Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
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
import { POFilter } from 'app/models/po/po.filter.model';
import { PurchaseOrderSummary } from 'app/models/po/purchase.order.summary.model';
import { PoService } from 'app/services/po/po.service';

@Component({
  selector: 'po-list',
  templateUrl: './po-list.component.html',
  styleUrls: ['./po-list.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class PoListComponent implements OnInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  dataSource = new MatTableDataSource([]);

  filter:POFilter = new POFilter();
  suppliers:DropDownModel[]=[];
  warehouses:DropDownModel[]=[];
  statuses:DropDownModel[]=[];

  dialogRef: any;
  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;

  displayedColumns = ["buttons", "poNumber","status","supplierName","warehouseName","date","subTotal","discount","taxRate","totalTaxAmount","shippingCharges","total"];

  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  @ViewChild('input') input: ElementRef;
  
  constructor(    private _route: ActivatedRoute,
    private _fuseProgressBarService: FuseProgressBarService,
    public _router: Router,
    private _matDialog: MatDialog,
    private _snackBar: MatSnackBar,
    private _poService: PoService) { 
      this.filter.selectedStatusId =0;
      this.filter.selectedSupplierId=0;
      this.filter.selectedWarehouseNameId =0;
    }

  ngOnInit(): void {
    this.getMasterData();
  }

  addNew() {
    this._router.navigate(['inventory/purchase-order/list/' + 0 ]);
  }

  edit(item: PurchaseOrderSummary) {

    this._router.navigate(['inventory/purchase-order/list/' + item.id ]);
  }

  delete(item: PurchaseOrderSummary) {

  }

  downloadPercentage:number=0;
  isDownloading:boolean;
  downloadPurchasingOrderForm(item:PurchaseOrderSummary)
  {
    this._fuseProgressBarService.show();
    this.isDownloading=true;
    this._poService.downloadPurchasingOrderForm(item.id)
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
            a.download = item.poNumber+".pdf";
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

  getMasterData()
  {
    this._fuseProgressBarService.show();
    this._poService.getPurchaseOrderMasterData()
        .subscribe(response=>{
          this._fuseProgressBarService.hide();

          let firstItem = new DropDownModel();
          firstItem.id=0;
          firstItem.name="--All--";
          response.suppliers.unshift(firstItem);
          response.warehouses.unshift(firstItem);
          response.statuses.unshift(firstItem);

          this.suppliers = response.suppliers;
          this.warehouses = response.warehouses;
          this.statuses = response.statuses;

          this.loadAll();
        },error=>{
          this._fuseProgressBarService.hide();
        });
  }

  filterChanged()
  {
    this.loadAll();
  }


  loadAll() {
    this._fuseProgressBarService.show();
    this._poService.getAll(this.filter)
      .subscribe(response => {
        this._fuseProgressBarService.hide();
        console.log(response);

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



}
