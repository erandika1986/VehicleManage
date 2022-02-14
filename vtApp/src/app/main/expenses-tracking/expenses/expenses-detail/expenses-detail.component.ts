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
  expenseFrom: FormGroup;
  dialogTitle: string;

  constructor(public matDialogRef: MatDialogRef<UserDetailComponent>,
    private _fuseProgressBarService: FuseProgressBarService,
    private _expensesService:ExpensesService,
    private _snackBar: MatSnackBar,
    @Inject(MAT_DIALOG_DATA) private _data: any,
    private _formBuilder: FormBuilder) { 

      console.log(_data);
      
              // Set the defaults
              this.action = _data.action;
              this.expensesMasterData = _data.masterData;


              if ( this.action === 'edit' )
              {
                  this.dialogTitle = 'Edit Expense';
                  this.expense = _data.expense;
                  this.expenseFrom = this.createExistingExpenseForm();
              }
              else
              {
                  this.dialogTitle = 'New User';
                  this.expense = new ExpensesModel();
                  this.expenseFrom = this.createContactForm();
              }
      
          
    }

  ngOnInit(): void {
  }

  createExistingExpenseForm():FormGroup
  {
    console.log(this.expense);
    
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

  createContactForm(): FormGroup
  {
      return this._formBuilder.group({
        id: [0],
        expenseCategoryId: [[null],Validators.required],
        description: ['',Validators.required],
        expenseDate: ['',Validators.required],
        amount: ['',Validators.required],
        vehicleId: [null],
        vehicleExpenseTypeId: [null],
      });
  }

//   upload$: Observable<Upload> = EMPTY;
//   precentage:any;
//   onFileChange(event: any,type:number) 
//   {

//     let fi = event.srcElement;
//     const formData = new FormData();
//     formData.set("id",this.user.id.toString());
//     formData.set("type",type.toString());
    
//     if(fi.files.length>0)
//     {
//         this._fuseProgressBarService.show();
//         for (let index = 0; index < fi.files.length; index++) {
          
//           formData.append('file', fi.files[index], fi.files[index].name);
//         }

//         this._userService.uploadUserImage(formData).subscribe(res=>
//           {
//             this.precentage =res;
//             if(res.state=="DONE")
//             {
//               //item.isUploading=false;
//               this._fuseProgressBarService.hide();
//               this._userService.onUserUpdated.next(true);
//               this.getUser(this.user.id);
//               //this.getVehicleFitnessreportList();
//               this._snackBar.open("Image has been uploaded successfully", 'Success', {
//                 duration: 2500,
//                 horizontalPosition: this.horizontalPosition,
//                 verticalPosition: this.verticalPosition,
//               });
//             }
//             //progress
//           },error=>{
//             this._fuseProgressBarService.hide();
//             //item.isUploading=false;
//             this._snackBar.open("Network error has been occured. Please try again.", 'Error', {
//               duration: 2500,
//               horizontalPosition: this.horizontalPosition,
//               verticalPosition: this.verticalPosition,
//             });
//           });
// /*         this._quotationService.uploadQuotationFiles(formData)
//           .subscribe(response=>{
 
//           },error=>{
//             console.log("Error occured");
            
//           }); */
//     }    
/*   }

  downloadPercentage:number=0;
  isDownloading:boolean;
  downloadFile(type:number,fileName:string)
  {
    this._fuseProgressBarService.show();
    this.isDownloading=true;
    this._userService.downloadUserImage(this.user.id,type)
      .subscribe(response=>{

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
            a.download = fileName;
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
 */
  getExpenseById(id:number,expenseCategoryId:number)
  {
    this._fuseProgressBarService.show();
    this._expensesService.getExpensesById(id,expenseCategoryId)
      .subscribe(response=>{
        this._fuseProgressBarService.hide();
        this.expense = response;
        this.expenseFrom = this.createExistingExpenseForm();
      },error=>{
        this._fuseProgressBarService.hide();
      });
  }

  get expeseCatagoryId()
  {
    return this.expenseFrom.get('expenseCategoryId').value;
  }


  get id()
  {
    return this.expenseFrom.get('id').value;
  }


}
