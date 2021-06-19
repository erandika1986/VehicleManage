import { DataSource } from "@angular/cdk/table";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { UserService } from "app/services/user/user.service";
import { BehaviorSubject, Observable } from "rxjs";

export class UserDataSource extends DataSource<any>
{

    private _filterChange = new BehaviorSubject('');
    private _filteredDataChange = new BehaviorSubject('');
    
    constructor(
        private _userService: UserService,
        private _matPaginator: MatPaginator,
        private _matSort: MatSort
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

    get filteredData(): any
    {
        return this._filteredDataChange.value;
    }

    set filteredData(value: any)
    {
        this._filteredDataChange.next(value);
    }


        // Filter
        get filter(): string
        {
            return this._filterChange.value;
        }
    
        set filter(filter: string)
        {
            this._filterChange.next(filter);
        }

    /**
     * Disconnect
     */
    disconnect(): void
    {
    }
}