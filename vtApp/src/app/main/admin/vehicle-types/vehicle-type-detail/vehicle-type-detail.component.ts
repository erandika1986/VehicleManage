import { Location } from '@angular/common';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { DropDownModel } from 'app/models/common/drop-down.modal';
import { VehicleTypeMasterDataModel } from 'app/models/vehicle/vehicle-type-master-data.model';
import { VehicleTypeModel } from 'app/models/vehicle/vehicle-type.model';
import { VehicleTypeService } from 'app/services/vehicle/vehicle-type.service';

@Component({
  selector: 'app-vehicle-type-detail',
  templateUrl: './vehicle-type-detail.component.html',
  styleUrls: ['./vehicle-type-detail.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class VehicleTypeDetailComponent implements OnInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';
  
  masterData: VehicleTypeMasterDataModel = new VehicleTypeMasterDataModel();
  vehicleType:VehicleTypeModel;
  pageType: string;
  vehicleTypeId:number;
  vehicleTypeForm: FormGroup;

  fuelTypes:DropDownModel[];
  differentialOilTypes:DropDownModel[];
  gearBoxOilTypes:DropDownModel[];
  engineOilTypes:DropDownModel[];
  breakOilTypes:DropDownModel[];
  engineCoolantTypes:DropDownModel[];
  powerSteeringOilTypes:DropDownModel[];
  permissionId: number;
  isview: boolean = false;

  constructor(private vehicleTypeService: VehicleTypeService, private _route: ActivatedRoute,
    private _fuseProgressBarService: FuseProgressBarService,
    public _activateRoute: ActivatedRoute,
    private _snackBar: MatSnackBar,
    private _location: Location,
    private _formBuilder: FormBuilder,
    public _router: Router) { }

  ngOnInit(): void {

    this._activateRoute.params.subscribe(params => {
      this.vehicleTypeId = +params.id;
      this.pageType = this.vehicleTypeId === 0 ? 'new' : 'edit';
      this.vehicleTypeForm = this.createNewVehicleTypeForm();

      //this.isview = params.isview == "true" ? true : false;
      //this.getEquipmentPhotos();
      /*       this.getTotalNoOfReports();
      
            this.dataSource = new BasicEquipmentReportDataSource(this._equipmentService);
      
            this.dataSource.getEquipmentReports('asc', 0, 10, this.equipmentId); */

            this.getMasterData();
    });
  }

  getMasterData() {
    this._fuseProgressBarService.show();
    this.vehicleTypeService.getVehicleTypeMasterData()
      .subscribe(response => {
        this._fuseProgressBarService.hide();
        this.masterData = response;
        this.fuelTypes = response.fuelTypes;
        this.differentialOilTypes=response.differentialOilTypes;
        this.gearBoxOilTypes= response.gearBoxOilTypes;
        this.engineOilTypes=response.engineOilTypes;
        this.breakOilTypes=response.breakOilTypes;
        this.engineCoolantTypes=response.coolantsTypes;
        this.powerSteeringOilTypes=response.powerSteeringOilTypes;
        if(this.vehicleTypeId!=0)
        {
          this.getVehicleTypeDetail();
        }

      }, error => {
        this._fuseProgressBarService.hide();
      });
  }

  getVehicleTypeDetail()
  {
    this._fuseProgressBarService.show();
    this.vehicleTypeService.getVehicleTypeById(this.vehicleTypeId)
      .subscribe(response=>{
        this._fuseProgressBarService.hide();
          this.vehicleType = response;
          this.vehicleTypeForm = this.createExistingVehicleTypeForm(this.vehicleType);
          
      },error=>{
        this._fuseProgressBarService.hide();
      });
  }


  createNewVehicleTypeForm(): FormGroup {
    return this._formBuilder.group({
      id: [0],
      name: ['',Validators.required],
      engineOilChangeMilage: [0,Validators.required],
      engineOilId: [0,Validators.required],
      fuelFilterChangeMilage: [0,Validators.required],
      gearBoxChangeMilage: [0,Validators.required],
      gearBoxOilId: [0,Validators.required],
      hasDifferentialOil: [false],
      differentialOilChangeMilage: [0],
      differentialOilId: [0],
      fuelFilterNumber: [''],
      airCleanerAge: [0],
      hasGreeceNipple: [false],
      greeceNipleAge: [0],
      insuranceAge: [12],
      hasFitnessReport: [false],
      fitnessReportAge: [12],
      emitionTestAge: [12],
      revenueLicenceAge: [1],
      fuelType: [1],
      fuelTypeName: [0],
      breakOilId: [0,Validators.required],
      engineCoolantId: [0,Validators.required],
      powerSteeringOilId: [0,Validators.required],
      gearBoxOilNumber: [''],
      differentialOilNumber: [''],
      engineOilNumber: [''],
    });
  }

  createExistingVehicleTypeForm(vehicleType:VehicleTypeModel): FormGroup {
    return this._formBuilder.group({
      id: [vehicleType.id],
      name: [vehicleType.name],
      engineOilChangeMilage: [vehicleType.engineOilChangeMilage],
      engineOilId: [vehicleType.engineOilId],
      fuelFilterChangeMilage: [vehicleType.fuelFilterChangeMilage],
      gearBoxChangeMilage: [vehicleType.gearBoxChangeMilage],
      gearBoxOilId: [vehicleType.gearBoxOilId],
      hasDifferentialOil: [vehicleType.hasDifferentialOil],
      differentialOilChangeMilage: [vehicleType.differentialOilChangeMilage],
      differentialOilId: [vehicleType.differentialOilId],
      fuelFilterNumber: [vehicleType.fuelFilterNumber],
      airCleanerAge: [vehicleType.airCleanerAge],
      hasGreeceNipple: [vehicleType.hasGreeceNipple],
      greeceNipleAge: [vehicleType.greeceNipleAge],
      insuranceAge: [vehicleType.insuranceAge],
      hasFitnessReport: [vehicleType.hasFitnessReport],
      fitnessReportAge: [vehicleType.fitnessReportAge],
      emitionTestAge: [vehicleType.emitionTestAge],
      revenueLicenceAge: [vehicleType.revenueLicenceAge],
      fuelType: [vehicleType.fuelType],
      fuelTypeName: [vehicleType.fuelTypeName],
      breakOilId: [vehicleType.breakOilId],
      engineCoolantId: [vehicleType.engineCoolantId],
      powerSteeringOilId: [vehicleType.powerSteeringOilId],
      gearBoxOilNumber: [vehicleType.gearBoxOilNumber],
      differentialOilNumber: [vehicleType.differentialOilNumber],
      engineOilNumber: [vehicleType.engineOilNumber],
    });
  }

  saveVahicleType()
  {
    this.vehicleTypeService.saveVehicleType(this.vehicleTypeForm.getRawValue())
      .subscribe(response=>{
        if (response.isSuccess) {
          this._snackBar.open(response.message, 'Success', {
            duration: 2500,
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition,
          });

          this._location.back();
        }
        else
        {
          this._snackBar.open(response.message, 'Error', {
            duration: 2500,
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition,
          });
        }
      },error=>{
        this._fuseProgressBarService.hide();
        this._snackBar.open("Network error has been occured. Please try again.", 'Error', {
          duration: 2500,
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
        });
      });
  }

  get hasDifferentialOil() {
    return this.vehicleTypeForm.get('hasDifferentialOil').value;
  }

  get hasFitnessReport() {
    return this.vehicleTypeForm.get('hasFitnessReport').value;
  }

  get hasGreeceNipple() {
    return this.vehicleTypeForm.get('hasGreeceNipple').value;
  }

}
