import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { DropDownModel } from 'app/models/common/drop-down.modal';
import { MasterDataCodeService } from 'app/services/vehicle/master-data-code.service';

@Component({
  selector: 'app-filter-bar',
  templateUrl: './filter-bar.component.html',
  styleUrls: ['./filter-bar.component.scss']
})
export class FilterBarComponent implements OnInit, OnDestroy, AfterViewInit {

  codeTypes: DropDownModel[] = [];
  filterForm: FormGroup;

  constructor(private _fuseProgressBarService: FuseProgressBarService,
    private _masterDataService: MasterDataCodeService) {
    this.filterForm = this.createFilterForm();
  }

  ngOnInit(): void {
    this.getAllCodeTypes();
  }

  ngOnDestroy(): void {

  }

  ngAfterViewInit() {


  }

  getAllCodeTypes() {
    this._fuseProgressBarService.show();
    this._masterDataService.getAllCodeTypes()
      .subscribe(response => {
        this._fuseProgressBarService.hide();
        this.codeTypes = response;
        this.filterForm.get('selectedCodeType').setValue(this.codeTypes[0].id);
        this._masterDataService.onFilterChanged.next(this.codeTypes[0]);
      }, error => {
        this._fuseProgressBarService.hide();
      });
  }


  createFilterForm(): FormGroup {

    return new FormGroup({
      selectedCodeType: new FormControl(0)

    });
  }

  dropdownFilterChanged() {
    for (let index = 0; index < this.codeTypes.length; index++) {
      if (this.codeTypes[index].id == this.selectedCodeType) {
        this._masterDataService.onFilterChanged.next(this.codeTypes[index]);
        break;
      }

    }

  }

  get selectedCodeType() {
    return this.filterForm.get("selectedCodeType").value;
  }

}
