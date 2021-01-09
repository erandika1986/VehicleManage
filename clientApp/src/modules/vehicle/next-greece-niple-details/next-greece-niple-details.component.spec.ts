import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NextGreeceNipleDetailsComponent } from './next-greece-niple-details.component';

describe('NextGreeceNipleDetailsComponent', () => {
  let component: NextGreeceNipleDetailsComponent;
  let fixture: ComponentFixture<NextGreeceNipleDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NextGreeceNipleDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NextGreeceNipleDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
