import { ExpenseImageModel } from './expense.Image.model';
export class ExpensesModel{

    id:number;
    expenseCategoryId:number;
    description:string;
    expenseDate:Date;
    expenseYear:number;
    expenseMonth:number;
    expenseDay:number;
    amount:number;
    vehicleId:number;
    vehicleExpenseTypeId:number;
    expenseImages:ExpenseImageModel[];

}