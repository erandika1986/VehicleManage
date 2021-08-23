import { DecimalPipe, Location } from '@angular/common';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTable } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { DropDownModel } from 'app/models/common/drop-down.modal';
import { SalesOrderMasterDataModel } from 'app/models/sales-order/sales.order.master.data.model';
import { SalesOrderModel } from 'app/models/sales-order/sales.order.model';
import { SalesOrderService } from 'app/services/sales-order/sales-order.service';
import { SupplierService } from 'app/services/supplier/supplier.service';
import { DaterangepickerDirective } from 'ngx-daterangepicker-material';
import { BehaviorSubject, Subject } from 'rxjs';

@Component({
  selector: 'app-sale-order-detail',
  templateUrl: './sale-order-detail.component.html',
  styleUrls: ['./sale-order-detail.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class SaleOrderDetailComponent implements OnInit {

  masterData: SalesOrderMasterDataModel = new SalesOrderMasterDataModel();
  salesOrder:SalesOrderModel;


  pageType: string;
  title: string;
  salesOrderId:number;
  salesOrderStep1Form: FormGroup;
  salesOrderStep2Form: FormGroup;
  salesOrderStep3Form: FormGroup;

  customers:DropDownModel[]=[];
  status:DropDownModel[]=[];

  permissionId: number;
  isViewOnly: boolean = false;

  isExistingRecord: boolean = false;
  previouslySelectedTab: number = 0;

  dataSource = new BehaviorSubject<AbstractControl[]>([]);
  rows: FormArray = this._formBuilder.array([]);
  displayedColumns = ['action','selectedCategoryId','selectedSubCategoryId','productId','qty','unitPrice','total'];
  

  @ViewChild(DaterangepickerDirective, { static: true })
  pickerDirective: DaterangepickerDirective;

  // Private
  private _unsubscribeAll: Subject<any>;

  @ViewChild(MatTable) table: MatTable<any>;

  constructor(private _salesOrderService: SalesOrderService,
    private _supplierService:SupplierService,
    private _route: ActivatedRoute,
    private decimalPipe: DecimalPipe,
    private _fuseProgressBarService: FuseProgressBarService,
    public _activateRoute: ActivatedRoute,
    private _snackBar: MatSnackBar,
    private _location: Location,
    private _formBuilder: FormBuilder,
    public _router: Router) { }

  ngOnInit(): void {
    this._activateRoute.params.subscribe(params => {
      this.salesOrderId = +params.id;
      this.pageType = this.salesOrderId === 0 ? 'new' : 'edit';

      this.createNewPOForm();
      this.getMasterData();
    });


    this._salesOrderService.onSalesOrderChanged.subscribe(response=>{
      
      let sum:number=0;

      this.salesOrderStep2Form.getRawValue().items.forEach(element => {
        let total:number= +element.total;
        sum = sum + total;    
      });

/*       this.salesOrderStep3Form.get('subTotal').setValue(sum);
      let taxAmount = ((this.subTotal-this.discount)*this.taxRate)/100.00;
      this.salesOrderStep3Form.get('totalTaxAmout').setValue(taxAmount);
      this.calculateTotal(); */
      
    });
  }

  createNewPOForm()
  {
    this.salesOrderStep1Form = this._formBuilder.group({
      id: [0],
      orderNumber: [{ value: '', disabled: true }, Validators.required],
      orderDate:[{ value: new Date(), disabled: this.isViewOnly },Validators.required],
      deliverDate:[{ value: null, disabled: this.isViewOnly }],
      ownerId: [{ value: null, disabled: this.isViewOnly },Validators.required],
      status: [{ value: null, disabled: this.isViewOnly },Validators.required],
    });

    this.salesOrderStep2Form = this._formBuilder.group({
      items: this._formBuilder.array([]),
    });

    this.salesOrderStep3Form = this._formBuilder.group({
      subTotal: [{ value: 0, disabled: true },Validators.required],
      discount: [{ value: 0, disabled: this.isViewOnly },Validators.required],
      taxRate: [{ value: 0, disabled: this.isViewOnly },Validators.required],
      totalTaxAmout: [{ value: 0, disabled: true }],
      shippingCharge: [{ value: 0, disabled: this.isViewOnly }],
      total: [{ value: 0, disabled: true }],
      remarks: [{ value: '', disabled: this.isViewOnly }]
    });

    this.makeForm3Subsriber();
  }

  createSalesOrderNumber()
  {    this._fuseProgressBarService.show();
      this._salesOrderService.getSalesOrderNumber().subscribe(response=>{
        this.salesOrder.orderNumber = response.number;
        this.salesOrderStep1Form.patchValue({'poNumber':response.number});
        this._fuseProgressBarService.hide();
      },error=>{
        this._fuseProgressBarService.hide();
      });
  }

  getMasterData()
  {
    this._salesOrderService.getSalesOrderMasterData().subscribe(response=>{
        this.masterData= response;
        this.customers = this.masterData.customers;
        this.status = this.masterData.statuses;

        if (this.pageType === 'edit') {
          this.title = 'Edit Sales Order';
          this.getSalesOrderDetails();
        }
        else {
          this.title = 'New Sales Order';
          this.createSalesOrderNumber();
        }

    },error=>{

    });
  }

  getSalesOrderDetails()
  {
    this._salesOrderService.getSalesOrderById(this.salesOrderId)

  }

  makeForm3Subsriber()
  {
/*       this.poStep3Form.get("discount").valueChanges.subscribe(value=>{
        this.calculateTotal();
      });

      this.poStep3Form.get("taxRate").valueChanges.subscribe(value=>{

        let taxAmount = ((this.subTotal-this.discount)*value)/100.00;
        this.poStep3Form.get('totalTaxAmout').setValue(taxAmount);
        this.calculateTotal();
      });

      this.poStep3Form.get("shippingCharge").valueChanges.subscribe(value=>{
        this.calculateTotal();
      }); */

  }

  saveOrder(needExit: boolean): void {
  }

  goToBack()
  {
    this._location.back();
  }

  get id()
  {
    return this.salesOrderStep1Form.get('id').value;
  }

  get selectedCustomerId()
  {
    return this.salesOrderStep1Form.get('selectedSupplierId').value;
  }

  get salesOrderNumber()
  {
    return this.salesOrderStep1Form.get('poNumber').value;
  }

  get date()
  {
    return this.salesOrderStep1Form.get('date').value;
  }

  get items(): FormArray {
    return this.salesOrderStep2Form.get('items') as FormArray;
  }

  get subTotal()
  {
    return this.salesOrderStep3Form.get("subTotal").value;
  }

  get discount()
  {
    return this.salesOrderStep3Form.get("discount").value;
  }

  get taxRate()
  {
    return this.salesOrderStep3Form.get("taxRate").value;
  }

  get totalTaxAmout()
  {
    return this.salesOrderStep3Form.get("totalTaxAmout").value;
  }

  get shippingCharge()
  {
    return this.salesOrderStep3Form.get("shippingCharge").value;
  }

  get totalAmount()
  {
    return this.salesOrderStep3Form.get("total").value;
  }

}
