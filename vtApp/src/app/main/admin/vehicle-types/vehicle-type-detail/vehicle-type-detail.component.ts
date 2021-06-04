import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
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

  masterData: VehicleTypeMasterDataModel = new VehicleTypeMasterDataModel();
  vehicleType:VehicleTypeModel;
  pageType: string;
  vehicleTypeId:number;
  vehicleTypeForm: FormGroup;

  fuelTypes:DropDownModel[];
  permissionId: number;
  isview: boolean = false;

  constructor(private vehicleTypeService: VehicleTypeService, private _route: ActivatedRoute,
    private _fuseProgressBarService: FuseProgressBarService,
    public _activateRoute: ActivatedRoute,
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
      name: [''],
      engineOilChangeMilage: [0],
      engineOilId: [0],
      fuelFilterChangeMilage: [0],
      gearBoxChangeMilage: [0],
      gearBoxOilId: [0],
      hasDifferentialOil: [true],
      differentialOilChangeMilage: [0],
      differentialOilId: [0],
      fuelFilterNumber: [''],
      airCleanerAge: [0],
      hasGreeceNipple: [false],
      greeceNipleAge: [0],
      insuranceAge: [0],
      hasFitnessReport: [false],
      fitnessReportAge: [0],
      emitionTestAge: [0],
      revenueLicenceAge: [1],
      fuelType: [1],
      fuelTypeName: [0],
      breakOilId: [0],
      engineCoolantId: [0],
      powerSteeringOilId: [0],
      gearBoxOilNumber: [''],
      differentialOilNumber: [''],
      engineOilNumber: [''],
    });
  }

  createExistingVehicleTypeForm(vehicleType:VehicleTypeModel): FormGroup {
    return this._formBuilder.group({
      id: [0],
      name: [vehicleType.id],
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
}
