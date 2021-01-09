import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NextFitnessReportDetailsComponent } from './next-fitness-report-details.component';

describe('NextFitnessReportDetailsComponent', () => {
  let component: NextFitnessReportDetailsComponent;
  let fixture: ComponentFixture<NextFitnessReportDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NextFitnessReportDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NextFitnessReportDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
