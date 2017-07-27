import { Component, Output } from '@angular/core';
import { WeatherService } from '../infrastructure/services/weather.service';
import { ICountry } from '../domain/contracts/icountry';
import { ICity } from '../domain/contracts/icity';
import { DateTime } from '../infrastructure/common/datetime';

@Component({
    templateUrl: `./app/components/home.component.html`
})
export class HomeComponent {
    public countryName: string;
    public cityName: string;
    public cities: string [];
    public country: ICountry;
    public isLoadingCities: boolean = false;
    public isLoadingWeatherInfo: boolean = false;

    constructor(private weatherService: WeatherService) {}

    public async loadCities(countryName: string) {
        this.isLoadingCities = true;
        this.clearCities();

        if (countryName.length > 1) {
            this.cities = await this.weatherService.getCities(countryName);
        } 

        this.countryName = countryName;
        this.isLoadingCities = false;
    }

    public clearCities(): void {
        this.cityName = null;
        this.cities = null;
    }

    public async showWeatherInfo(cityName: string) {
        this.isLoadingWeatherInfo = true;
        this.country = null;
        this.cityName = cityName;
        this.country = await this.weatherService.getWeather(this.countryName, cityName);

        this.isLoadingWeatherInfo = false;
    }

    public getTimeStamp(stamp: number): number {
        return DateTime.getTimeStamp(stamp);
    }
}
