import { Injectable } from "@angular/core";
import { DropDownModel } from './../common/drop-down.modal';

@Injectable()
export class ExpensesMasterDataModel
{
    vehicles:DropDownModel[];
    expensesCategories:DropDownModel[];
    vehicleExpenses:DropDownModel[];
}