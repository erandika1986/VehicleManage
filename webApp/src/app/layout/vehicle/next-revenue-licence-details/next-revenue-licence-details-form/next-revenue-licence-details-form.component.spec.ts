import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NextRevenueLicenceDetailsFormComponent } from './next-revenue-licence-details-form.component';

describe('NextRevenueLicenceDetailsFormComponent', () => {
  let component: NextRevenueLicenceDetailsFormComponent;
  let fixture: ComponentFixture<NextRevenueLicenceDetailsFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NextRevenueLicenceDetailsFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NextRevenueLicenceDetailsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
