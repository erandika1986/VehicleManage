import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FitnessReportListComponent } from './fitness-report-list.component';

describe('FitnessReportListComponent', () => {
  let component: FitnessReportListComponent;
  let fixture: ComponentFixture<FitnessReportListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FitnessReportListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FitnessReportListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
