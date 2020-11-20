import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NextInsurenceRenewalDetailsFormComponent } from './next-insurence-renewal-details-form.component';

describe('NextInsurenceRenewalDetailsFormComponent', () => {
  let component: NextInsurenceRenewalDetailsFormComponent;
  let fixture: ComponentFixture<NextInsurenceRenewalDetailsFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NextInsurenceRenewalDetailsFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NextInsurenceRenewalDetailsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
