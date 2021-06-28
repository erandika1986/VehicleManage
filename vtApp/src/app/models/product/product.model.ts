import { ProductImageModel } from "./product.image.model";

export class ProductModel
{
    id :number;
    productCategory:string;

    productSubCategoryId :number;
    productSubCategory:string;
    
    name :string;
    productCode :string;
    unitPrice :number;
    availableQty :number;
    supplierId :number;
    description :string;
    picture :string;
    isActive :boolean;

    images:ProductImageModel[];
}