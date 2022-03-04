import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { FuseSidebarService } from '@fuse/components/sidebar/sidebar.service';
import { SalesOrderMasterDataModel } from 'app/models/sales-order/sales.order.master.data.model';
import { SalesOrderModel } from 'app/models/sales-order/sales.order.model';
import { SalesOrderService } from 'app/services/sales-order/sales-order.service';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-sale-orders',
  templateUrl: './sale-orders.component.html',
  styleUrls: ['./sale-orders.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations   : fuseAnimations
})
export class SaleOrdersComponent implements OnInit, OnDestroy {

  dialogRef: any;
  hasSelectedContacts: boolean;
  searchInput: FormControl;

  private _unsubscribeAll: Subject<any>;
  masterData:SalesOrderMasterDataModel;

  constructor(private _salesOrderService: SalesOrderService,
    private _fuseSidebarService: FuseSidebarService,
    private _fuseProgressBarService: FuseProgressBarService,
    public _router: Router,
    private _matDialog: MatDialog) {
      this.searchInput = new FormControl('');
      // Set the private defaults
      this._unsubscribeAll = new Subject();
     }

  ngOnInit(): void {

    this.searchInput.valueChanges
    .pipe(
        takeUntil(this._unsubscribeAll),
        debounceTime(300),
        distinctUntilChanged()
    )
    .subscribe(searchText => {
        this._salesOrderService.onSearchTextChanged.next(searchText);
    }); 

    this._salesOrderService.onMasterDataRecieved.subscribe(response=>{

        this.masterData =response;
    });
  }

        /**
     * On destroy
     */
    ngOnDestroy(): void
    {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    newSalesOrder(): void
    {
        this._salesOrderService.onClickViewOnly.next(false);
        this._router.navigate(['sale-order/list/' + 0 ]);
/*         this.dialogRef = this._matDialog.open(UserDetailComponent, {
            panelClass: 'user-form-dialog',
            data      : {
                masterData:this.masterData,
                action: 'new'
            }
        });

        this.dialogRef.afterClosed()
            .subscribe((response: FormGroup) => {
                if ( !response )
                {
                    return;
                }
                this.saveUser(response.getRawValue())
            }); */
    }

        /**
     * Toggle the sidebar
     *
     * @param name
     */
    toggleSidebar(name): void
    {
        this._fuseSidebarService.getSidebar(name).toggleOpen();
    }
    

    saveUser(salesOrder:SalesOrderModel)
    {
        this._salesOrderService.onNewSalesOrderAdded.next(salesOrder);
    }

    addNew()
    {
        this._fuseProgressBarService.show();
        this._salesOrderService.createNewSalesOrder()
            .subscribe(response=>{
                this._fuseProgressBarService.hide()
                this._salesOrderService.onClickViewOnly.next(false);
                this._router.navigate(['sale-order/list/' + response ]);
            });
    }
}
