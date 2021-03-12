import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WherehouseListComponent } from './wherehouse-list.component';

describe('WherehouseListComponent', () => {
  let component: WherehouseListComponent;
  let fixture: ComponentFixture<WherehouseListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WherehouseListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WherehouseListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
