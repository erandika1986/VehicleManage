import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FuseSharedModule } from '@fuse/shared.module';
const routes = [
  {
    path: '',
    redirectTo: 'expenses',
    pathMatch: 'full'
  },
  {
    path: 'expenses',
    loadChildren: () => import('./expenses/expenses.module').then(m => m.ExpensesModule)
  },
];



@NgModule({
  declarations: [
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    FuseSharedModule
  ]
})
export class ExpensesTrackingModule { }
