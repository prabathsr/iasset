import { Component, Input, OnInit } from '@angular/core';

@Component({
    selector: "weatherview-app",
    templateUrl: `./app/app.component.html`
})

export class AppComponent implements OnInit{
    @Input() title: string = "iasset Weather View";

    ngOnInit() {
    }
}