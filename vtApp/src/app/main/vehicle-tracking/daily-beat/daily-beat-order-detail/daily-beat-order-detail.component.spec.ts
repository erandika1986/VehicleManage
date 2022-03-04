import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DailyBeatOrderDetailComponent } from './daily-beat-order-detail.component';

describe('DailyBeatOrderDetailComponent', () => {
  let component: DailyBeatOrderDetailComponent;
  let fixture: ComponentFixture<DailyBeatOrderDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DailyBeatOrderDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DailyBeatOrderDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
