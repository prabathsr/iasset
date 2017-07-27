import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { ICountry } from '../../domain/contracts/icountry';

@Injectable()
export class RemoteApiClientService {
    private remoteApiURl: string = "http://localhost:61488/api";

    constructor(private httpClient:Http) { }

    public async getCities(countryName: string): Promise<string[]> {
        var url = `${this.remoteApiURl}/${countryName}/cities`;
        try {
            return await this.httpClient
                .get(url)
                .toPromise()
                .then(response => (response.json() as string[]));
        } catch (e) {
            console.log(e.message);
            return null;
        }
    }

    public async getWeather(countryName: string, cityName: string): Promise<ICountry> {
        var url = `${this.remoteApiURl}/${countryName}/${cityName}/weather`;
        try {
            return await this.httpClient
                .get(url)
                .toPromise()
                .then(response => (response.json() as ICountry));           
        } catch (e) {
            console.log(e.message);
            return {
                name: countryName,
                city: null
            };
        } 
    }
}