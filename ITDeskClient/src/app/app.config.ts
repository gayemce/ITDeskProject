import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import { HttpErrorResponse, provideHttpClient } from '@angular/common/http';
import { GoogleLoginProvider, SocialAuthServiceConfig, SocialLoginModule } from '@abacritt/angularx-social-login';

export const appConfig: ApplicationConfig = {
  providers: [provideHttpClient(), 
    provideRouter(routes), 
    importProvidersFrom(
      [
        BrowserAnimationsModule, 
        SocialLoginModule
      ]),
      {
        provide: 'SocialAuthServiceConfig',
        useValue: {
          autoLogin: false,
          providers: [
            {
              id: GoogleLoginProvider.PROVIDER_ID,
              provider: new GoogleLoginProvider(
                '1015976538486-skmmel56qcjd1s3muha8smgmq1kjf0gr.apps.googleusercontent.com'
              )
            }
          ],
          onError: (err: HttpErrorResponse) => {
            console.error(err);
          }
        } as SocialAuthServiceConfig,
      }
    
    ]
};
