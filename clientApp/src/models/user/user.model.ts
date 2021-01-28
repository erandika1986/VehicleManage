import { Role } from './role.mode';

export class User {
    id: number=0;
    firstName: string="";
    lastName: string="";
    userName: string="";
    email: string="";
    mobileNo: string="";
    password: string="";
    isActive: boolean=false;
    role: Role[]=[];
}