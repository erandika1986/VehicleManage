import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { VehicleService } from 'src/app/services/vehicle/vehicle.service';
import { DropDownModel } from 'src/app/models/common/drop-down.modal';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { VahicleRegNoValidator } from 'src/app/validators/vehicle-reg-no.validator';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.scss']
})
export class VehicleFormComponent implements OnInit {

  vehicleRegisterForm: FormGroup;
  vehicleTypes: DropDownModel[];
  productionYears: DropDownModel[];
  vehicleId = 0;

  constructor
    (
      private spinner: NgxSpinnerService,
      private toastr: ToastrService,
      private formBuilder: FormBuilder,
      private vehicleService: VehicleService,
      private router: Router,
      private route: ActivatedRoute,
      public vehicleRehNovalidator: VahicleRegNoValidator
    ) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.vehicleId = +params.get('id');
    });
    this.spinner.show();
    this.setForm();
    this.getVehicleTypes();
  }

  setForm() {
    const date = new Date();
    const currentYear = date.getFullYear();
    this.vehicleRegisterForm = this.formBuilder.group({
      id: [0],
      // registrationNo: [{ value: '', disabled: this.vehicleId !== 0 ? true : false }, Validators.compose([Validators.required]), this.vehicleRehNovalidator.checkVehicleRegNo.bind(this.vehicleRehNovalidator)],
      registrationNo: [{ value: '', disabled: this.vehicleId !== 0 ? true : false }, Validators.compose([Validators.required])],
      productionYear: [2019],
      vehicelType: [2, Validators.required],
      initialOdometerReading: [0, Validators.required],
      isActive: [1, Validators.required]
    });
  }


  onSubmit() {
    if (this.vehicleId === 0) {
      this.spinner.show();
      this.vehicleService.addNewVehicle(this.vehicleRegisterForm.value).subscribe(response => {
        this.spinner.hide();
        if (response.isSuccess) {
          this.toastr.success(response.message, 'Success');
          this.vehicleId = response.id;
          this.vehicleRegisterForm.get('id').setValue(response.id);
          this.vehicleRegisterForm.get('registrationNo').disable();
          this.router.navigate(['vehicle/vehicle-detail/' + response.id]);
        } else {
          this.toastr.error(response.message, 'Error');
        }


      }, error => {
        this.toastr.error('Error has been occured.Please try again.', 'Error');
        this.spinner.hide();
      });
    } else {
      this.spinner.show();
      this.vehicleService.updateVehicle(this.vehicleRegisterForm.value).subscribe(response => {

        this.spinner.hide();
        if (response.isSuccess) {

          this.toastr.success(response.message, 'Success');
        } else {
          this.toastr.error(response.message, 'Error');
        }


      }, error => {
        this.toastr.error('Error has been occured.Please try again.', 'Error');
        this.spinner.hide();
      });
    }
  }

  onCancel() {
    this.router.navigate(['vehicle/vehicles']);
  }

  getVehicleTypes() {
    this.vehicleService.getVehicleMasterData().subscribe(response => {
      this.vehicleTypes = response.vehicleTypes;
      this.productionYears = response.productionYears;
      if (this.vehicleId !== 0) {
        this.getVehicleDetails();
      } else {
        this.spinner.hide();
      }
    }, error => {
      this.spinner.hide();
    });
  }

  getVehicleDetails() {
    this.vehicleService.getVehicleById(this.vehicleId).subscribe(response => {
      this.spinner.hide();
      this.vehicleRegisterForm.setValue(response);
    }, error => {
      this.spinner.hide();
    });
  }


  get registrationNo() {
    return this.vehicleRegisterForm.get('registrationNo');
  }
}
