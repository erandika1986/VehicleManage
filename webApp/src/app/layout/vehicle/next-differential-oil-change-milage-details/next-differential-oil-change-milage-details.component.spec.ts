import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NextDifferentialOilChangeMilageDetailsComponent } from './next-differential-oil-change-milage-details.component';

describe('NextDifferentialOilChangeMilageDetailsComponent', () => {
  let component: NextDifferentialOilChangeMilageDetailsComponent;
  let fixture: ComponentFixture<NextDifferentialOilChangeMilageDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NextDifferentialOilChangeMilageDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NextDifferentialOilChangeMilageDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
