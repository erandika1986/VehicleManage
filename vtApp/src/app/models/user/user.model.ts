import { Role } from './role.mode';

export class User {
    id: number;
    firstName: string;
    lastName: string;
    userName: string;
    email: string;
    mobileNo: string;
    password: string;
    isActive: boolean;
    role: Role[];
}