import { Injectable } from '@angular/core';
import { RemoteApiClientService } from '../remote/remoteapiclient.service';
import { ICountry } from '../../domain/contracts/icountry';

@Injectable()
export class WeatherService {
    constructor(private remoteApiClient: RemoteApiClientService) { }

    public async getCities(countryName: string):Promise<string[]> {
        return await this.remoteApiClient.getCities(countryName);
    }

    public async getWeather(countryName: string, cityName:string): Promise<ICountry> {
        return await this.remoteApiClient.getWeather(countryName, cityName);
    }

}