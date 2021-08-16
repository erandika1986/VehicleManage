import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatColors } from '@fuse/mat-colors';
import { DropDownModel } from 'app/models/common/drop-down.modal';
import { InventoryBasicDetailModel } from 'app/models/inventory/inventory.basic.detail.model';
import { POInventoryReceievedDetailModel } from 'app/models/inventory/po.inventory.receieved.detail.model';
import { InventoryModel } from 'app/models/inventory/inventory.model';
import { InventoryService } from 'app/services/inventory/inventory.service';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-add-inventory',
  templateUrl: './add-inventory.component.html',
  styleUrls: ['./add-inventory.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AddInventoryComponent implements OnInit {

  permissionId: number;
  isViewOnly: boolean = false;

  action: string;
  inventoryBasicModel:InventoryBasicDetailModel;
  inventoryForm: FormGroup;
  dialogTitle: string;
  presetColors = MatColors.presets;

  inventoryDetail:POInventoryReceievedDetailModel;
  purchaseOrders:DropDownModel[]=[];
  warehouses:DropDownModel[]=[];
  suppliers:DropDownModel[]=[];
  
  selectedPurchaseOrderId:number;
  selectedWarehouseId:number;
  selectedSupplierId:number;

  dataSource = new BehaviorSubject<AbstractControl[]>([]);
  //rows: FormArray = this._formBuilder.array([]);
  displayedColumns = ['productName','dateOfExpiration','batchNo','totalOrderedQty','alreadyRecievedQty','currentReceivedQty'];

  constructor(public matDialogRef: MatDialogRef<AddInventoryComponent>, 
    @Inject(MAT_DIALOG_DATA) private _data: any,
    private _formBuilder: FormBuilder,
    private inventoryService:InventoryService) 
    { 
      this.dialogTitle="Add New Inventory";
      this.inventoryDetail = new POInventoryReceievedDetailModel();
      this.inventoryForm = this._formBuilder.group({
        inventories: this._formBuilder.array([]),
        warehouseId: [0],
        puchaseOrderId: [0],
        supplierId: [0],
      });
    }

  ngOnInit(): void {
    this.getMasterData();
  }


  getMasterData()
  {
    this.inventoryService.getInventoryMasterData().subscribe(response=>{
        this.purchaseOrders= response.activePurchaseOrders;
        this.warehouses=response.warehouses;
        this.suppliers = response.suppliers;
        if(this.purchaseOrders.length>0)
        {
          this.selectedPurchaseOrderId = this.purchaseOrders[0].id;
          this.getPOData();
        }
    },error=>{

    });
  }

  getPOData()
  {
    this.inventoryService.getInventoryDetailsForPO(this.selectedPurchaseOrderId)
      .subscribe(response=>{
        this.inventoryDetail =response;
        this.selectedWarehouseId = response.warehouseId;
        this.selectedSupplierId=response.supplierId;

        const cf = this.inventoryDetail.inventories.map((value, index) => { return InventoryModel.asFormGroup(value, this.isViewOnly) });
        const fArray = new FormArray(cf);
        this.inventoryForm.setControl('inventories', fArray);
        this.inventoryForm.get("warehouseId").patchValue(this.selectedWarehouseId);
        this.inventoryForm.get("puchaseOrderId").patchValue(response.puchaseOrderId);
        this.inventoryForm.get("supplierId").patchValue(this.selectedSupplierId);
        this.updateView();
      },error=>{

      });
  }

  updateView() {
    this.dataSource.next(this.inventories.controls);
  }

  seletedPOChanged(item:any)
  {
    this.inventoryForm.get("puchaseOrderId").patchValue(item);
    this.getPOData();
      
  }

  selectedWarehouseChanged(item:any)
  {
    this.inventoryForm.get("warehouseId").patchValue(item);
  }
  get inventories(): FormArray {
    return this.inventoryForm.get('inventories') as FormArray;
  }

}
