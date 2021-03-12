import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DifferentialOilDetailComponent } from './differential-oil-detail.component';

describe('DifferentialOilDetailComponent', () => {
  let component: DifferentialOilDetailComponent;
  let fixture: ComponentFixture<DifferentialOilDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DifferentialOilDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DifferentialOilDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
