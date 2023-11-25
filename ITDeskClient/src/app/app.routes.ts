import { Routes } from '@angular/router';
import { LayoutsComponent } from './component/layouts/layouts.component';
import HomeComponent from './component/home/home.component';

export const routes: Routes = [
    {
        path: "",
        component: LayoutsComponent,
        children: [
            {
                path: "",
                component: HomeComponent
                //loadComponent: ()=> import("./components/home/home.component")
            }
        ]
    }
];
