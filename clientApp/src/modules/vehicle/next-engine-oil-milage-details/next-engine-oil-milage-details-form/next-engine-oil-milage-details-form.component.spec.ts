import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NextEngineOilMilageDetailsFormComponent } from './next-engine-oil-milage-details-form.component';

describe('NextEngineOilMilageDetailsFormComponent', () => {
  let component: NextEngineOilMilageDetailsFormComponent;
  let fixture: ComponentFixture<NextEngineOilMilageDetailsFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NextEngineOilMilageDetailsFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NextEngineOilMilageDetailsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
