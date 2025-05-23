import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { AccountRoutingModule } from './account-routing.module';
import { MsalModule } from '@azure/msal-angular';

@NgModule({
  declarations: [LoginComponent],
  imports: [CommonModule, AccountRoutingModule, MsalModule],
})
export class AccountModule {}
