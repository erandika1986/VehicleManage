
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { ExpensesMasterDataModel } from 'app/models/expenses/expenses.master.data.model';
import { ExpensesModel } from './../../../../models/expenses/expenses.model';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FuseProgressBarService } from './../../../../../@fuse/components/progress-bar/progress-bar.service';
import { ExpensesService } from './../../../../services/expenses/expenses.service';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject, Observable, EMPTY } from 'rxjs';
import { Upload } from './../../../../models/common/upload';
import { HttpEventType } from '@angular/common/http';
import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FuseConfirmDialogComponent } from '@fuse/components/confirm-dialog/confirm-dialog.component';


@Component({
  selector: 'app-expenses-detail',
  templateUrl: './expenses-detail.component.html',
  styleUrls: ['./expenses-detail.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ExpensesDetailComponent implements OnInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;

  
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
    private _matDialog: MatDialog,
    public _activateRoute: ActivatedRoute,
   
    @Inject(MAT_DIALOG_DATA) private _data: any,
    private _formBuilder: FormBuilder) { 
              this._unsubscribeAll = new BehaviorSubject(false);
              // Set the defaults
              this.action = _data.action;
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
      expenseDate: [{value:new Date(this.expense.expenseDate), disabled: this.isViewOnly},Validators.required],
      amount: [{value:this.expense.amount, disabled: this.isViewOnly},Validators.required],
      vehicleId: [{value:this.expense.vehicleId, disabled: this.isViewOnly}],
      vehicleExpenseTypeId: [{value:this.expense.vehicleExpenseTypeId, disabled: this.isViewOnly}],
      expenseImages:['']
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

  getExpenseDetailsById()
  {
    this._fuseProgressBarService.show();
    this._expensesService.getExpensesById(this.expense.id, this.expense.expenseCategoryId)
    .subscribe(response=>
    {
      this.expense = response;

      this._fuseProgressBarService.hide();
      this.expenseForm = this.createExistingExpenseForm();

    },error=>{

      this._fuseProgressBarService.hide();

    })
  }

  upload$: Observable<Upload> = EMPTY;
  precentage:any;

  onFileChange(event:any, type:number)
  {
    let file = event.srcElement;
    const formData = new FormData();

    formData.set("id",this.expense.id.toString());
    formData.set("type", type.toString());

    if(file.files.length > 0)
    {
      this._fuseProgressBarService.show();

      for (let index = 0; index < file.files.length; index++) 
      {
        formData.append('file',file.files[index], file.files[index].name);
        
      }

      console.log(formData.get("id"));

      this._expensesService.uploadExpenseReceiptImage(formData).subscribe(response=>{

        this.precentage = response;
        if(response.state === "DONE")
        {

          this._fuseProgressBarService.hide();
          this._expensesService.onExpensesDetailsSaved.next(true);
          this.getExpenseDetailsById();

          this._snackBar.open("Image has been uploaded successfully", 'Success',
          {
                duration: 2500,
                horizontalPosition: this.horizontalPosition,
                verticalPosition: this.verticalPosition,
          });

        }
      },error=>{

        this._fuseProgressBarService.hide();
        this._snackBar.open("Error has been occured image upload please try again","Eroor",
        {
            duration:2500,
            horizontalPosition:this.horizontalPosition,
            verticalPosition:this.verticalPosition,
        });
      })
    }
  }

  downloadPercentage:number = 0;
  isDownloading:boolean;

  downloadExpenseReceiptImage(id:number, attachmentName:string)
  {
    console.log(id,attachmentName);
    
    this._fuseProgressBarService.show();
    this.isDownloading = true;

    this._expensesService.downloadExpenseReceiptImage(this.expense.id, id).subscribe(response=>{
      console.log(response);
      
      if (response.type === HttpEventType.DownloadProgress) {
        this.downloadPercentage = Math.round(100 * response.loaded / response.total);
      }
      
      if (response.type === HttpEventType.Response) {
        if(response.status == 204)
        {
          this.isDownloading=false;
          this.downloadPercentage=0;
          this._fuseProgressBarService.hide();
        }
        else
        {
          const objectUrl: string = URL.createObjectURL(response.body);
          const a: HTMLAnchorElement = document.createElement('a') as HTMLAnchorElement;
  
          a.href = objectUrl;
          a.download = attachmentName;
          document.body.appendChild(a);
          a.click();
  
          document.body.removeChild(a);
          URL.revokeObjectURL(objectUrl);
          this.isDownloading=false;
          this.downloadPercentage=0;
          this._fuseProgressBarService.hide();
        }

      }
    },error=>{
      this._fuseProgressBarService.hide();
      this.isDownloading=false;
      this.downloadPercentage=0;
    });

  }
  

  deleteExpeseImage(id:number)
  {

    this.confirmDialogRef = this._matDialog.open(FuseConfirmDialogComponent, {
      disableClose: false
    });

    this.confirmDialogRef.componentInstance.confirmMessage = 'Are you sure you want to delete this record?';

    this.confirmDialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._fuseProgressBarService.show();

        this._expensesService.deleteExpenseReciptImage(this.expense.id, id)
        .subscribe(response=>{
          console.log("dev11111111111111");
          console.log(response);
          
          
          if (response.isSuccess) {
            this._snackBar.open(response.message, 'Success', {
              duration: 2500,
              horizontalPosition: this.horizontalPosition,
              verticalPosition: this.verticalPosition,
            });

            this._fuseProgressBarService.hide();
            this.getExpenseDetailsById();
          }
          else {
            this._snackBar.open(response.message, 'Error', {
              duration: 2500,
              horizontalPosition: this.horizontalPosition,
              verticalPosition: this.verticalPosition,
            });
          }   
        },error=>{
          this._fuseProgressBarService.hide();
          this._snackBar.open("Network error has been occured. Please try again.", 'Error', {
            duration: 2500,
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition,
          });
        })

      }
      this.confirmDialogRef = null;
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
