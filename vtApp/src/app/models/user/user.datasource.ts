import { DataSource } from "@angular/cdk/table";
import { UserService } from "app/services/user/user.service";
import { Observable } from "rxjs";

export class UserDataSource extends DataSource<any>
{

    constructor(
        private _userService: UserService
    )
    {
        super();
    }

    /**
     * Connect function called by the table to retrieve one stream containing the data to render.
     * @returns {Observable<any[]>}
     */
    connect(): Observable<any[]>
    {
        return this._userService.getAllUsers(0,true);
    }

    /**
     * Disconnect
     */
    disconnect(): void
    {
    }
}