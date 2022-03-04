import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DailyBeatsComponent } from './daily-beats.component';

describe('DailyBeatsComponent', () => {
  let component: DailyBeatsComponent;
  let fixture: ComponentFixture<DailyBeatsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DailyBeatsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DailyBeatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
