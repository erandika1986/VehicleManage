import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EngineOilListComponent } from './engine-oil-list.component';

describe('EngineOilListComponent', () => {
  let component: EngineOilListComponent;
  let fixture: ComponentFixture<EngineOilListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EngineOilListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EngineOilListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
