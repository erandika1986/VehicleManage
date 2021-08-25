import { DecimalPipe, Location } from '@angular/common';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTable } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { DropDownModel } from 'app/models/common/drop-down.modal';
import { SalesOrderItemModel } from 'app/models/sales-order/sales.order.item.model';
import { SalesOrderMasterDataModel } from 'app/models/sales-order/sales.order.master.data.model';
import { SalesOrderModel } from 'app/models/sales-order/sales.order.model';
import { DropdownService } from 'app/services/common/dropdown.service';
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
    private _dropDownService:DropdownService,
    private _supplierService:SupplierService,
    private _route: ActivatedRoute,
    private decimalPipe: DecimalPipe,
    private _fuseProgressBarService: FuseProgressBarService,
    public _activateRoute: ActivatedRoute,
    private _snackBar: MatSnackBar,
    private _location: Location,
    private _formBuilder: FormBuilder,
    public _router: Router) {
      this.salesOrder = new SalesOrderModel();
     }

  ngOnInit(): void {
    this._activateRoute.params.subscribe(params => {
      this.salesOrderId = +params.id;
      this.pageType = this.salesOrderId === 0 ? 'new' : 'edit';

      this.createNewPOForm();
      this.getMasterData();
    });


    this._salesOrderService.onSalesOrderDetailChanged.subscribe(response=>{
      
      let sum:number=0;

      this.salesOrderStep2Form.getRawValue().items.forEach(element => {
        let total:number= +element.total;
        sum = sum + total;    
      });

       this.salesOrderStep3Form.get('subTotal').setValue(sum);
      let taxAmount = ((this.subTotal-this.discount)*this.taxRate)/100.00;
      this.salesOrderStep3Form.get('totalTaxAmout').setValue(taxAmount);
      this.calculateTotal();
      
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



  productCatgoryChanged(item:any,index:number)
  {
    this._fuseProgressBarService.show();
    this._dropDownService.getProductSubCategories(item)
      .subscribe(response=>{
        this._fuseProgressBarService.hide();
        this.salesOrder.items[index].subCategories = response;
      },error=>{
        this._fuseProgressBarService.hide();

      })
    
  }

  productSubCategoryChanged(item:any,index:number)
  {
    this._fuseProgressBarService.show();
    this._dropDownService.getProducts(item)
      .subscribe(response=>{
        this._fuseProgressBarService.hide();
        this.salesOrder.items[index].products = response;
      },error=>{
        this._fuseProgressBarService.hide();

      })
  }

  addNewItem() {

    const zeroPrice = this.decimalPipe.transform(
      0,
      "1.2-10"
    );
    
      const fg = this._formBuilder.group({
        id: new FormControl(0),
        selectedCategoryId:new FormControl(null,Validators.required), 
        selectedSubCategoryId:new FormControl(null,Validators.required), 
        productId:new FormControl(null,Validators.required), 
        description: new FormControl(''),
        qty: new FormControl(0,Validators.required),
        unitPrice: new FormControl(zeroPrice,Validators.required),
        total: new FormControl(zeroPrice,Validators.required)
    });

    fg.get("qty").valueChanges.subscribe(value=>{

      const tot = 
      this.decimalPipe.transform(value*fg.get("unitPrice").value,
      "1.2-2"
    );
        
      fg.get("total").setValue(tot);
      this._salesOrderService.onSalesOrderDetailChanged.next(true);
      
  });
  
  fg.get("unitPrice").valueChanges.subscribe(value=>{
      const tot = 
      this.decimalPipe.transform(value*fg.get("qty").value,
      "1.2-2"
    );
      fg.get("total").setValue(tot);
      this._salesOrderService.onSalesOrderDetailChanged.next(true);
  });

  if (this.isViewOnly) {
    fg.get("selectedCategoryId").disable();
    fg.get("selectedSubCategoryId").disable();
    fg.get("productId").disable();
    fg.get("qty").disable();
    fg.get("unitPrice").disable();
    fg.get("total").disable();
  }

  let salesOrderItem = new SalesOrderItemModel();
  salesOrderItem.categories = this.masterData.productCategories;

  if(!this.salesOrder.items)
  {
    this.salesOrder.items=[];
  }

  this.salesOrder.items.push(salesOrderItem);

  (this.salesOrderStep2Form.get('items') as FormArray).push(fg);

  this.table.renderRows();
}

  createSalesOrderNumber()
  {    this._fuseProgressBarService.show();
      this._salesOrderService.getSalesOrderNumber().subscribe(response=>{
        console.log(this.salesOrderStep1Form);
        
        this.salesOrder.orderNumber = response.number;
        //this.salesOrderStep1Form.get("orderNumber").setValue(response.number);
        this.salesOrderStep1Form.patchValue({'orderNumber':response.number});
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
        .subscribe(response=>{

          this.salesOrderStep2Form = this._formBuilder.group({
            items: this._formBuilder.array([]),
          });
      
          this.salesOrderStep3Form = this._formBuilder.group({
            subTotal: [{ value: response.subTotal, disabled: true },Validators.required],
            discount: [{ value: response.discount, disabled: this.isViewOnly },Validators.required],
            taxRate: [{ value: response.taxRate, disabled: this.isViewOnly },Validators.required],
            totalTaxAmount: [{ value: response.totalTaxAmount, disabled: true }],
            shippingCharge: [{ value: this.shippingCharge, disabled: this.isViewOnly }],
            totalAmount: [{ value: response.totalAmount, disabled: true }],
            remarks: [{ value: response.remarks, disabled: this.isViewOnly }]
          });

        },error=>{

        });

  }

  calculateTotal()
  {
    let total = (this.subTotal-this.discount+this.totalTaxAmout+this.shippingCharge);
    this.salesOrderStep3Form.get('total').setValue(total);
  }

  makeForm3Subsriber()
  {
       this.salesOrderStep3Form.get("discount").valueChanges.subscribe(value=>{
        this.calculateTotal();
      });

      this.salesOrderStep3Form.get("taxRate").valueChanges.subscribe(value=>{

        let taxAmount = ((this.subTotal-this.discount)*value)/100.00;
        this.salesOrderStep3Form.get('totalTaxAmout').setValue(taxAmount);
        this.calculateTotal();
      });

      this.salesOrderStep3Form.get("shippingCharge").valueChanges.subscribe(value=>{
        this.calculateTotal();
      }); 

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

  get ownerId()
  {
    return this.salesOrderStep1Form.get('ownerId').value;
  }

  get salesOrderNumber()
  {
    return this.salesOrderStep1Form.get('orderNumber').value;
  }

  get orderDate()
  {
    return this.salesOrderStep1Form.get('orderDate').value;
  }

  get deliverDate()
  {
    return this.salesOrderStep1Form.get('deliverDate').value;
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
