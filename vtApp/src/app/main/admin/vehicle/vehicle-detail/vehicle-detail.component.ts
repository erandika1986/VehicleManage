import { Location } from '@angular/common';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { DropDownModel } from 'app/models/common/drop-down.modal';
import { VehicleMasterDataModel } from 'app/models/vehicle/vehicle-master-data.model';
import { VehicleModel } from 'app/models/vehicle/vehicle.model';
import { VehicleService } from 'app/services/vehicle/vehicle.service';

@Component({
  selector: 'app-vehicle-detail',
  templateUrl: './vehicle-detail.component.html',
  styleUrls: ['./vehicle-detail.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class VehicleDetailComponent implements OnInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';
  
  vehicleId:number;
  masterData: VehicleMasterDataModel = new VehicleMasterDataModel();
  vehicle:VehicleModel;
  vehicleTypes:DropDownModel[];
  productionYears:DropDownModel[];
  statuses:any[]=[{id:true,name:'Yes'},{id:false,name:'No'}];
  pageType: string;
  vehicleForm: FormGroup;
  
  constructor(
    private vehicleService: VehicleService, 
    private _route: ActivatedRoute,
    private _fuseProgressBarService: FuseProgressBarService,
    public _activateRoute: ActivatedRoute,
    private _snackBar: MatSnackBar,
    private _location: Location,
    private _formBuilder: FormBuilder,
    public _router: Router) { }

  ngOnInit(): void {
    this._activateRoute.params.subscribe(params => {
      this.vehicleId = +params.id;
      this.pageType = this.vehicleId === 0 ? 'new' : 'edit';
      this.vehicleForm = this.createNewVehicleForm();

      //this.isview = params.isview == "true" ? true : false;
      //this.getEquipmentPhotos();
      /*       this.getTotalNoOfReports();
      
            this.dataSource = new BasicEquipmentReportDataSource(this._equipmentService);
      
            this.dataSource.getEquipmentReports('asc', 0, 10, this.equipmentId); */

            this.getMasterData();
    });
  }

  back()
  {
    this._location.back();
  }

  getMasterData() {
    this._fuseProgressBarService.show();
    this.vehicleService.getVehicleMasterData()
      .subscribe(response => {
        this._fuseProgressBarService.hide();
        this.masterData = response;
        this.vehicleTypes = response.vehicleTypes;
        this.productionYears = response.productionYears;
        if(this.vehicleId!=0)
        {
          this.getVehicleDetail();
        }

      }, error => {
        this._fuseProgressBarService.hide();
      });
  }

  getVehicleDetail()
  {
    this._fuseProgressBarService.show();
    this.vehicleService.getVehicleById(this.vehicleId)
      .subscribe(response=>{
        this._fuseProgressBarService.hide();
          this.vehicle = response;
          this.vehicleForm = this.createExistingVehicleForm(this.vehicle);
          
      },error=>{
        this._fuseProgressBarService.hide();
      });
  }

  createNewVehicleForm(): FormGroup {
    return this._formBuilder.group({
      id: [0],
      registrationNo:['',Validators.required],
      initialOdometerReading:[0,Validators.required],
      vehicelType:[0,Validators.required],
      productionYear:[0,Validators.required],
      isActive:[true,Validators.required],
    });
  }

  createExistingVehicleForm(vehicle:VehicleModel): FormGroup {
    return this._formBuilder.group({
      id: [vehicle.id],
      registrationNo:[vehicle.registrationNo,Validators.required],
      initialOdometerReading:[vehicle.initialOdometerReading,Validators.required],
      vehicelType:[vehicle.vehicelType,Validators.required],
      productionYear:[vehicle.productionYear,Validators.required],
      isActive:[vehicle.isActive,Validators.required],
    });
  }

  saveVehicle()
  {
    this._fuseProgressBarService.show();
    this.vehicleService.saveVehicle(this.vehicleForm.getRawValue())
      .subscribe(response=>{
        this._fuseProgressBarService.hide();
        if (response.isSuccess) {
          this._snackBar.open(response.message, 'Success', {
            duration: 2500,
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition,
          });
          if(this.vehicleId==0)
          {
            this.vehicleId = response.id;
            this.getVehicleDetail();
          }
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
      })
  }

  get hasDifferentialOil() {
    return this.vehicle && this.vehicle.hasDifferentialOil?this.vehicle.hasDifferentialOil:false;
  }

  get hasFitnessReport() {
    return this.vehicle && this.vehicle.hasFitnessReport?this.vehicle.hasFitnessReport:false;
  }

  get hasGreeceNipple() {
    return this.vehicle && this.vehicle.hasGreeceNipple?this.vehicle.hasGreeceNipple:false;
  }
}
