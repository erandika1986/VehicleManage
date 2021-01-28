export class DailyVehicleBeatModel {
    id: number=0;
    vehicleId: number=0;
    vehicleNumber: string="";

    routeId: number=0;
    route: string="";

    date: Date=new Date();
    startingMilage: number=0;
    endMilage: number=0;
    status: number=0;
    statusInText: string="";
    createdOn?: Date;
    updatedOn?: Date;
    isActive: boolean=false;

}
