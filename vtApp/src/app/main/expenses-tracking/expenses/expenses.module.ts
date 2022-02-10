import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExpensesFilterComponent } from './sidebars/expenses-filter/expenses-filter.component';
import { ExpensesListComponent } from './expenses-list/expenses-list.component';
import { RouterModule, Routes } from '@angular/router';
import { MaterialModule } from 'app/MaterialModule';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseSidebarModule, FuseWidgetModule } from '@fuse/components';
import { SharedModule } from 'app/shared/shared.module';
import { MatToolbarModule } from '@angular/material/toolbar';
import { ExpensesComponent } from './expenses.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
    pathMatch: 'full'
  },
  {
    path: 'list',
    component: ExpensesComponent

  }
];

@NgModule({
  declarations: [
    ExpensesComponent,
    ExpensesFilterComponent,
    ExpensesListComponent,
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    MaterialModule,
    FuseSharedModule,
    FuseWidgetModule,
    SharedModule,    
    FuseSidebarModule,
    MatToolbarModule
  ]
})
export class ExpensesModule { }
