import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { RouterModule } from '@angular/router';
import { NotFoundComponent } from './not-found/not-found.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { HeaderComponent } from './header/header.component';
import { BreadcrumbComponent, BreadcrumbItemDirective } from 'xng-breadcrumb';
import { NgxSpinnerModule } from 'ngx-spinner';

@NgModule({
  declarations: [
    NavbarComponent,
    NotFoundComponent,
    UnauthorizedComponent,
    ServerErrorComponent,
    HeaderComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    BreadcrumbComponent,
    BreadcrumbItemDirective,
    NgxSpinnerModule,
  ],
  exports: [NavbarComponent, HeaderComponent, NgxSpinnerModule],
})
export class CoreModule {}
