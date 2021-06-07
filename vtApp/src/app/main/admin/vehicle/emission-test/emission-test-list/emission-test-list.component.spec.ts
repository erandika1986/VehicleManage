import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmissionTestListComponent } from './emission-test-list.component';

describe('EmissionTestListComponent', () => {
  let component: EmissionTestListComponent;
  let fixture: ComponentFixture<EmissionTestListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmissionTestListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmissionTestListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
