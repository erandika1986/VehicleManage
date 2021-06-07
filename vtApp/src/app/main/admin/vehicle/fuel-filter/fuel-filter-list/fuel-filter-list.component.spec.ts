import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FuelFilterListComponent } from './fuel-filter-list.component';

describe('FuelFilterListComponent', () => {
  let component: FuelFilterListComponent;
  let fixture: ComponentFixture<FuelFilterListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FuelFilterListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FuelFilterListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
