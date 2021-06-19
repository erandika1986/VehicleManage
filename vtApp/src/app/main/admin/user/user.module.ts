import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserListComponent } from './user-list/user-list.component';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { RouterModule, Routes } from '@angular/router';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseSidebarModule, FuseWidgetModule } from '@fuse/components';
import { MaterialModule } from 'app/MaterialModule';
import { SeletedBarComponent } from './seleted-bar/seleted-bar.component';
import { UsersComponent } from './users.component';
import { UserService } from 'app/services/user/user.service';
import { MainComponent } from './sidebars/main/main.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
    pathMatch: 'full'
  },
  {
    path: 'list',
    component: UsersComponent

  }
];

@NgModule({
  declarations: [
    UserListComponent,
    UserDetailComponent,
    SeletedBarComponent,
    UsersComponent,
    MainComponent
  ],
  imports: [
    RouterModule.forChild(routes),

    
    CommonModule,
    FuseSharedModule,
    FuseWidgetModule,
    FuseSidebarModule,
    //FuseSharedModule,
    FuseWidgetModule,
    MaterialModule
  ],
  providers :[UserService],
  entryComponents: [
    UserDetailComponent
]
})
export class UserModule { }
