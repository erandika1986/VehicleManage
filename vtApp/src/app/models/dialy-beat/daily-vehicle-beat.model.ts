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
    dateYear:number;
    dateMonth:number;
    dateDay:number;

    startingMilage: number;
    endMilage: number;
    remarks:string;
    status: number;
    statusInText: string;
    createdOn?: Date;
    updatedOn?: Date;
    isActive: boolean;

}
