import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { RouteService } from 'services/route/route.service';
import { RouteMessageService } from 'services/route/route-message.service';

@Component({
  selector: 'app-route-detail',
  templateUrl: './route-detail.component.html',
  styleUrls: ['./route-detail.component.scss']
})
export class RouteDetailComponent implements OnInit {

  @Input() public routeId;
  @Input() public header;
  routeForm: FormGroup;

  constructor(
    public modal: NgbActiveModal,
    private routeService: RouteService,
    private formBuilder: FormBuilder,
    private spinner: NgxSpinnerService,
    private toastrService: ToastrService,
    private routeMessageService: RouteMessageService
  ) { }

  ngOnInit() {
    this.setForm();
    if (this.routeId !== 0) {
      this.getRouteDetails();
    }
  }


  setForm() {
    this.routeForm = this.formBuilder.group({
      id: [this.routeId],
      routeCode: ['', Validators.required],
      startFrom: ['', Validators.required],
      endFrom: ['', Validators.required],
      totalDistance: [0, Validators.required],
      isActive: [true, Validators.required],
    });
  }

  onSubmit() {
    if (this.routeId === 0) {
      this.spinner.show();
      this.routeService.addNewRoute(this.routeForm.value)
        .subscribe(response => {
          this.spinner.hide();
          if (response.isSuccess) {
            this.toastrService.success(response.message, "Success");
            this.routeMessageService.sendModelSaveMessasge(true);
            this.modal.close("Model Closed");
          }
          else {
            this.toastrService.error(response.message, "Error");
          }
        }, error => {
          this.toastrService.error("Error has been occured.Please try again.", "Error");
          this.spinner.hide();
        })
    }
  }

  getRouteDetails() {

    this.spinner.show();
    this.routeService.getRouteById(this.routeId).subscribe(response => {
      this.spinner.hide();
      this.routeForm.setValue(response);
    }, error => {
      this.spinner.hide();
    });
  }

  get routeCode() {
    return this.routeForm.get('routeCode');
  }

  get startFrom() {
    return this.routeForm.get('startFrom');
  }

  get endFrom() {
    return this.routeForm.get('endFrom');
  }



}
