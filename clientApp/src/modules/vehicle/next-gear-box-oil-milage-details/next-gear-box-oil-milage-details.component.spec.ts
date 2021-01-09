import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NextGearBoxOilMilageDetailsComponent } from './next-gear-box-oil-milage-details.component';

describe('NextGearBoxOilMilageDetailsComponent', () => {
  let component: NextGearBoxOilMilageDetailsComponent;
  let fixture: ComponentFixture<NextGearBoxOilMilageDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NextGearBoxOilMilageDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NextGearBoxOilMilageDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
