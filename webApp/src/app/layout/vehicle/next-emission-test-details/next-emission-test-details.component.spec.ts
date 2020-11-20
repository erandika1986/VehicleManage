import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NextEmissionTestDetailsComponent } from './next-emission-test-details.component';

describe('NextEmissionTestDetailsComponent', () => {
  let component: NextEmissionTestDetailsComponent;
  let fixture: ComponentFixture<NextEmissionTestDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NextEmissionTestDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NextEmissionTestDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
