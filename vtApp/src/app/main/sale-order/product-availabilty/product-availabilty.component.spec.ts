import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductAvailabiltyComponent } from './product-availabilty.component';

describe('ProductAvailabiltyComponent', () => {
  let component: ProductAvailabiltyComponent;
  let fixture: ComponentFixture<ProductAvailabiltyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductAvailabiltyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductAvailabiltyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
