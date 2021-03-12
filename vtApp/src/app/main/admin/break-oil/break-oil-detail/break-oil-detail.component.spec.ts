import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BreakOilDetailComponent } from './break-oil-detail.component';

describe('BreakOilDetailComponent', () => {
  let component: BreakOilDetailComponent;
  let fixture: ComponentFixture<BreakOilDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BreakOilDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BreakOilDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
