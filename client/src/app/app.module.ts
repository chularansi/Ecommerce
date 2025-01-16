import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideHttpClient, withInterceptors } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { HomeModule } from './home/home.module';
import { errorInterceptor } from './core/interceptors/error.interceptor';
import { loadingInterceptor } from './core/interceptors/loading.interceptor';
import {
  BrowserCacheLocation,
  InteractionType,
  PublicClientApplication,
} from '@azure/msal-browser';
import {
  MsalGuardConfiguration,
  MsalInterceptorConfiguration,
  MsalModule,
  MsalRedirectComponent,
} from '@azure/msal-angular';

export function MSALInstanceFactory(): PublicClientApplication {
  return new PublicClientApplication({
    auth: {
      clientId: '85ec0233-0ecb-4830-96f5-12d00bf87176',
      authority:
        'https://sportscenter19.b2clogin.com/sportscenter19.onmicrosoft.com/B2C_1_SignInSignUp/v2.0/',
      redirectUri: 'http://localhost:4200',
      knownAuthorities: ['sportscenter19.b2clogin.com'],
    },
    cache: {
      cacheLocation: BrowserCacheLocation.LocalStorage,
      storeAuthStateInCookie: false,
    },
  });
}

// MSAL Guard configuration
export function MSALGuardConfigFactory(): MsalGuardConfiguration {
  return {
    interactionType: InteractionType.Redirect,
    authRequest: {
      scopes: [
        'openid',
        'profile',
        'https://sportscenter19.onmicrosoft.com/85ec0233-0ecb-4830-96f5-12d00bf87176/access_as_user',
      ],
    },
  };
}

// MSAL interceptor configuration
export function MSALInterceptorConfigFactory(): MsalInterceptorConfiguration {
  return {
    interactionType: InteractionType.Redirect,
    protectedResourceMap: new Map([
      [
        'https://sportscenter19.onmicrosoft.com/85ec0233-0ecb-4830-96f5-12d00bf87176',
        [
          'https://sportscenter19.onmicrosoft.com/85ec0233-0ecb-4830-96f5-12d00bf87176/access_as_user',
        ],
      ],
    ]),
  };
}

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MsalModule.forRoot(
      MSALInstanceFactory(),
      MSALGuardConfigFactory(),
      MSALInterceptorConfigFactory()
    ),
    AppRoutingModule,
    CoreModule,
    HomeModule,
  ],
  providers: [
    provideHttpClient(withInterceptors([errorInterceptor, loadingInterceptor])),
  ],
  bootstrap: [AppComponent, MsalRedirectComponent], // Include MsalRedirectComponent in the bootstrap array],
})
export class AppModule {}
