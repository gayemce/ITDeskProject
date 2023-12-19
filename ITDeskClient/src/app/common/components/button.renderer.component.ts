import { Component } from "@angular/core";
import { ButtonModule } from "primeng/button";

@Component({
    selector: "app-button-renderer",
    standalone: true,
    imports: [ButtonModule],
    template: `
        <span style="cursor: pointer;" class="p-badge p-component p-badge-lg p-badge-secondary" (click)="onClick($event)">{{label}}</span>
    `
})
export class buttonRendererComponent {
    params: any;
    label: string = "";

    // agInit - OnInit yaşam döngüsünün agDataGrid de kullanımı. Değişkenleri set eder
    agInit(params: any): void {
        this.params = params;
        this.label = this.params.label || null;
    }

    // click - onClick olarak çalışacak
    onClick(event: any) {
        if (this.params.onClick instanceof Function) {
            const params = {
                event: event,
                rowData: this.params.node.data
            }
            this.params.onClick(params);
        }
    }
}