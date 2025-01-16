import { Component, OnInit } from '@angular/core';
import { BasketService } from '../../basket/basket.service';
import { BasketItem } from '../../shared/models/basket';
import { AccountService } from '../../account/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
})
export class NavbarComponent implements OnInit {
  public isUserAuthenticated: boolean = false;

  constructor(
    public basketService: BasketService,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe({
      next: (user) => {
        this.isUserAuthenticated = !!user; // true if user is logged in, false otherwise
        console.log(`User authenticated: ${this.isUserAuthenticated}`);
      },
      error: (error) => {
        console.log(
          `An error occurred while setting isUserAuthenticated flag: ${error}`
        );
      },
    });
  }

  getBasketCount(items: BasketItem[]): number {
    return items.reduce((sum, item) => sum + item.quantity, 0);
  }

  public login(): void {
    this.accountService.login();
  }

  public logout(): void {
    this.accountService.logout();
  }
}
