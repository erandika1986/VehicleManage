import { Injectable } from '@angular/core';
import { PaginatedItemsModel } from './../common/paginated.items.model';
import { BasicExpensesModel } from './basic-expenses.model';
@Injectable()
export class ExpensesPaginatedItemsModel extends PaginatedItemsModel {

    data: BasicExpensesModel[];
}