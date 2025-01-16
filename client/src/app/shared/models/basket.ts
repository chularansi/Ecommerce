export interface BasketItem {
  productId: string;
  productName: string;
  quantity: number;
  imageFile: string;
  price: number;
}

export interface Basket {
  userName: string;
  items: BasketItem[];
  totalPrice: number;
}

export class Basket implements Basket {
  userName: string = 'chula';
  items: BasketItem[] = [];
  totalPrice: number = 0;
}

export interface BasketTotal {
  total: number;
}
