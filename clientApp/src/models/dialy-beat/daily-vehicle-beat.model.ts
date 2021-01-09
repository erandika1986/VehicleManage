export class DailyVehicleBeatModel {
    id: number;
    vehicleId: number;
    vehicleNumber: string;

    routeId: number;
    route: string;

    date: Date;
    startingMilage: number;
    endMilage: number;
    status: number;
    statusInText: string;
    createdOn?: Date;
    updatedOn?: Date;
    isActive: boolean;

}
