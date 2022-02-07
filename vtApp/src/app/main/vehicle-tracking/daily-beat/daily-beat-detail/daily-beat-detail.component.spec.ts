import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DailyBeatDetailComponent } from './daily-beat-detail.component';

describe('DailyBeatDetailComponent', () => {
  let component: DailyBeatDetailComponent;
  let fixture: ComponentFixture<DailyBeatDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DailyBeatDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DailyBeatDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
