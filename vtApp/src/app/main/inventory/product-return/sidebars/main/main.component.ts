import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ProductReturnService } from 'app/services/inventory/product-return.service';
import { Subject } from 'rxjs';
import { DropDownModel } from 'app/models/common/drop-down.modal';

@Component({
  selector: 'product-return-main-sidebar',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  private _unsubscribeAll: Subject<any>;

  productCategories:DropDownModel[]=[];
  productReturnStatus:DropDownModel[]=[];
  productReturnReasonCodes:DropDownModel[]=[];

  filterForm:FormGroup;
  constructor
  (
    private _productReturnService:ProductReturnService
  )
  {
    this._unsubscribeAll = new Subject();
  }

  ngOnInit(): void {
    this.getProductReturnMasterData();
    this.filterForm = this.createFilterForm();
  }

  createFilterForm(): FormGroup {
    return new FormGroup({
      productCategories:new FormControl(0),
      productReturnStatus:new FormControl(0),
      productReturnReasonCodes:new FormControl(0),

    });
  }

  dropdownFilterChanged()
  {
    this._productReturnService.onFilterChanged.next(this.filterForm.getRawValue());
  }

  getProductReturnMasterData()
  {
    this._productReturnService.getProductReturnMasterData().subscribe(response=>
    {
      this.productCategories = response.productCategories;
      this.productReturnStatus = response.productReturnStatus;
      this.productReturnReasonCodes = response.productReturnReasonCodes;
      
    });
  }


}
