import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NextGearBoxOilMilageDetailsFormComponent } from './next-gear-box-oil-milage-details-form.component';

describe('NextGearBoxOilMilageDetailsFormComponent', () => {
  let component: NextGearBoxOilMilageDetailsFormComponent;
  let fixture: ComponentFixture<NextGearBoxOilMilageDetailsFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NextGearBoxOilMilageDetailsFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NextGearBoxOilMilageDetailsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
