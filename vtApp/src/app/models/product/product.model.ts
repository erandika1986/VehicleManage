import { ProductImageModel } from "./product.image.model";

export class ProductModel
{
    id :number;
    productCategoryId:number;
    productSubCategoryId :number;
    name :string;
    productCode :string;
    unitPrice :number;
    availableQty :number;
    maxOrderQty:number;
    minOrderQty:number;
    supplierId :number;
    description :string;
    defaultImage :string;
    isActive :boolean;

    images:ProductImageModel[];

    supplierName:string;
    categoryName:string;
    subCategoryName:string;
}