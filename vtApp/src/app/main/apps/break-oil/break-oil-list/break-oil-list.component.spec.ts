import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BreakOilListComponent } from './break-oil-list.component';

describe('BreakOilListComponent', () => {
  let component: BreakOilListComponent;
  let fixture: ComponentFixture<BreakOilListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BreakOilListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BreakOilListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
