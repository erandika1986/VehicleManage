import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NextRevenueLicenceDetailsComponent } from './next-revenue-licence-details.component';

describe('NextRevenueLicenceDetailsComponent', () => {
  let component: NextRevenueLicenceDetailsComponent;
  let fixture: ComponentFixture<NextRevenueLicenceDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NextRevenueLicenceDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NextRevenueLicenceDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
