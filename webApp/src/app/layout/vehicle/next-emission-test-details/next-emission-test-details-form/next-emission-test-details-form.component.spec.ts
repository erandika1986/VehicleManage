import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NextEmissionTestDetailsFormComponent } from './next-emission-test-details-form.component';

describe('NextEmissionTestDetailsFormComponent', () => {
  let component: NextEmissionTestDetailsFormComponent;
  let fixture: ComponentFixture<NextEmissionTestDetailsFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NextEmissionTestDetailsFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NextEmissionTestDetailsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
