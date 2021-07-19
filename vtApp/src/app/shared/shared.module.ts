import { NgModule } from '@angular/core';
import { CommonModule, DecimalPipe } from '@angular/common';
import { ConfirmationDialog } from './confirmation-dialog/confirmation-dialog';
import { ThousandSeparatorDirective } from './thousand-separator.directive';

@NgModule({
  declarations: [ConfirmationDialog,ThousandSeparatorDirective],
  imports: [CommonModule],
  providers:[DecimalPipe],
  exports: [ThousandSeparatorDirective]
})
export class SharedModule { }
