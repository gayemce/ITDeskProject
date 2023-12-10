import { Routes } from '@angular/router';
import { LayoutsComponent } from './component/layouts/layouts.component';
import LoginComponent from './component/login/login.component';
import { AuthService } from './services/auth.service';
import { inject } from '@angular/core';

export const routes: Routes = [
    {
        path: "login",
        loadComponent: ()=> import("./component/login/login.component"),
    },
    {
        path: "",
        component: LayoutsComponent,
        canActivateChild: [() => inject(AuthService).checkAuthentication()],
        children: [
            {
                path: "",
                loadComponent: ()=> import("./component/home/home.component")
            },
            {
                path: "ticket-details/:value",
                loadComponent: ()=> import("./component/detail/detail.component")
            }
        ]
    }
];
