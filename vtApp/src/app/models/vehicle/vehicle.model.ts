import { VehicleFitnessReportModel } from './vehicle-fitness-report.model';
import { VehicleGreeceNipleModel } from './vehicle-greece-niple';
import { VehicleInsuranceModel } from './vehicle-insurance.model';
import { VehicleRevenueLicenceModel } from './vehicle-revenue-licence.model';
import { VehicleEmissionTestModel } from './vehicle-emission-test.model';
import { VehicleAirCleanerModel } from './vehicle-air-cleaner.model';
import { VehicleEngineOilMilageModel } from './vehicle-engine-oil-milage.model';
import { VehicleFuelFilterMilageModel } from './vehicle-fuel-filter-milage.model';
import { VehicleGearBoxOilMilageModel } from './vehicle-gear-box-oil-milage.model';
import { VehicleTypeModel } from './vehicle-type.model';
import { VehicleDifferentialOilChangeMilageModel } from './vehicle-differential-oil-change-milage.model';
import { User } from '../user/user.model';

export class VehicleModel {
    id: number;
    registrationNo: string;
    vehicelType: number;
    productionYear: number;
    initialOdometerReading: number;
    isActive: boolean;

    // For table view
    vehicelTypeName: string;

    // hasDifferentialOil: boolean;
    // nextDifferentialOilChangeMilageDetails: VehicleDifferentialOilChangeMilageModel;
    // hasFitnessReport: boolean;
    // nextFitnessReportDetails: VehicleFitnessReportModel;
    // hasGreeceNipple: boolean;
    // nextGreeceNipleDetails: VehicleGreeceNipleModel;
    // nextInsurenceRenewalDetails: VehicleInsuranceModel;
    // nextRevenueLicenceDetails: VehicleRevenueLicenceModel;
    // nextEmissionTestDetails: VehicleEmissionTestModel;
    // nextAirCleanerDetails: VehicleAirCleanerModel;
    // nextEngineOilMilageDetails: VehicleEngineOilMilageModel;
    // nextFuelFilterMilageDetails: VehicleFuelFilterMilageModel;
    // nextGearBoxOilMilageDetails: VehicleGearBoxOilMilageModel;
}
