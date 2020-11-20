import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NextFuelFilterMilageDetailsComponent } from './next-fuel-filter-milage-details.component';

describe('NextFuelFilterMilageDetailsComponent', () => {
  let component: NextFuelFilterMilageDetailsComponent;
  let fixture: ComponentFixture<NextFuelFilterMilageDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NextFuelFilterMilageDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NextFuelFilterMilageDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
