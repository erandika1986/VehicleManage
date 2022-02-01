import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DailyBeatListComponent } from './daily-beat-list.component';

describe('DailyBeatListComponent', () => {
  let component: DailyBeatListComponent;
  let fixture: ComponentFixture<DailyBeatListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DailyBeatListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DailyBeatListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
