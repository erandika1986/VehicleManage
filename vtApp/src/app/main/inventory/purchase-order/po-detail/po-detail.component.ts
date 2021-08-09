import { DecimalPipe, Location } from '@angular/common';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { MatTable } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { DropDownModel } from 'app/models/common/drop-down.modal';
import { PurchaseOrderItem } from 'app/models/po/purchase.order.item.model';
import { PurchaseOrderMasterData } from 'app/models/po/purchase.order.master.data.model';
import { PurchaseOrder } from 'app/models/po/purchase.order.model';
import { SupplierModel } from 'app/models/supplier/supplier.model';
import { WarehouseModel } from 'app/models/warehouse/warehouse.model';
import { PoService } from 'app/services/po/po.service';
import { SupplierService } from 'app/services/supplier/supplier.service';
import { WarehouseService } from 'app/services/warehouse/warehouse.service';
import { DaterangepickerDirective } from 'ngx-daterangepicker-material';
import { BehaviorSubject, Subject } from 'rxjs';

@Component({
  selector: 'app-po-detail',
  templateUrl: './po-detail.component.html',
  styleUrls: ['./po-detail.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class PoDetailComponent implements OnInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';
  
  masterData: PurchaseOrderMasterData = new PurchaseOrderMasterData();
  purchaseOrder:PurchaseOrder;
  warehouse:WarehouseModel;
  supplier:SupplierModel;
  selectedStatus:DropDownModel;

  pageType: string;
  title: string;
  poId:number;
  poStep1Form: FormGroup;
  poStep2Form: FormGroup;
  poStep3Form: FormGroup;

  suppliers:DropDownModel[]=[];
  warehouses:DropDownModel[]=[];
  statuses:DropDownModel[]=[];

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
  
  constructor(private _poService: PoService, 
    private _warehouseService:WarehouseService,
    private _supplierService:SupplierService,
    private _route: ActivatedRoute,
    private decimalPipe: DecimalPipe,
    private _fuseProgressBarService: FuseProgressBarService,
    public _activateRoute: ActivatedRoute,
    private _snackBar: MatSnackBar,
    private _location: Location,
    private _formBuilder: FormBuilder,
    public _router: Router) {
      this._unsubscribeAll = new Subject();
      this.purchaseOrder = new PurchaseOrder();
     }

  ngOnInit(): void {
    this._activateRoute.params.subscribe(params => {
      this.poId = +params.id;
      this.pageType = this.poId === 0 ? 'new' : 'edit';

      this.createNewPOForm();
      this.getMasterData();
    });


    this._poService.onPODetailChanged.subscribe(response=>{
      
      let sum:number=0;

      this.poStep2Form.getRawValue().items.forEach(element => {
        let total:number= +element.total;
        sum = sum + total;    
      });

      this.poStep3Form.get('subTotal').setValue(sum);
      let taxAmount = ((this.subTotal-this.discount)*this.taxRate)/100.00;
      this.poStep3Form.get('totalTaxAmout').setValue(taxAmount);
      this.calculateTotal();
      
    });
  }

  createNewPOForm()
  {
    this.poStep1Form = this._formBuilder.group({
      id: [0],
      poNumber: [{ value: '', disabled: true }, Validators.required],
      date:[{ value: new Date(), disabled: this.isViewOnly },Validators.required],
      selectedSupplierId: [{ value: null, disabled: this.isViewOnly },Validators.required],
      selectedWarehouseId: [{ value: null, disabled: this.isViewOnly },Validators.required],
      status: [{ value: null, disabled: this.isViewOnly },Validators.required],
    });

    this.poStep2Form = this._formBuilder.group({
      items: this._formBuilder.array([]),
      finalAmount: [0.00]
    });

    this.poStep3Form = this._formBuilder.group({
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

  createPONumber()
  {    this._fuseProgressBarService.show();
      this._poService.getPONumber().subscribe(response=>{
        this.purchaseOrder.poNumber = response.number;
        this.poStep1Form.patchValue({'poNumber':response.number});
        this._fuseProgressBarService.hide();
      },error=>{
        this._fuseProgressBarService.hide();
      });
  }

  getMasterData()
  {
    this._poService.getPurchaseOrderMasterData().subscribe(response=>{
        this.masterData= response;
        this.suppliers =  this.masterData.suppliers;
        this.warehouses =  this.masterData.warehouses;
        this.statuses = this.masterData.statuses;

        if (this.pageType === 'edit') {
          this.title = 'Edit Purchase Order';
          this.getPODetails();
        }
        else {
          this.title = 'New Purchase Order';
          this.createPONumber();
        }

    },error=>{

    });
  }

  getPODetails()
  {
    this._fuseProgressBarService.show();
    this._poService.getById(this.poId)
      .subscribe(response=>{
        this.purchaseOrder = response;
        this._fuseProgressBarService.hide();
        
        this.poStep1Form = this._formBuilder.group({
          id: [this.purchaseOrder.id],
          date:[new Date(this.purchaseOrder.date),Validators.required],
          poNumber: [this.purchaseOrder.poNumber,Validators.required],
          selectedSupplierId: [this.purchaseOrder.selectedSupplierId,Validators.required],
          selectedWarehouseId: [this.purchaseOrder.selectedWarehouseId,Validators.required],
          status: [this.purchaseOrder.status]
        });

        let totalAmout: number = 0;

        const cf = this.purchaseOrder.items.map((value, index) => { return PurchaseOrderItem.asFormGroup(value, this.isViewOnly,this.decimalPipe,this._poService) });
        const fArray = new FormArray(cf);
        this.poStep2Form.setControl('items', fArray);

        this.updateView();

        this.purchaseOrder.items.forEach(element => {
          totalAmout = totalAmout + element.total;
        });

        this.poStep3Form = this._formBuilder.group({
          subTotal: [this.purchaseOrder.subTotal,Validators.required],
          discount: [this.purchaseOrder.discount,Validators.required],
          taxRate: [this.purchaseOrder.taxRate,Validators.required],
          totalTaxAmout: [this.purchaseOrder.totalTaxAmout],
          shippingCharge: [this.purchaseOrder.shippingCharge],
          total: [this.purchaseOrder.total],
          remarks: [this.purchaseOrder.remarks],
        });
        
        this.warehouseChanged(response.selectedWarehouseId);
        this.supplierChanged(response.selectedSupplierId);
        this.statusChanged(response.status);
      },error=>{
        this._fuseProgressBarService.hide();
      });
  }

  //For poStep2Form methods
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
      this._poService.onPODetailChanged.next(true);
      
  });
  
  fg.get("unitPrice").valueChanges.subscribe(value=>{
      const tot = 
      this.decimalPipe.transform(value*fg.get("qty").value,
      "1.2-2"
    );
      fg.get("total").setValue(tot);
      this._poService.onPODetailChanged.next(true);
  });

  if (this.isViewOnly) {
    fg.get("selectedCategoryId").disable();
    fg.get("selectedSubCategoryId").disable();
    fg.get("productId").disable();
    fg.get("qty").disable();
    fg.get("unitPrice").disable();
    fg.get("total").disable();
  }

  let poItem = new PurchaseOrderItem();
  poItem.categories = this.masterData.productCategories;

  if(!this.purchaseOrder.items)
  {
    this.purchaseOrder.items=[];
  }

  this.purchaseOrder.items.push(poItem);

  (this.poStep2Form.get('items') as FormArray).push(fg);

  this.table.renderRows();
  }

  deletePOItem(item: any, index: number) {

    this.purchaseOrder.items.splice(index,1);;
    (this.poStep2Form.get('items') as FormArray).removeAt(index);
    this.table.renderRows();
    this._poService.onPODetailChanged.next(true);
/*     this.po.poItems.splice(index, 1);
    let control = <FormArray>this.poFormStep3.get('poItems');
    control.clear();

    const cf = this.po.poItems.map((value, index) => { return POItem.asFormGroup(value, this.isViewOnly,this.decimalPipe,this._poService) });
    const fArray = new FormArray(cf);
    this.poFormStep3.setControl('poItems', fArray);

    this.calculateTotal();

    this.updateView(); */
  }

  productCatgoryChanged(item:any,index:number)
  {
    this._fuseProgressBarService.show();
    this._poService.getProductSubCategories(item)
      .subscribe(response=>{
        this._fuseProgressBarService.hide();
        this.purchaseOrder.items[index].subCategories = response;
      },error=>{
        this._fuseProgressBarService.hide();

      })
    
  }

  productSubCategoryChanged(item:any,index:number)
  {
    this._fuseProgressBarService.show();
    this._poService.getProducts(item,this.selectedSupplierId)
      .subscribe(response=>{
        this._fuseProgressBarService.hide();
        this.purchaseOrder.items[index].products = response;
      },error=>{
        this._fuseProgressBarService.hide();

      })
  }

  makeForm3Subsriber()
  {
      this.poStep3Form.get("discount").valueChanges.subscribe(value=>{
        this.calculateTotal();
      });

      this.poStep3Form.get("taxRate").valueChanges.subscribe(value=>{

        let taxAmount = ((this.subTotal-this.discount)*value)/100.00;
        this.poStep3Form.get('totalTaxAmout').setValue(taxAmount);
        this.calculateTotal();
      });

      this.poStep3Form.get("shippingCharge").valueChanges.subscribe(value=>{
        this.calculateTotal();
      });

  }

  calculateTotal()
  {
    let total = (this.subTotal-this.discount+this.totalTaxAmout+this.shippingCharge);
    this.poStep3Form.get('total').setValue(total);
  }

  warehouseChanged(item:any)
  {
    this._fuseProgressBarService.show();
    this._warehouseService.getWarehouseById(item)
      .subscribe(response=>{
        this._fuseProgressBarService.hide();
        this.warehouse = response;
      },error=>{
        this._fuseProgressBarService.hide();
      })
  }

  supplierChanged(item:any)
  {
    this._fuseProgressBarService.show();
    this._supplierService.GetSupplierById(item)
      .subscribe(response=>{
        this._fuseProgressBarService.hide();
        this.supplier=response;
      },error=>{
        this._fuseProgressBarService.hide();
      })
  }

  statusChanged(item:any)
  {
    this.statuses.forEach(element => {
      if(element.id==item)
      {
        this.selectedStatus = element;
      }
      
    });
  }

  goToBack()
  {
    this._location.back();
  }

  updateView() {
    this.dataSource.next(this.items.controls);
  }


  saveOrder(needExit: boolean): void {

    let form1 = this.poStep1Form.getRawValue();
    let form2 = this.poStep2Form.getRawValue();
    let form3 = this.poStep3Form.getRawValue();

    if(!this.purchaseOrder)
    {
      //this.purchaseOrder = new PurchaseOrder();
      this.purchaseOrder.id=0;
    }

    this.purchaseOrder.poNumber = form1.poNumber;
    this.purchaseOrder.date=form1.date;
    this.purchaseOrder.selectedSupplierId = form1.selectedSupplierId;
    this.purchaseOrder.selectedWarehouseId = form1.selectedWarehouseId;
    this.purchaseOrder.status = form1.status;
    //this.purchaseOrder.items = form2;
    for (let index = 0; index < this.purchaseOrder.items.length; index++) {
      
      this.purchaseOrder.items[index].id=form2.items[index].id;
      this.purchaseOrder.items[index].purchaseOrderId=form2.items[index].purchaseOrderId;
      this.purchaseOrder.items[index].selectedCategoryId=form2.items[index].selectedCategoryId;
      this.purchaseOrder.items[index].selectedSubCategoryId=form2.items[index].selectedSubCategoryId;
      this.purchaseOrder.items[index].productId=form2.items[index].productId;
      this.purchaseOrder.items[index].qty=form2.items[index].qty;
      this.purchaseOrder.items[index].unitPrice=form2.items[index].unitPrice;
      this.purchaseOrder.items[index].total=form2.items[index].total;
      
    }
    this.purchaseOrder.subTotal= form3.subTotal;
    this.purchaseOrder.discount= form3.discount;
    this.purchaseOrder.taxRate= form3.taxRate;
    this.purchaseOrder.totalTaxAmout= form3.totalTaxAmout;
    this.purchaseOrder.shippingCharge= form3.shippingCharge;
    this.purchaseOrder.total= form3.total;
    this.purchaseOrder.remarks= form3.remarks;

    console.log(this.purchaseOrder);
    

    this._fuseProgressBarService.show();
    this._poService.save(this.purchaseOrder)
      .subscribe(response=>{
        this._fuseProgressBarService.hide();
        this.goToBack();
      },error=>{
        this._fuseProgressBarService.hide();
      })
  }

  get id()
  {
    return this.poStep1Form.get('id').value;
  }

  get selectedSupplierId()
  {
    return this.poStep1Form.get('selectedSupplierId').value;
  }

  get poNumber()
  {
    return this.poStep1Form.get('poNumber').value;
  }

  get date()
  {
    return this.poStep1Form.get('date').value;
  }

  get items(): FormArray {
    return this.poStep2Form.get('items') as FormArray;
  }

  get subTotal()
  {
    return this.poStep3Form.get("subTotal").value;
  }

  get discount()
  {
    return this.poStep3Form.get("discount").value;
  }

  get taxRate()
  {
    return this.poStep3Form.get("taxRate").value;
  }

  get totalTaxAmout()
  {
    return this.poStep3Form.get("totalTaxAmout").value;
  }

  get shippingCharge()
  {
    return this.poStep3Form.get("shippingCharge").value;
  }

  get totalAmount()
  {
    return this.poStep3Form.get("total").value;
  }
}
