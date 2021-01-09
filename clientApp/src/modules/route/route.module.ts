import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RoutesComponent } from './routes/routes.component';
import { RouteRoutingModule } from './route-routing.module';
import { RouteDetailComponent } from './route-detail/route-detail.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { SharedModule } from 'primeng/api';


@NgModule({
  declarations: [RoutesComponent, RouteDetailComponent],
  imports: [
    CommonModule,
    RouteRoutingModule,
    NgbModule,
    MatDialogModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule
  ],
  providers: [],
  entryComponents: [
    RouteDetailComponent
  ]
})
export class RouteModule { }
