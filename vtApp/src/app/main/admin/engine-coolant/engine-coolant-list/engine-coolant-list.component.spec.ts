import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EngineCoolantListComponent } from './engine-coolant-list.component';

describe('EngineCoolantListComponent', () => {
  let component: EngineCoolantListComponent;
  let fixture: ComponentFixture<EngineCoolantListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EngineCoolantListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EngineCoolantListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
