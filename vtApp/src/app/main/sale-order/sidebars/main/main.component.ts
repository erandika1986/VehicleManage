import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { DropDownModel } from 'app/models/common/drop-down.modal';
import { SalesOrderService } from 'app/services/sales-order/sales-order.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'sale-sorder-main-sidebar',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent  implements OnInit, OnDestroy {

  user: any;
  filterBy: string;
  status:DropDownModel[]=[];
  routes:DropDownModel[]=[];
  customers:DropDownModel[]=[];
  salesReps:DropDownModel[]=[];
  filterForm:FormGroup;

    // Private
    private _unsubscribeAll: Subject<any>;

  constructor(private _salesOrderService: SalesOrderService) {
    this._unsubscribeAll = new Subject();
  }

  ngOnInit(): void {
    this.getAllMasterData();
    this.filterForm = this.createFilterForm();
  }

  ngOnDestroy(): void
  {
      // Unsubscribe from all subscriptions
      this._unsubscribeAll.next();
      this._unsubscribeAll.complete();
  }

  getAllMasterData()
  {
    this._salesOrderService.getSalesOrderMasterData()
      .subscribe(response=>{
        this._salesOrderService.onMasterDataRecieved.next(response);

        let firstItem = new DropDownModel();
        firstItem.id=0;
        firstItem.name="--All--";

        response.routes.unshift(firstItem);
        response.customers.unshift(firstItem);
        response.salesPerson.unshift(firstItem);
        response.statuses.unshift(firstItem);

        this.status= response.statuses;
        this.routes= response.routes;
        this.salesReps= response.salesPerson;
        this.customers= response.customers;


      },error=>{

      });
  }

  createFilterForm(): FormGroup {
    return new FormGroup({
   
      selectedStatus: new FormControl(0),
      selectedRouteId:new FormControl(0),
      selectedCustomerId: new FormControl(0),
      selectedSalesPersonId: new FormControl(0)
    });
  }

  routeChanged(item:any)
  {
    this._salesOrderService.getCustomersByRouteId(item)
    .subscribe(response=>{
      this._salesOrderService.onMasterDataRecieved.next(response);

      let firstItem = new DropDownModel();
      firstItem.id=0;
      firstItem.name="--All--";

      response.unshift(firstItem);
      this.customers= response;
      this.filterForm.get("selectedCustomerId").patchValue(0);

      this.dropdownFilterChanged();

    },error=>{

    });
  }

  dropdownFilterChanged()
  {
    this._salesOrderService.onFilterChanged.next(this.filterForm.getRawValue());
  }

}
