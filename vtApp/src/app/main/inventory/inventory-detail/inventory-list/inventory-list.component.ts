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
import { InventoryBasicDetailModel } from 'app/models/inventory/inventory.basic.detail.model';
import { InventoryService } from 'app/services/inventory/inventory.service';
import { InventoryFilter } from '../../../../models/inventory/inventory.filter.model';

@Component({
  selector: 'app-inventory-list',
  templateUrl: './inventory-list.component.html',
  styleUrls: ['./inventory-list.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class InventoryListComponent implements OnInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';
  
  dataSource = new MatTableDataSource([]);
  filter:InventoryFilter = new InventoryFilter();
  suppliers:DropDownModel[]=[];
  warehouses:DropDownModel[]=[];
  
  dialogRef: any;
  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;
  
  displayedColumns = ["buttons", "productImage","categoryName","subCategoryName","productName","supplierName","totalItemRecieved","qtyInHand","totalItemReturn"];

  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  @ViewChild('input') input: ElementRef;

  constructor(private _route: ActivatedRoute,
    private _fuseProgressBarService: FuseProgressBarService,
    public _router: Router,
    private _matDialog: MatDialog,
    private _snackBar: MatSnackBar,
    private _inventoryService:  InventoryService) { 
      this.filter.selectedSupplierId=0;
      this.filter.selectedWarehouseNameId =0;
    }

  ngOnInit(): void {
    this.getMasterData();
  }

  addNew() {
    //this._router.navigate(['inventory/purchase-order/list/' + 0 ]);
  }

  edit(item: InventoryBasicDetailModel) {

    //this._router.navigate(['inventory/purchase-order/list/' + item.id ]);
  }

  delete(item: InventoryBasicDetailModel) {

  }

  getMasterData()
  {
    this._fuseProgressBarService.show();
    this._inventoryService.getInventoryMasterData()
        .subscribe(response=>{
          this._fuseProgressBarService.hide();

          let firstItem = new DropDownModel();
          firstItem.id=0;
          firstItem.name="--All--";
          response.suppliers.unshift(firstItem);
          response.warehouses.unshift(firstItem);

          this.suppliers = response.suppliers;
          this.warehouses = response.warehouses;


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
    this._inventoryService.getProductInvetorySummary()
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
