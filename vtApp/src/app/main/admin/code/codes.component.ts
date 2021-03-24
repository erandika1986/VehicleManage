import { AfterViewInit, Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { FuseSidebarService } from '@fuse/components/sidebar/sidebar.service';
import { MasterDataCodeService } from 'app/services/vehicle/master-data-code.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-codes',
  templateUrl: './codes.component.html',
  styleUrls: ['./codes.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class CodesComponent implements OnInit, OnDestroy, AfterViewInit {

  // Private
  private _unsubscribeAll: Subject<any>;

  constructor(private _fuseSidebarService: FuseSidebarService, private _masterDataService: MasterDataCodeService) {

    this._unsubscribeAll = new Subject();
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this._unsubscribeAll.next();
    this._unsubscribeAll.complete();
  }

  ngAfterViewInit() {

  }

  doSearch(value) {
    //this._inspectionervice.onSearchTextChanged.next(value);
  }

  toggleSidebar(name): void {
    this._fuseSidebarService.getSidebar(name).toggleOpen();
  }

  newExpense(): void {


  }

}
