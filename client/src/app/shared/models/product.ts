import { Brand } from './brand';
import { Type } from './type';

export interface Product {
  id: string;
  name: string;
  summary: string;
  description: string;
  imageFile: string;
  brands: Brand;
  types: Type;
  price: number;
}
