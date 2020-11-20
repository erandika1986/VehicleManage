import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BeatsComponent } from './beats/beats.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';
import { DailyBeatRoutingModule } from './daily-beat-routing.module';

import { MatDialogModule } from '@angular/material/dialog';

@NgModule({
  declarations: [BeatsComponent],
  imports: [
    CommonModule,
    DailyBeatRoutingModule,
    NgbModule,
    MatDialogModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    NgbModule
  ]
})
export class DailyBeatModule { }
