import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NextFuelFilterMilageDetailsFormComponent } from './next-fuel-filter-milage-details-form.component';

describe('NextFuelFilterMilageDetailsFormComponent', () => {
  let component: NextFuelFilterMilageDetailsFormComponent;
  let fixture: ComponentFixture<NextFuelFilterMilageDetailsFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NextFuelFilterMilageDetailsFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NextFuelFilterMilageDetailsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
