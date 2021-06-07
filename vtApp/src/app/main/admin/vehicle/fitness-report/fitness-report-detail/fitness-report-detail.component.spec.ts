import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FitnessReportDetailComponent } from './fitness-report-detail.component';

describe('FitnessReportDetailComponent', () => {
  let component: FitnessReportDetailComponent;
  let fixture: ComponentFixture<FitnessReportDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FitnessReportDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FitnessReportDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
