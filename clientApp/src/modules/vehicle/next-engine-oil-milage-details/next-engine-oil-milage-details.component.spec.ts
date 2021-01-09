import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NextEngineOilMilageDetailsComponent } from './next-engine-oil-milage-details.component';

describe('NextEngineOilMilageDetailsComponent', () => {
  let component: NextEngineOilMilageDetailsComponent;
  let fixture: ComponentFixture<NextEngineOilMilageDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NextEngineOilMilageDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NextEngineOilMilageDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
