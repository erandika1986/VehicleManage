import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DifferentialOilListComponent } from './differential-oil-list.component';

describe('DifferentialOilListComponent', () => {
  let component: DifferentialOilListComponent;
  let fixture: ComponentFixture<DifferentialOilListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DifferentialOilListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DifferentialOilListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
