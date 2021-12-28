import { DecimalPipe } from "@angular/common";
import { Injectable } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { SalesOrderService } from "app/services/sales-order/sales-order.service";
import { DropDownModel } from "../common/drop-down.modal";

@Injectable()
export class SalesOrderItemModel {
    id :number;
    salesOrderId :number;
    selectedCategoryId:number;
    selectedSubCategoryId:number;
    productId :number;
    productName:string;
    categoryName:string;
    subCategoryName:string;
    qty :number;
    unitPrice :number;
    total :number;

    categories:DropDownModel[];
    subCategories:DropDownModel[];
    products:DropDownModel[];

    static asFormGroup(item: SalesOrderItemModel, isDisable: boolean,saleOrderService:SalesOrderService): FormGroup {

        const fg = new FormGroup({
            id: new FormControl(item.id),
            salesOrderId: new FormControl(item.salesOrderId),
            selectedCategoryId: new FormControl(item.selectedCategoryId,Validators.required),
            selectedSubCategoryId: new FormControl(item.selectedSubCategoryId,Validators.required),
            productId: new FormControl(item.productId,Validators.required),
            productName: new FormControl(item.productName,Validators.required),
            categoryName: new FormControl(item.categoryName,Validators.required),
            subCategoryName: new FormControl(item.subCategoryName,Validators.required),
            qty: new FormControl(item.qty,Validators.required),
            unitPrice: new FormControl(item.unitPrice,Validators.required),
            total: new FormControl(item.total,Validators.required),
        });


        if (isDisable) {
            fg.get("selectedCategoryId").disable();
            fg.get("selectedSubCategoryId").disable();
            fg.get("productId").disable();
            fg.get("productName").disable();
            fg.get("categoryName").disable();
            fg.get("subCategoryName").disable();
            fg.get("qty").disable();
            fg.get("unitPrice").disable();
            fg.get("total").disable();
        }
        return fg;
    }
}