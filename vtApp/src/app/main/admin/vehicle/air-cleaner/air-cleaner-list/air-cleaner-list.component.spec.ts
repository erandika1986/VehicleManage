import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AirCleanerListComponent } from './air-cleaner-list.component';

describe('AirCleanerListComponent', () => {
  let component: AirCleanerListComponent;
  let fixture: ComponentFixture<AirCleanerListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AirCleanerListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AirCleanerListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
