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
import { ErrorService } from '../../services/error.service';
import { HttpService } from '../../services/http.service';

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
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export default class LoginComponent implements OnInit {
  request: LoginModel = new LoginModel();

  constructor(
    private message: MessageService,
    private http: HttpService, //Service
    private router: Router,
    private auth: SocialAuthService,
    private error: ErrorService) { }

  ngOnInit(): void {
    this.auth.authState.subscribe(res => {
      this.http.post("Auth/GoogleLogin", res, (data) => {
        localStorage.setItem("response", JSON.stringify(data));
        this.router.navigateByUrl("/");
      })
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


    this.http.post("Auth/Login", this.request, res => {
      localStorage.setItem("response", JSON.stringify(res));
      this.router.navigateByUrl("/");
    });
  }
}
