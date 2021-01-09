import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BeatDetailComponent } from './beat-detail.component';

describe('BeatDetailComponent', () => {
  let component: BeatDetailComponent;
  let fixture: ComponentFixture<BeatDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BeatDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BeatDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
