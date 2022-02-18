import { Component, OnInit, ViewEncapsulation, Inject } from '@angular/core';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { ExpensesMasterDataModel } from 'app/models/expenses/expenses.master.data.model';
import { ExpensesModel } from './../../../../models/expenses/expenses.model';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserDetailComponent } from './../../../admin/user/user-detail/user-detail.component';
import { FuseProgressBarService } from './../../../../../@fuse/components/progress-bar/progress-bar.service';
import { ExpensesService } from './../../../../services/expenses/expenses.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-expenses-detail',
  templateUrl: './expenses-detail.component.html',
  styleUrls: ['./expenses-detail.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ExpensesDetailComponent implements OnInit {

  
  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';
  
  action: string;
  expense: ExpensesModel;
  expensesMasterData:ExpensesMasterDataModel;
  expenseForm: FormGroup;
  dialogTitle: string;

  constructor(public matDialogRef: MatDialogRef<ExpensesDetailComponent>,
    private _fuseProgressBarService: FuseProgressBarService,
    private _expensesService:ExpensesService,
    private _snackBar: MatSnackBar,
    @Inject(MAT_DIALOG_DATA) private _data: any,
    private _formBuilder: FormBuilder) { 
        
              // Set the defaults
              this.action = _data.action;
              this.expensesMasterData = _data.masterData;
            
              if ( this.action === 'edit' )
              {
                  this.dialogTitle = 'Edit Expense';
                  this.expense = _data.data;
                  this.expenseForm = this.createExistingExpenseForm();
              }
              else
              {
                  this.dialogTitle = 'New Expense';
                  this.expense = new ExpensesModel();
                  this.expenseForm = this.createExpenseForm();
              }
      
          
    }

  ngOnInit(): void {
  }

  createExistingExpenseForm():FormGroup
  {
    
    return this._formBuilder.group({
      id: [this.expense.id],
      expenseCategoryId: [this.expense.expenseCategoryId,Validators.required],
      description: [this.expense.description,Validators.required],
      expenseDate: [this.expense.expenseDate,Validators.required],
      amount: [this.expense.amount,Validators.required],
      vehicleId: [this.expense.vehicleId],
      vehicleExpenseTypeId: [this.expense.vehicleExpenseTypeId],
      
    });
  }

  createExpenseForm(): FormGroup
  {
      return this._formBuilder.group({
        id: [0],
        expenseCategoryId: [[null],Validators.required],
        description: ['',Validators.required],
        expenseDate: ['',Validators.required],
        amount: ['',Validators.required],
        vehicleId: [0],
        vehicleExpenseTypeId: [0],
      });
  }

  getExpenseById(id:number,expenseCategoryId:number)
  {
    this._fuseProgressBarService.show();
    this._expensesService.getExpensesById(id,expenseCategoryId)
      .subscribe(response=>{
        this._fuseProgressBarService.hide();
        this.expense = response;
        this.expenseForm = this.createExistingExpenseForm();
      },error=>{
        this._fuseProgressBarService.hide();
      });
  }

  get expeseCatagoryId()
  {
    return this.expenseForm.get('expenseCategoryId').value;
  }


  get id()
  {
    return this.expenseForm.get('id').value;
  }


}
