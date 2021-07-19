import { DecimalPipe } from "@angular/common";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { PoService } from "app/services/po/po.service";
import { DropDownModel } from "../common/drop-down.modal";
import { PurchaseOrder } from "./purchase.order.model";

export class PurchaseOrderItem
{
    id:number;
    purchaseOrderId:number;
    selectedCategoryId:number;
    selectedSubCategoryId:number;
    productId:number;
    qty:number;
    unitPrice:number;
    total:number;

    categories:DropDownModel[];
    subCategories:DropDownModel[];
    products:DropDownModel[];

    static asFormGroup(item: PurchaseOrderItem, isDisable: boolean,decimalPipe: DecimalPipe,poService:PoService): FormGroup {

        const unitPrice = decimalPipe.transform(
            item.unitPrice,
            "1.2-2"
          );

          const total = decimalPipe.transform(
            item.total,
            "1.2-2"
          );

      

        const fg = new FormGroup({
            id: new FormControl(item.id),
            purchaseOrderId: new FormControl(item.purchaseOrderId),
            selectedCategoryId: new FormControl(item.selectedCategoryId,Validators.required),
            selectedSubCategoryId: new FormControl(item.selectedSubCategoryId,Validators.required),
            productId: new FormControl(item.productId,Validators.required),
            qty: new FormControl(item.qty,Validators.required),
            unitPrice: new FormControl(unitPrice,Validators.required),
            total: new FormControl(total,Validators.required),
        });

        fg.get("quantity").valueChanges.subscribe(value=>{

            const tot = decimalPipe.transform(
                value*fg.get("unitPrice").value,
                "1.2-2"
              );
            fg.get("total").setValue(tot);
            poService.onPODetailChanged.next(true);
        });

        fg.get("unitPrice").valueChanges.subscribe(value=>{
            const tot = decimalPipe.transform(
                value*fg.get("quantity").value,
                "1.2-2"
              );
            fg.get("total").setValue(tot);
            poService.onPODetailChanged.next(true);
        });


        if (isDisable) {
            fg.get("selectedCategoryId").disable();
            fg.get("selectedSubCategoryId").disable();
            fg.get("productId").disable();
            fg.get("qty").disable();
            fg.get("unitPrice").disable();
            fg.get("total").disable();
        }
        return fg;
    }
}