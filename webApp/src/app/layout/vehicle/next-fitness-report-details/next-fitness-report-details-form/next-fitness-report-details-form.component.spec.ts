import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NextFitnessReportDetailsFormComponent } from './next-fitness-report-details-form.component';

describe('NextFitnessReportDetailsFormComponent', () => {
  let component: NextFitnessReportDetailsFormComponent;
  let fixture: ComponentFixture<NextFitnessReportDetailsFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NextFitnessReportDetailsFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NextFitnessReportDetailsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
