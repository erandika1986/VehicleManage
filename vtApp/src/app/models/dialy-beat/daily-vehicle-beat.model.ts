export class DailyVehicleBeatModel {
    id: number;
    vehicleId: number;
    vehicleNumber: string;

    routeId: number;
    routeName:string;

    salesRepId:number;
    salesRepName:string;

    driverId:number;
    driverName:string;

    date: Date;
    startingMilage: number;
    endMilage: number;
    status: number;
    statusInText: string;
    createdOn?: Date;
    updatedOn?: Date;
    isActive: boolean;

}
