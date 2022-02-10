import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExpensesDetailComponent } from './expenses-detail/expenses-detail.component';
import { ExpensesListComponent } from './expenses-list/expenses-list.component';
import { MainComponent } from './sidebars/main/main.component';
import { Routes } from '@angular/router';
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
    ExpensesDetailComponent,
    ExpensesListComponent,
    MainComponent
  ],
  imports: [

  CommonModule
  ]
})
export class ExpensesModule { }
