import { NgModule } from '@angular/core';
import { Http } from '@angular/http';
import { APP_BASE_HREF } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { AppComponent } from './app.component';
import { routing } from './app.routing';
import { HomeComponent } from './components/home.component';
import { RemoteApiClientService } from './infrastructure/remote/remoteapiclient.service';
import { WeatherService } from './infrastructure/services/weather.service';

@NgModule({
    imports: [BrowserModule, FormsModule, HttpModule, routing],
    declarations: [AppComponent, HomeComponent],
    providers: [
        { provide: APP_BASE_HREF, useValue: "/" },
        RemoteApiClientService,
        WeatherService
    ],
    bootstrap: [AppComponent]
})

export class AppModule { }