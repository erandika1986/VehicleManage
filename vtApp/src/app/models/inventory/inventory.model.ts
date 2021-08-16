import { Injectable } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";

@Injectable()
export class InventoryModel {
    id:number;
    dateRecieved:Date;
    batchNo:string;
    dateOfManufacture:Date;
    dateOfExpiration:Date;
    productCategoryName:string;
    productSubCategoryName:string;
    supplierName:string;
    productName:string;
    productId:number;
    totalOrderedQty:number;
    alreadyRecievedQty:number;
    currentReceivedQty:number;
    isActive:boolean;

    static asFormGroup(item: InventoryModel, isDisable: boolean): FormGroup {

        const fg = new FormGroup({
            id: new FormControl(item.id),
            batchNo:new FormControl(item.batchNo),
            dateRecieved: new FormControl(new Date(item.dateRecieved)),
            dateOfManufacture: new FormControl(new Date(item.dateOfManufacture)),
            dateOfExpiration: new FormControl(new Date(item.dateOfExpiration)),
            productCategoryName: new FormControl(item.productCategoryName),
            productSubCategoryName: new FormControl(item.productSubCategoryName),
            supplierName: new FormControl(item.supplierName),
            productName: new FormControl(item.productName),
            productId: new FormControl(item.productId),
            totalOrderedQty: new FormControl(item.totalOrderedQty),
            alreadyRecievedQty: new FormControl(item.alreadyRecievedQty),
            currentReceivedQty: new FormControl(item.currentReceivedQty,Validators.required),
            isActive: new FormControl(true),
        });
        
        fg.get("productName").disable();
        fg.get("productId").disable();
        fg.get("totalOrderedQty").disable();
        fg.get("alreadyRecievedQty").disable();

        if (isDisable) {
            fg.get("batchNo").disable();
            fg.get("dateRecieved").disable();
            fg.get("dateOfManufacture").disable();
            fg.get("dateOfExpiration").disable();
            fg.get("productCategoryName").disable();
            fg.get("productSubCategoryName").disable();
            fg.get("supplierName").disable();

            fg.get("currentReceivedQty").disable();
            fg.get("isActive").disable();
        }
        return fg;
    }
}