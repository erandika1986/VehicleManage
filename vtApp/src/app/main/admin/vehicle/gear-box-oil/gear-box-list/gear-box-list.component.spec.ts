import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GearBoxListComponent } from './gear-box-list.component';

describe('GearBoxListComponent', () => {
  let component: GearBoxListComponent;
  let fixture: ComponentFixture<GearBoxListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GearBoxListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GearBoxListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
