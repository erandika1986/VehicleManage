import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FuelFilterDetailComponent } from './fuel-filter-detail.component';

describe('FuelFilterDetailComponent', () => {
  let component: FuelFilterDetailComponent;
  let fixture: ComponentFixture<FuelFilterDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FuelFilterDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FuelFilterDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
