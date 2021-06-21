import { User } from "../user/user.model";

export class WarehouseModel
{
    id:number;
    address:string;
    phone:string;
    ManagerId:string;
    floorSpace:number;
    createOn:Date
    createById:number;
    updateOn:Date;
    updatedById:number;
    isActive:boolean;
    SelectedManagerId:number;
   
    
}