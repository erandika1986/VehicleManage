import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BeatsComponent } from './beats/beats.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DailyBeatRoutingModule } from './daily-beat-routing.module';

import { MatDialogModule } from '@angular/material/dialog';
import { CalendarModule } from 'primeng/calendar';
import { TableModule } from 'primeng/table';
import { DialogModule } from 'primeng/dialog';

import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { SharedModule } from 'primeng/api';


@NgModule({
  declarations: [BeatsComponent],
  imports: [
    CommonModule,
    DailyBeatRoutingModule,
    CalendarModule,
    DialogModule,
    TableModule,
    ConfirmDialogModule,
    NgbModule,
    MatDialogModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    NgbModule
  ]
})
export class DailyBeatModule { }
