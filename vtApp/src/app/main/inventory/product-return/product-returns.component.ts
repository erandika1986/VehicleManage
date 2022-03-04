import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FuseSidebarService } from './../../../../@fuse/components/sidebar/sidebar.service';
import { FuseProgressBarService } from './../../../../@fuse/components/progress-bar/progress-bar.service';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { ProductReturnDetailComponent } from './product-return-detail/product-return-detail.component';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-product-returns',
  templateUrl: './product-returns.component.html',
  styleUrls: ['./product-returns.component.scss']
})
export class ProductReturnsComponent implements OnInit {

  @ViewChild('filter', {static: true})
  filter: ElementRef;

  dialogRef: any;
  private _unsubscribeAll: Subject<any>;
  hasSelectedContacts: boolean;
  constructor
  (
    private _fuseSidebarService: FuseSidebarService,
    private _fuseProgressBarService: FuseProgressBarService,
    public _router: Router,
    private _matDialog: MatDialog
  ) 
  { 
    this._unsubscribeAll = new Subject();
  }

  ngOnInit(): void {
  }

  toggleSidebar(name): void
  {
      this._fuseSidebarService.getSidebar(name).toggleOpen();
  }


  addNew()
  {
    this.dialogRef = this._matDialog.open(ProductReturnDetailComponent, {
      panelClass: 'expense-form-dialog',
      data      : {
          
      }
  });

  this.dialogRef.afterClosed()
      .subscribe((response: FormGroup) => {
          if ( !response )
          {
              return;
          }
        
      });
  }

}
