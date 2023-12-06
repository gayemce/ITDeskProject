import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { DividerModule } from 'primeng/divider';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { FormsModule } from '@angular/forms';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { LoginModel } from '../../models/login.model';
import { CheckboxModule } from 'primeng/checkbox';
import { GoogleSigninButtonModule, SocialAuthService } from '@abacritt/angularx-social-login';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    CardModule,
    ButtonModule,
    DividerModule,
    InputTextModule,
    PasswordModule,
    FormsModule,
    ToastModule,
    CheckboxModule,
    GoogleSigninButtonModule
  ],
  providers: [MessageService],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export default class LoginComponent implements OnInit {
  request: LoginModel = new LoginModel();

  constructor(
    private message: MessageService,
    private http: HttpClient,
    private router: Router,
    private auth: SocialAuthService) { }

  ngOnInit(): void {
    this.auth.authState.subscribe(res => {
      this.http.post("https://localhost:7159/api/Auth/GoogleLogin", res).subscribe({
        next: res => {
          localStorage.setItem("response", JSON.stringify(res));
          this.router.navigateByUrl("/");
        },
        error: (err: HttpErrorResponse) => {
          //*Server Validation Kontrol
          console.log(err);
          switch (err.status) {
            case 400:
              this.message.add({ severity: 'error', summary: 'Hata!', detail: err.error.message });
              break;
            case 0:
              this.message.add({ severity: 'error', summary: 'Hata!', detail: 'API Adresine ulaşılamıyor!' });
              break;
          }
        }
      })
      console.log(res);
    })
  }

  signIn() {

    //*Client Validation Kontrol

    if (this.request.userNameOrEmail.length < 3) {
      this.message.add({
        severity: 'warn',
        summary: 'Validasyon Hatası',
        detail: 'Geçerli bir kullanıcı adı ya da email adresi girin.'
      });
      return;
    }

    if (this.request.password.length < 6) {
      this.message.add({
        severity: 'warn',
        summary: 'Validasyon Hatası',
        detail: 'Şifreniz en az 6 karakter olmalıdır.'
      });
      return;
    }

    // Şifre büyük harf içermelidir
    // else if (!/[A-Z]/.test(this.request.password)) {
    //   this.message.add({
    //     severity: 'warn',
    //     summary: 'Validasyon Hatası',
    //     detail: 'Şifreniz en az bir büyük harf içermelidir.'
    //   });
    //   return;
    // }

    // Şifre küçük harf içermelidir
    // else if (!/[a-z]/.test(this.request.password)) {
    //   this.message.add({
    //     severity: 'warn',
    //     summary: 'Validasyon Hatası',
    //     detail: 'Şifreniz en az bir küçük harf içermelidir.'
    //   });
    //   return;
    // }

    // Şifre rakam içermelidir
    // else if (!/\d/.test(this.request.password)) {
    //   this.message.add({
    //     severity: 'warn',
    //     summary: 'Validasyon Hatası',
    //     detail: 'Şifreniz en az bir rakam içermelidir.'
    //   });
    //   return;
    // }

    // Şifre özel karakter içermelidir (örneğin: !@#$%^&*)
    // else if (!/[!@#$%^&*]/.test(this.request.password)) {
    //   this.message.add({
    //     severity: 'warn',
    //     summary: 'Validasyon Hatası',
    //     detail: 'Şifreniz en az bir özel karakter içermelidir.'
    //   });
    //   return;
    // }


    this.http.post("https://localhost:7159/api/Auth/Login", this.request)
      .subscribe({
        next: res => {
          localStorage.setItem("response", JSON.stringify(res));
          this.router.navigateByUrl("/");
        },
        error: (err: HttpErrorResponse) => {
          //*Server Validation Kontrol
          console.log(err);
          switch (err.status) {
            case 400:
              this.message.add({ severity: 'error', summary: 'Hata!', detail: err.error.message });
              break;
            case 422:
              for (let e of err.error)
                this.message.add({ severity: 'error', summary: "Validation Hatası!", detail: e });
              break;
            case 0:
              this.message.add({ severity: 'error', summary: 'Hata!', detail: 'API Adresine ulaşılamıyor!' });
              break;
          }
        }
      })
  }
}
