import { Component, OnInit } from '@angular/core';
import { Basket, BasketItem } from '../shared/models/basket';
import { BasketService } from '../basket/basket.service';
import { Router } from '@angular/router';
import { AccountService } from '../account/account.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.scss',
})
export class CheckoutComponent implements OnInit {
  constructor(
    public basketService: BasketService,
    private accountService: AccountService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe({
      next: (user) => {
        this.isUserAuthenticated = !!user;
        if (!this.isUserAuthenticated) {
          // If the user is not authenticated, trigger the login process
          this.accountService.login(this.router.url);
        }
      },
      error: (err) => {
        console.log(
          `An error occurred while setting isUserAuthenticated flag: ${err}`
        );
      },
    });
  }
  public isUserAuthenticated: boolean = false;

  removeBasketItem(item: BasketItem) {
    this.basketService.removeItemFromBasket(item);
  }

  incrementItem(item: BasketItem) {
    this.basketService.incrementItemQuantity(item);
  }

  decrementItem(item: BasketItem) {
    this.basketService.decrementItemQuantity(item);
  }

  orderNow(item: Basket) {
    this.basketService.checkoutBasket(item);
  }
}
