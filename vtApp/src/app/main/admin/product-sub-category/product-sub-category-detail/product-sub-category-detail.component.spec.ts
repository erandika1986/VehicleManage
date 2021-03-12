import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductSubCategoryDetailComponent } from './product-sub-category-detail.component';

describe('ProductSubCategoryDetailComponent', () => {
  let component: ProductSubCategoryDetailComponent;
  let fixture: ComponentFixture<ProductSubCategoryDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductSubCategoryDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductSubCategoryDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
