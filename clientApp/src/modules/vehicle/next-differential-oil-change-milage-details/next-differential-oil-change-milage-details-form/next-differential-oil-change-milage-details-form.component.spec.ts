import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NextDifferentialOilChangeMilageDetailsFormComponent } from './next-differential-oil-change-milage-details-form.component';

describe('NextDifferentialOilChangeMilageDetailsFormComponent', () => {
  let component: NextDifferentialOilChangeMilageDetailsFormComponent;
  let fixture: ComponentFixture<NextDifferentialOilChangeMilageDetailsFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NextDifferentialOilChangeMilageDetailsFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NextDifferentialOilChangeMilageDetailsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
