import { DataSource } from "@angular/cdk/table";
import { UserService } from "app/services/user/user.service";
import { BehaviorSubject, Observable } from "rxjs";

export class UserDataSource extends DataSource<any>
{

    private _filterChange = new BehaviorSubject('');
    private _filteredDataChange = new BehaviorSubject('');
    
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
        return this._userService.getAllUsers(0,0);
    }

    /**
     * Disconnect
     */
    disconnect(): void
    {
    }
}