import { Injectable } from '@angular/core';

@Injectable()
export class PaginatedItemsModel {

    currentPage: number=0;
    pageSize: number=0;
    totalPageCount: number=0;
    totalRecordCount: number=0;


}
