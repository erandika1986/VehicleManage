import { DecimalPipe, Location } from '@angular/common';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { MatTable } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { DropDownModel } from 'app/models/common/drop-down.modal';
import { SalesOrderItemModel } from 'app/models/sales-order/sales.order.item.model';
import { SalesOrderMasterDataModel } from 'app/models/sales-order/sales.order.master.data.model';
import { SalesOrderModel } from 'app/models/sales-order/sales.order.model';
import { SalesOrderStep1Model } from 'app/models/sales-order/sales.order.step1.model';
import { SalesOrderStep3Model } from 'app/models/sales-order/sales.order.step3.model';
import { DropdownService } from 'app/services/common/dropdown.service';
import { CustomerService } from 'app/services/customer/customer.service';
import { SalesOrderService } from 'app/services/sales-order/sales-order.service';
import { SupplierService } from 'app/services/supplier/supplier.service';
import { DaterangepickerDirective } from 'ngx-daterangepicker-material';
import { BehaviorSubject, Subject } from 'rxjs';
import { ProductAvailabiltyComponent } from '../product-availabilty/product-availabilty.component';

@Component({
  selector: 'app-sale-order-detail',
  templateUrl: './sale-order-detail.component.html',
  styleUrls: ['./sale-order-detail.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class SaleOrderDetailComponent implements OnInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

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

  dialogRef: any;

  dataSource = new BehaviorSubject<AbstractControl[]>([]);
  rows: FormArray = this._formBuilder.array([]);
  displayedColumns = ['action','categoryName','subCategoryName','productName','qty','unitPrice','total'];
  

  @ViewChild(DaterangepickerDirective, { static: true })
  pickerDirective: DaterangepickerDirective;

  // Private
  private _unsubscribeAll: Subject<any>;

  @ViewChild(MatTable) table: MatTable<any>;

  constructor(private _salesOrderService: SalesOrderService,
    private _customerService:CustomerService,
    private _dropDownService:DropdownService,
    private _matDialog: MatDialog,
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
      this.createPOForm();
      this.getMasterData();

    });


    this._salesOrderService.onSalesOrderChanged.subscribe(response=>{
      this.getSalesOrderDetails();
/*       let sum:number=0;

      this.salesOrderStep2Form.getRawValue().items.forEach(element => {
        let total:number= +element.total;
        sum = sum + total;    
      });

       this.salesOrderStep3Form.get('subTotal').setValue(sum);
      let taxAmount = ((this.subTotal-this.discount)*this.taxRate)/100.00;
      this.salesOrderStep3Form.get('totalTaxAmount').setValue(taxAmount);
      this.calculateTotal(); */
      
    });
  }

  createPOForm()
  {
    this.salesOrderStep1Form = this._formBuilder.group({
      id: [0],
      orderNumber: [{ value: '', disabled: true }, Validators.required],
      orderDate:[{ value: new Date(), disabled: this.isViewOnly },Validators.required],
      deliverDate:[{ value: null, disabled: this.isViewOnly }],
      ownerId: [{ value: null, disabled: this.isViewOnly },Validators.required],
      ownerAddress:[''],
      routeId:[0],
      status: [{ value: null, disabled: this.isViewOnly },Validators.required],
    });

    this.salesOrderStep2Form = this._formBuilder.group({
      items: this._formBuilder.array([]),
    });

    this.salesOrderStep3Form = this._formBuilder.group({
      subTotal: [{ value: 0, disabled: true },Validators.required],
      discount: [{ value: 0, disabled: this.isViewOnly },Validators.required],
      taxRate: [{ value: 0, disabled: this.isViewOnly },Validators.required],
      totalTaxAmount: [{ value: 0, disabled: true }],
      shippingCharge: [{ value: 0, disabled: this.isViewOnly }],
      totalAmount: [{ value: 0, disabled: true }],
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

  onCustomerChanged(item:any)
  {

    this.getCustomerDetail(item.value);
  }

  getCustomerDetail(id:number)
  {
    this._customerService.getCustomerById(id)
    .subscribe(response=>{

      this.salesOrderStep1Form.get("routeId").setValue(response.routeId);
      this.salesOrderStep1Form.get("ownerAddress").setValue(response.address);
    });
  }

  addNewItem() {
    
    this.dialogRef = this._matDialog.open(ProductAvailabiltyComponent, {
      panelClass: 'product-search-form-dialog',
      data: {
        salesOrderId:this.salesOrderId,
        categoryId:0,
        subCategoryId:0,
        productId :0
      }
    });
  }

  deleteOrderItem(item:any,index:number)
  {
    this.dialogRef = this._matDialog.open(ProductAvailabiltyComponent, {
      panelClass: 'product-search-form-dialog',
      data: {
        salesOrderId:this.salesOrderId,
        categoryId:item.get('selectedCategoryId').value,
        subCategoryId:item.get('selectedSubCategoryId').value,
        productId :item.get('productId').value
      }
    });
  }

  editOrderItem(item:any,index:number)
  {
    console.log(item);
    
    this.dialogRef = this._matDialog.open(ProductAvailabiltyComponent, {
      panelClass: 'product-search-form-dialog',
      data: {
        salesOrderId:this.salesOrderId,
        categoryId:item.get('selectedCategoryId').value,
        subCategoryId:item.get('selectedSubCategoryId').value,
        productId :item.get('productId').value
      }
    });
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
    },error=>{

    });
  }

  getSalesOrderDetails()
  {
    this._fuseProgressBarService.show();
    this._salesOrderService.getSalesOrderById(this.salesOrderId)
        .subscribe(response=>{

          this._fuseProgressBarService.hide();
          this.salesOrderStep1Form = this._formBuilder.group({
            id: [response.id],
            orderNumber: [{ value: response.orderNumber, disabled: true },Validators.required],
            orderDate:[{ value: response.orderDate, disabled: this.isViewOnly },Validators.required],
            deliverDate:[{ value: response.deliverDate, disabled: this.isViewOnly }],
            ownerId: [{ value: response.ownerId, disabled: this.isViewOnly },Validators.required],
            ownerAddress:[''],
            routeId:[response.routeId],
            status: [{ value: response.status, disabled: this.isViewOnly }],
          });

          const cf = response.items.map((value, index) => { return SalesOrderItemModel.asFormGroup(value, true,this._salesOrderService) });
          const fArray = new FormArray(cf);
          this.salesOrderStep2Form.setControl('items', fArray);

          this.updateView();

          this.salesOrderStep3Form = this._formBuilder.group({
            subTotal: [{ value: response.subTotal, disabled: true },Validators.required],
            discount: [{ value: response.discount, disabled: this.isViewOnly },Validators.required],
            taxRate: [{ value: response.taxRate, disabled: this.isViewOnly },Validators.required],
            totalTaxAmount: [{ value: response.totalTaxAmount, disabled: true }],
            shippingCharge: [{ value: response.shippingCharge, disabled: this.isViewOnly }],
            totalAmount: [{ value: response.totalAmount, disabled: true }],
            remarks: [{ value: response.remarks, disabled: this.isViewOnly }]
          });

/*           let taxAmount = ((this.subTotal-this.discount)*this.taxRate)/100.00;
          this.salesOrderStep3Form.get('totalTaxAmount').setValue(taxAmount); */
          //this.calculateTotal();

          this.makeForm3Subsriber();

          if(this.ownerId>0)
          {
            this.getCustomerDetail(this.ownerId);
          }


        },error=>{
          this._fuseProgressBarService.hide();
        });

  }

  updateView() {
    this.dataSource.next(this.items.controls);
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

  calculateTotal()
  {
    let total = (this.subTotal-this.discount+this.totalTaxAmount+this.shippingCharge);
    this.salesOrderStep3Form.get('totalAmount').setValue(total);
  }

  saveAndExitOrderStep1(needExit: boolean): void {
    let salesOrder = new SalesOrderStep1Model();
    salesOrder.id = this.id;
    salesOrder.orderDate = this.orderDate;
    let orderDateObject = new Date(this.orderDate);
    salesOrder.orderDateYear = orderDateObject.getFullYear();
    salesOrder.orderDateMonth = orderDateObject.getMonth()+1;
    salesOrder.orderDateDay = orderDateObject.getDate();
    salesOrder.orderDateHour = orderDateObject.getHours();
    salesOrder.orderDateMin = orderDateObject.getMinutes();

    salesOrder.deliverDate = this.deliverDate;
    if(this.deliverDate!=null)
    {
      let deliverDateObject = new Date(this.deliverDate);
      salesOrder.deliverDateYear = deliverDateObject.getFullYear();
      salesOrder.deliverDateMonth = deliverDateObject.getMonth()+1;
      salesOrder.deliverDateDay = deliverDateObject.getDate();
      salesOrder.deliverDateHour = deliverDateObject.getHours();
      salesOrder.deliverDateMin = deliverDateObject.getMinutes();
    }
    salesOrder.ownerId = this.ownerId;
    salesOrder.status = this.salesOrderStep1Form.get('status').value;;

    this._fuseProgressBarService.show();
    this._salesOrderService.saveSalesOrderStep1(salesOrder)
      .subscribe(response=>{
        this._fuseProgressBarService.hide();
        this._snackBar.open(response.message, response.isSuccess? 'Success':'Error', {
          duration: 2500,
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
        });
        this._location.back()
      },error=>{
        this._fuseProgressBarService.hide();
        this._snackBar.open("Error has been occured in client application.", 'Error', {
          duration: 2500,
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
        });
      });
  }

  saveAndExitOrderStep3(needExit: boolean): void {
    let salesOrder = new SalesOrderStep3Model();
    salesOrder.id = this.id;
    salesOrder.subTotal = this.subTotal;
    salesOrder.discount = this.discount;
    salesOrder.taxRate = this.taxRate;
    salesOrder.totalTaxAmount = this.totalTaxAmount;
    salesOrder.shippingCharge = this.shippingCharge;
    salesOrder.totalAmount = this.totalAmount;
    salesOrder.remarks = this.remarks;

    this._fuseProgressBarService.show();
    this._salesOrderService.saveSalesOrderStep3(salesOrder)
      .subscribe(response=>{
        this._fuseProgressBarService.hide();
        this._snackBar.open(response.message, response.isSuccess? 'Success':'Error', {
          duration: 2500,
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
        });
        this._location.back()
      },error=>{
        this._fuseProgressBarService.hide();
        this._snackBar.open("Error has been occured in client application.", 'Error', {
          duration: 2500,
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
        });
      });
  }

  saveAndSubmit()
  {
    let salesOrder = new SalesOrderModel();
    salesOrder.id= this.id;
    salesOrder.orderNumber = this.salesOrderNumber;

    salesOrder.orderDate = this.orderDate;
    let orderDateObject = new Date(this.orderDate);
    salesOrder.orderDateYear = orderDateObject.getFullYear();
    salesOrder.orderDateMonth = orderDateObject.getMonth()+1;
    salesOrder.orderDateDay = orderDateObject.getDate();
    salesOrder.orderDateHour = orderDateObject.getHours();
    salesOrder.orderDateMin = orderDateObject.getMinutes();

    salesOrder.deliverDate = this.deliverDate;
    if(this.deliverDate!=null)
    {
      let deliverDateObject = new Date(this.deliverDate);
      salesOrder.deliverDateYear = deliverDateObject.getFullYear();
      salesOrder.deliverDateMonth = deliverDateObject.getMonth()+1;
      salesOrder.deliverDateDay = deliverDateObject.getDate();
      salesOrder.deliverDateHour = deliverDateObject.getHours();
      salesOrder.deliverDateMin = deliverDateObject.getMinutes();
    }

    salesOrder.ownerId = this.ownerId;
    salesOrder.routeId = this.routeId;
    salesOrder.status = this.selectedStatus;
    salesOrder.subTotal = this.subTotal;
    salesOrder.discount = this.discount;
    salesOrder.taxRate = this.taxRate;
    salesOrder.totalAmount = this.totalTaxAmount;
    salesOrder.shippingCharge = this.shippingCharge;
    salesOrder.totalAmount = this.totalAmount;
    salesOrder.isActive = true;
    salesOrder.remarks = this.remarks;

    this._fuseProgressBarService.show();
    this._salesOrderService.saveSalesOrder(salesOrder)
      .subscribe(response=>{

        this._fuseProgressBarService.hide();
        this._snackBar.open(response.message, response.isSuccess? 'Success':'Error', {
          duration: 2500,
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
        });
        this._location.back()

      },error=>{
        this._fuseProgressBarService.hide();
        this._snackBar.open("Error has been occured in client application.", 'Error', {
          duration: 2500,
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
        });
      });
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

  get ownerAddress()
  {
    return this.salesOrderStep1Form.get('ownerAddress').value;
  }

  get salesOrderNumber()
  {
    return this.salesOrderStep1Form.get('orderNumber').value;
  }

  get orderDate():Date
  {
    return this.salesOrderStep1Form.get('orderDate').value;
  }

  get deliverDate():Date
  {
    return this.salesOrderStep1Form.get('deliverDate').value;
  }

  get customerName()
  {
    for (let index = 0; index < this.customers.length; index++) {
      if(this.customers[index].id==this.ownerId)
      {
        return this.customers[index].name;
      }
      
    }
    return "";
  }

  get routeId()
  {
    return this.salesOrderStep1Form.get('routeId').value;
  }

  get routeName()
  {
    if(this.masterData && this.masterData.routes)
    {
      for (let index = 0; index < this.masterData.routes.length; index++) {
        if(this.masterData.routes[index].id==this.routeId)
        {
          return this.masterData.routes[index].name;
        }      
      }
    }

    return "";
  }

  get selectedStatus()
  {
    return this.salesOrderStep1Form.get('status').value;
  }

  get selectedStatusName()
  {
    if(this.masterData && this.masterData.statuses)
    {
      for (let index = 0; index < this.masterData.statuses.length; index++) {
        if(this.masterData.statuses[index].id==this.selectedStatus)
        {
          return this.masterData.statuses[index].name;
        }      
      }
    }

    return "";
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

  get totalTaxAmount()
  {
    return this.salesOrderStep3Form.get("totalTaxAmount").value;
  }

  get shippingCharge()
  {
    return this.salesOrderStep3Form.get("shippingCharge").value;
  }

  get totalAmount()
  {
    return this.salesOrderStep3Form.get("totalAmount").value;
  }

  get remarks()
  {
    return this.salesOrderStep3Form.get("remarks").value;
  }

}
