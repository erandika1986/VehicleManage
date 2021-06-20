import { Role } from './role.mode';

export class User {
    id: number;
    firstName: string;
    lastName: string;
    userName: string;
    email: string;
    mobileNo: string;
    personalAddress:string;
    password: string;
    image:string;
    timeZoneId:number;
    isActive: boolean;
    nicno:string;
    nicFrontImage:string;
    nicBackImage:string;
    drivingLicenceNo:string;
    drivingLicenceFrontImage:string;
    drivingLicenceBackImage:string;
    role: Role[];
    roleText:string;
}