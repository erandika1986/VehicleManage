import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GearBoxOilListComponent } from './gear-box-oil-list.component';

describe('GearBoxOilListComponent', () => {
  let component: GearBoxOilListComponent;
  let fixture: ComponentFixture<GearBoxOilListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GearBoxOilListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GearBoxOilListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
