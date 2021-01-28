import { Component, OnInit, Input, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgbModal, ModalDismissReasons, NgbModalRef, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { VehicleTypeMasterDataModel } from 'models/vehicle/vehicle-type-master-data.model';
import { DropDownModel } from 'models/common/drop-down.modal';
import { VehicleService } from 'services/vehicle/vehicle.service';
import { VehicleMessageService } from 'services/vehicle/vehicle-message.service';
import { VehicleTypeModel } from 'models/vehicle/vehicle-type.model';

@Component({
  selector: 'app-vehicle-type-form',
  templateUrl: './vehicle-type-form.component.html',
  styleUrls: ['./vehicle-type-form.component.scss']
})
export class VehicleTypeFormComponent implements OnInit {

  vehicleTypeRegisterForm: FormGroup;

  isSuccess: boolean;
  message: string;

  masterData: VehicleTypeMasterDataModel;

  fuelTypes: DropDownModel[];

  vehicleTypeId = 0;

  vehicleType: VehicleTypeModel;

  @Input() public data:any;

  constructor(private vehicleServices: VehicleService,
    private formBuilder: FormBuilder,
    private toastrService: ToastrService,
    private vehicleMessageService: VehicleMessageService,
    public modal: NgbActiveModal
  ) { }

  ngOnInit() {
    this.setForm();
    this.getMasterData();
    this.vehicleTypeId = this.data;
    console.log('data- ' + this.data);
  }

  loadData() {
    if (this.vehicleTypeId === 0) {
      this.setForm();
    } else {
      this.getVehicleTypeById(this.vehicleTypeId);
    }
  }

  setForm() {
    this.vehicleTypeRegisterForm = this.formBuilder.group({
      id: [this.vehicleTypeId],
      name: ['', Validators.required],
      engineOilChangeMilage: [0],
      fuelFilterChangeMilage: [0],
      gearBoxChangeMilage: [0],
      hasDifferentialOil: [false],
      differentialOilChangeMilage: [0],
      differentialOilId: [0],
      fuelFilterNumber: [''],
      airCleanerAge: [6],
      hasGreeceNipple: [false],
      greeceNipleAge: [6],
      insuranceAge: [12, Validators.required],
      hasFitnessReport: [false],
      fitnessReportAge: [12],
      emitionTestAge: [12, Validators.required],
      revenueLicenceAge: [12, Validators.required],
      fuelType: [null, Validators.required],
      fuelTypeName: [],
      breakOilId: [0],
      engineCoolantId: [0],
      engineOilId: [0],
      gearBoxOilId: [0],
      powerSteeringOilId: [0],
    });
  }

  getMasterData() {
    this.vehicleServices.getVehicleTypeMasterData().subscribe(response => {

      this.masterData = response;
      this.fuelTypes = response.fuelTypes;
      /*       this.vehicleTypeRegisterForm.controls['fuelType'].setValue(this.masterData.fuelTypes[0].id);
            this.vehicleTypeRegisterForm.controls['breakOilId'].setValue(this.masterData.breakOilTypes[0].id);
            this.vehicleTypeRegisterForm.controls['differentialOilId'].setValue(this.masterData.differentialOilTypes[0].id);
            this.vehicleTypeRegisterForm.controls['engineCoolantId'].setValue(this.masterData.coolantsTypes[0].id);
            this.vehicleTypeRegisterForm.controls['engineOilId'].setValue(this.masterData.engineOilTypes[0].id);
            this.vehicleTypeRegisterForm.controls['gearBoxOilId'].setValue(this.masterData.gearBoxOilTypes[0].id);
            this.vehicleTypeRegisterForm.controls['powerSteeringOilId'].setValue(this.masterData.powerSteeringOilTypes[0].id); */
      this.loadData();
    });
  }

  getVehicleTypeById(id:number) {
    this.vehicleServices.getVehicleTypeById(id).subscribe(res => {
      this.vehicleType = res;
      this.vehicleTypeRegisterForm.setValue(res);
      console.log('res=> ' + this.vehicleTypeRegisterForm.value);
    });
  }

  onSubmit() {
    if (this.vehicleTypeRegisterForm.value.id === 0) {
      this.vehicleServices.addNewVehicleType(this.vehicleTypeRegisterForm.value).
        subscribe(res => {
          this.isSuccess = res.isSuccess;
          this.message = res.message;
          if (this.isSuccess = true) {
            this.toastrService.success(this.message);
            this.vehicleMessageService.sendMessageToReloadVehicleTypesTable(true);
            this.modal.close('Ok Close');
          } else {
            this.toastrService.error(this.message);
          }
        });
    } else if (this.vehicleTypeRegisterForm.value.id !== 0) {
      this.vehicleServices.updateVehicleType(this.vehicleTypeRegisterForm.value).
        subscribe(res => {
          this.isSuccess = res.isSuccess;
          this.message = res.message;
          if (this.isSuccess = true) {
            this.toastrService.success(this.message);
            this.vehicleMessageService.sendMessageToReloadVehicleTypesTable(true);
            this.modal.close('Ok Close');
          } else {
            this.toastrService.error(this.message);
          }
        });
    }
  }

  get hasDifferentialOil() {
    return this.vehicleTypeRegisterForm.controls['hasDifferentialOil'].value;
  }

  get hasGreeceNipple() {
    return this.vehicleTypeRegisterForm.controls['hasGreeceNipple'].value;
  }

  get hasFitnessReport() {
    return this.vehicleTypeRegisterForm.controls['hasFitnessReport'].value;
  }

}
