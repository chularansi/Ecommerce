import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Basket, BasketItem, BasketTotal } from '../shared/models/basket';
import { Product } from '../shared/models/product';
import { AccountService } from '../account/account.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class BasketService {
  baseUrl = 'http://localhost:8001/api/v1/';

  constructor(
    private http: HttpClient,
    private accountService: AccountService,
    private router: Router
  ) {}

  private basketSource = new BehaviorSubject<Basket | null>(null);
  public basketSource$ = this.basketSource.asObservable();
  private basketTotal = new BehaviorSubject<BasketTotal | null>(null);
  public basketTotal$ = this.basketTotal.asObservable();

  getBasket(basket_username: string) {
    return this.http
      .get<Basket>(this.baseUrl + 'Basket/GetBasket/chula')
      .subscribe({
        //update the basketsource so that via observable these values will be available to the subscribers via component
        next: (response) => {
          this.basketSource.next(response);
          this.calculateBasketTotal();
        },
      });
  }

  setBasket(basket: Basket) {
    return this.http
      .post<Basket>(this.baseUrl + 'Basket/CreateBasket', basket)
      .subscribe({
        next: (response) => {
          // if basket is created newly or updated existing one in the backend successfully,
          // update the basketSource in the client side
          this.basketSource.next(response);
          this.calculateBasketTotal();
        },
      });
  }

  async checkoutBasket(basket: Basket) {
    const token = await this.accountService.authorizationHeaderValue;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: token,
      }),
    };
    return this.http
      .post<Basket>(
        'http://localhost:8001/api/v2/Basket/Checkout',
        basket,
        httpOptions
      )
      .subscribe({
        next: (basket) => {
          this.basketSource.next(null);
          this.router.navigateByUrl('/');
        },
      });
  }

  getCurrentBasket() {
    return this.basketSource.value;
  }

  private addOrUpdateItem(
    items: BasketItem[],
    itemToAdd: BasketItem,
    quantity: number
  ): BasketItem[] {
    //if we have the item in basket which matches the Id, then we can get here
    const item = items.find((x) => x.productId == itemToAdd.productId);
    if (item) {
      item.quantity += quantity;
    } else {
      itemToAdd.quantity = quantity;
      //then add the items in the basket
      items.push(itemToAdd);
    }

    return items;
  }

  private createBasket(): Basket {
    //since we have created class
    const basket = new Basket();
    localStorage.setItem('basket_username', basket.userName); //TODO: chula can be replaced with LoggedIn User
    return basket;
  }

  private mapProductItemToBasketItem(item: Product): BasketItem {
    return {
      productId: item.id,
      productName: item.name,
      price: item.price,
      imageFile: item.imageFile,
      quantity: 0,
    };
  }

  private calculateBasketTotal() {
    const basket = this.getCurrentBasket();
    if (!basket) return;
    //We are going to loop over in array and calculate total
    const total = basket.items.reduce(
      (sum, item) => item.price * item.quantity + sum,
      0
    );
    this.basketTotal.next({ total });
  }

  // Basket operations starts here
  addItemToBasket(item: Product, quantity = 1) {
    const basket = this.getCurrentBasket() ?? this.createBasket();
    const itemToAdd: BasketItem = this.mapProductItemToBasketItem(item);
    //now items can be added in the basket
    basket.items = this.addOrUpdateItem(basket.items, itemToAdd, quantity);
    this.setBasket(basket);
  }

  incrementItemQuantity(item: BasketItem) {
    const basket = this.getCurrentBasket();
    if (!basket) return;
    const founItemIndex = basket.items.findIndex(
      (x) => x.productId === item.productId
    );
    basket.items[founItemIndex].quantity++;
    this.setBasket(basket);
  }

  removeItemFromBasket(item: BasketItem) {
    const basket = this.getCurrentBasket();
    if (!basket) return;
    if (basket.items.some((x) => x.productId === item.productId)) {
      basket.items = basket.items.filter((x) => x.productId !== item.productId);
      if (basket.items.length > 0) {
        this.setBasket(basket);
      } else {
        this.deleteBasket(basket.userName);
      }
    }
  }

  deleteBasket(userName: string) {
    return this.http
      .delete(this.baseUrl + '/Basket/DeleteBasket/' + userName)
      .subscribe({
        next: (response) => {
          this.basketSource.next(null);
          this.basketTotal.next(null);
          localStorage.removeItem('basket_username');
        },
        error: (err) => {
          console.log('Error Occurred while deleting basket');
          console.log(err);
        },
      });
  }

  decrementItemQuantity(item: BasketItem) {
    const basket = this.getCurrentBasket();
    if (!basket) return;
    const founItemIndex = basket.items.findIndex(
      (x) => x.productId === item.productId
    );
    if (basket.items[founItemIndex].quantity > 1) {
      basket.items[founItemIndex].quantity--;
      this.setBasket(basket);
    } else {
      this.removeItemFromBasket(item);
    }
  }

  // Basket operations ends here
}
