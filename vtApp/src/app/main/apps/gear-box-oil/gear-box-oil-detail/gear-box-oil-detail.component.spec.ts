import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GearBoxOilDetailComponent } from './gear-box-oil-detail.component';

describe('GearBoxOilDetailComponent', () => {
  let component: GearBoxOilDetailComponent;
  let fixture: ComponentFixture<GearBoxOilDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GearBoxOilDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GearBoxOilDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
