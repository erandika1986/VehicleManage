import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExpensesListComponent } from './expenses-list/expenses-list.component';
import { RouterModule, Routes } from '@angular/router';
import { MaterialModule } from 'app/MaterialModule';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseSidebarModule, FuseWidgetModule } from '@fuse/components';
import { SharedModule } from 'app/shared/shared.module';
import { MatToolbarModule } from '@angular/material/toolbar';
import { ExpensesComponent } from './expenses.component';
import { MainComponent } from './sidebars/main/main.component';
import { ExpensesDetailComponent } from './expenses-detail/expenses-detail.component';

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
    ExpensesListComponent,
    MainComponent,
    ExpensesDetailComponent,
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    MaterialModule,
    FuseSharedModule,
    FuseWidgetModule,
    SharedModule,    
    FuseSidebarModule,
    MatToolbarModule,
  ],
  entryComponents: [
    ExpensesDetailComponent
]
})
export class ExpensesModule { }
