import { DecimalPipe, Location } from '@angular/common';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { MatTable } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { DropDownModel } from 'app/models/common/drop-down.modal';
import { PurchaseOrderItem } from 'app/models/po/purchase.order.item.model';
import { PurchaseOrderMasterData } from 'app/models/po/purchase.order.master.data.model';
import { PurchaseOrder } from 'app/models/po/purchase.order.model';
import { PoService } from 'app/services/po/po.service';
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
  pageType: string;
  title: string;
  poId:number;
  poStep1Form: FormGroup;
  poStep2Form: FormGroup;


  suppliers:DropDownModel[]=[];
  warehouses:DropDownModel[]=[];
  statuses:DropDownModel[]=[];

  permissionId: number;
  isViewOnly: boolean = false;

  isExistingRecord: boolean = false;
  previouslySelectedTab: number = 0;

  dataSource = new BehaviorSubject<AbstractControl[]>([]);
  rows: FormArray = this._formBuilder.array([]);
  displayedColumns = ['action', 'purchaseOrderId', 'selectedCategoryId', 'selectedSubCategoryId', 'productId','qty','unitPrice','total'];
  

  @ViewChild(DaterangepickerDirective, { static: true })
  pickerDirective: DaterangepickerDirective;

  // Private
  private _unsubscribeAll: Subject<any>;

  @ViewChild(MatTable) table: MatTable<any>;
  
  constructor(private _poService: PoService, 
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

      if (this.pageType === 'edit') {
        this.title = 'Edit Purchase Order';
        this.getPODetails();
      }
      else {
        this.title = 'New Purchase Order';
        this.createPONumber();
      }

      this.getMasterData();
    });

    this.poStep2Form = this._formBuilder.group({
      items: this._formBuilder.array([]),
      finalAmount: [0.00]
    });
  }

  createNewPOForm()
  {
    this.poStep1Form = this._formBuilder.group({
      id: [0],
      poNumber: ['',Validators.required],
      selectedSupplierId: [0,Validators.required],
      selectedWarehouseId: [0,Validators.required],
      subTotal: [0,Validators.required],
      discount: [0,Validators.required],
      taxRate: [0,Validators.required],
      totalTaxAmout: [false],
      shippingCharge: [0],
      total: [0],
      status: [''],
      remarks: [0],
      qty:[0]
    });


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
        
        this.poStep2Form = this._formBuilder.group({
          id: [this.purchaseOrder.id],
          poNumber: [this.purchaseOrder.poNumber,Validators.required],
          selectedSupplierId: [this.purchaseOrder.selectedSupplierId,Validators.required],
          selectedWarehouseId: [this.purchaseOrder.selectedWarehouseId,Validators.required],
          subTotal: [this.purchaseOrder.subTotal,Validators.required],
          discount: [this.purchaseOrder.discount,Validators.required],
          taxRate: [this.purchaseOrder.taxRate,Validators.required],
          totalTaxAmout: [this.purchaseOrder.totalTaxAmout],
          shippingCharge: [this.purchaseOrder.shippingCharge],
          total: [this.purchaseOrder.total],
          status: [this.purchaseOrder.status],
          remarks: [this.purchaseOrder.remarks],
        });

        let totalAmout: number = 0;

        const cf = this.purchaseOrder.items.map((value, index) => { return PurchaseOrderItem.asFormGroup(value, this.isViewOnly,this.decimalPipe,this._poService) });
        const fArray = new FormArray(cf);
        this.poStep2Form.setControl('items', fArray);

        this.purchaseOrder.items.forEach(element => {
          totalAmout = totalAmout + element.total;
        });
        
      },error=>{
        this._fuseProgressBarService.hide();
      });
  }
}
