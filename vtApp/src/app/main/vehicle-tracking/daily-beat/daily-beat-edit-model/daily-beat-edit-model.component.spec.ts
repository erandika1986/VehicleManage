import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DailyBeatEditModelComponent } from './daily-beat-edit-model.component';

describe('DailyBeatEditModelComponent', () => {
  let component: DailyBeatEditModelComponent;
  let fixture: ComponentFixture<DailyBeatEditModelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DailyBeatEditModelComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DailyBeatEditModelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
