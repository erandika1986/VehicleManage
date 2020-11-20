import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NextInsurenceRenewalDetailsComponent } from './next-insurence-renewal-details.component';

describe('NextInsurenceRenewalDetailsComponent', () => {
  let component: NextInsurenceRenewalDetailsComponent;
  let fixture: ComponentFixture<NextInsurenceRenewalDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NextInsurenceRenewalDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NextInsurenceRenewalDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
