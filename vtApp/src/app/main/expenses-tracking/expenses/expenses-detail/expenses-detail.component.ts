import { Component, OnInit, ViewEncapsulation, Inject } from '@angular/core';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { ExpensesMasterDataModel } from 'app/models/expenses/expenses.master.data.model';
import { ExpensesModel } from './../../../../models/expenses/expenses.model';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FuseProgressBarService } from './../../../../../@fuse/components/progress-bar/progress-bar.service';
import { ExpensesService } from './../../../../services/expenses/expenses.service';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject } from 'rxjs';

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
  isViewOnly:boolean = false;
  expenseId:number;

  private _unsubscribeAll: BehaviorSubject<any>;

  constructor(public matDialogRef: MatDialogRef<ExpensesDetailComponent>,
    private _fuseProgressBarService: FuseProgressBarService,
    private _expensesService:ExpensesService,
    private _snackBar: MatSnackBar,
    public _activateRoute: ActivatedRoute,
   
    @Inject(MAT_DIALOG_DATA) private _data: any,
    private _formBuilder: FormBuilder) { 
              this._unsubscribeAll = new BehaviorSubject(false);
              // Set the defaults
              this.action = _data.action;
              console.log(this.action);
              
              this.expensesMasterData = _data.masterData;

              
              if ( this.action === 'edit' || this.action === 'view')
              {
                  if(this.action === 'view'){

                    this._expensesService.onClickViewOnly.subscribe(response=>{
  
                      console.log('click'+ response + this.action);
                      this.isViewOnly = response;
                    });

                  }

                  this.dialogTitle = this.action === 'edit'?'Edit Expense' : 'View Expennse';
                  this.expense = _data.data;
                  console.log("data");
                  
                  console.log(this.expense);
                  
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
  ngOnDestroy(): void
  {
      // Unsubscribe from all subscriptions
      this._unsubscribeAll.next(false);
      this._unsubscribeAll.complete();
  }
  createExistingExpenseForm():FormGroup
  {
    
    return this._formBuilder.group({
      id: [this.expense.id],
      expenseCategoryId: [{value:this.expense.expenseCategoryId, disabled: this.isViewOnly}, Validators.required],
      description: [{value:this.expense.description, disabled: this.isViewOnly},Validators.required],
      expenseDate: [{value:this.expense.expenseDate, disabled: this.isViewOnly},Validators.required],
      amount: [{value:this.expense.amount, disabled: this.isViewOnly},Validators.required],
      vehicleId: [{value:this.expense.vehicleId, disabled: this.isViewOnly}],
      vehicleExpenseTypeId: [{value:this.expense.vehicleExpenseTypeId, disabled: this.isViewOnly}],
      
    });
  }

  createExpenseForm(): FormGroup
  {
      return this._formBuilder.group({
        id: [0],
        expenseCategoryId: [[null],Validators.required],
        description: ['',Validators.required],
        expenseDate: [new Date(),Validators.required],
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
