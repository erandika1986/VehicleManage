import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { RouteDetailComponent } from '../route-detail/route-detail.component';
import { Subscription } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { MatDialog } from '@angular/material/dialog';
import { RoutePaginatedItemsModel } from 'models/route/route-paginated-items.model';
import { RouteModel } from 'models/route/route.model';
import { RouteService } from 'services/route/route.service';
import { RouteMessageService } from 'services/route/route-message.service';
import { DialogData } from 'models/common/dialog-data';
import { ConfirmationDialog } from 'shared/confirmation-dialog/confirmation-dialog';

@Component({
  selector: 'app-routes',
  templateUrl: './routes.component.html',
  styleUrls: ['./routes.component.scss']
})
export class RoutesComponent implements OnInit {

  currentPageItemCount: number = 0;
  totalRecordCount: number = 0;
  totalPageCount: number = 0;
  currentPage: number = 1;
  pageSize: number = 2;
  dataContainer: RoutePaginatedItemsModel;
  rowData: RouteModel[];


  subscription: Subscription;

  constructor(
    private router: Router,
    private routeService: RouteService,
    private toastrService: ToastrService,
    private spinner: NgxSpinnerService,
    private modalService: NgbModal,
    public dialog: MatDialog,
    private routeMessageService: RouteMessageService,
  ) {

    this.subscription = this.routeMessageService.getModelSaveMessage().subscribe(response => {

      if (response.isReload) {
        this.getRouteList();
      }
    });
  }

  ngOnInit() {

    this.getRouteList();
  }

  delete(item: RouteModel, index: number) {

    let data1: DialogData = new DialogData();
    data1.header = "Please confirm.";
    data1.message = "Do you really want to delete this selected record ?";
    const dialogRef = this.dialog.open(ConfirmationDialog, {
      width: '250px',
      data: data1
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.spinner.show();
        this.rowData.splice(index, 1);
        this.routeService.delete(item.id).subscribe(response => {
          this.spinner.hide();
          if (response.isSuccess) {
            this.toastrService.success(response.message, "Success");
          }
          else {
            this.toastrService.error(response.message, "Error");
            this.getRouteList();
          }

        }, error => {
          this.spinner.hide();
          this.toastrService.error("Error has been occured.Please try again.", "Error");
        });
      }
    });


  }


  edit(id: number, index: number) {
    const modalRef = this.modalService.open(RouteDetailComponent, { centered: true, size: 'lg', backdrop: 'static' });
    modalRef.componentInstance.routeId = id;
    modalRef.componentInstance.header = id === 0 ? "Add New Route" : "Updating Route : " + this.rowData[index].routeCode;
  }

  getRouteList() {
    this.spinner.show();
    this.routeService.getAllRoutes(this.pageSize, this.currentPage)
      .subscribe(response => {
        this.spinner.hide();

        this.rowData = response.data;
        this.totalRecordCount = response.totalRecordCount;
        this.dataContainer = response;
      }, error => {
        this.spinner.hide();
      });
  }

  onPageChange() {

    this.getRouteList();

  }
}
