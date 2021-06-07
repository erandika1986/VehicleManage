import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EngineOilDetailComponent } from './engine-oil-detail.component';

describe('EngineOilDetailComponent', () => {
  let component: EngineOilDetailComponent;
  let fixture: ComponentFixture<EngineOilDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EngineOilDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EngineOilDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
