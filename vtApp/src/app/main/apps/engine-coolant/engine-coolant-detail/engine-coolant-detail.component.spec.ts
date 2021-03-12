import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EngineCoolantDetailComponent } from './engine-coolant-detail.component';

describe('EngineCoolantDetailComponent', () => {
  let component: EngineCoolantDetailComponent;
  let fixture: ComponentFixture<EngineCoolantDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EngineCoolantDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EngineCoolantDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
