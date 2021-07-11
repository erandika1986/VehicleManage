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


  dialogRef: any;
  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;

  displayedColumns = ["buttons", "poNumber","supplierName","warehouseName","subTotal","discount","taxRate","totalTaxAmount","shippingCharges","total","status"];

  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  @ViewChild('input') input: ElementRef;
  
  constructor(    private _route: ActivatedRoute,
    private _fuseProgressBarService: FuseProgressBarService,
    public _router: Router,
    private _matDialog: MatDialog,
    private _snackBar: MatSnackBar,
    private _poService: PoService) { }

  ngOnInit(): void {

    this.loadAll();
  }

  addNew() {
    this._router.navigate(['inventory/purchase-order/list/' + 0 ]);
  }

  edit(item: PurchaseOrderSummary) {
    this._router.navigate(['inventory/purchase-order/list/' + item.id ]);
  }

  delete(item: PurchaseOrderSummary) {

  }



  loadAll() {
    this._fuseProgressBarService.show();
    this._poService.getAll()
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
