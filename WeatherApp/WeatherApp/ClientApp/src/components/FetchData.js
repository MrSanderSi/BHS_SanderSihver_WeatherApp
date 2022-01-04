import React, { Component, useState } from 'react';
import authService from './api-authorization/AuthorizeService'

export class FetchData extends Component {


    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = { forecasts: [], loading: true, locations: [], chosenCity: 'Miami' };
        this.city = "";
    }

    updateCity = () => {
        var cityN = document.getElementById('cities')
        this.city = cityN.value
        console.log(this.city)
        this.setState({ chosenCity: this.city });
        this.populateWeatherData()
    };

    componentDidMount() {
        this.getAvailableCities();
        this.populateWeatherData();
    }

    static renderForecastsTable(forecasts) {
        var weekDays = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
        return (
            <div>
                <h3>Current weather: {forecasts.location}</h3>
                <p>temperature: {forecasts.temperature},
                    wind: {forecasts.wind},
                    description: {forecasts.description}</p>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Day</th>
                            <th>Temp. (C)</th>
                            <th>Wind</th>
                        </tr>
                    </thead>
                    <tbody>
                        {forecasts.forecast.map(forecast =>
                            <tr key={forecast.day}>
                                <td>{weekDays[forecast.dayOfWeek]}</td>
                                <td>{forecast.temperature}</td>
                                <td>{forecast.wind}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>

        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FetchData.renderForecastsTable(this.state.forecasts);

        return (
            <div>
                <p>Available cities:</p>
                <div style={{ display: 'flex' }}>
                    <form style={{ alignItems: 'bottom', justifyContent: 'left' }}>
                        {FetchData.renderAvailableCities(this.state.locations)}
                    </form>
                    <button className="btn btn-primary" style={{ margin: '10px' }} onClick={this.updateCity}>
                        Choose
                    </button>
                </div>
                <h1 id="tabelLabel" >Weather forecast</h1>
                {contents}
            </div>
        );
    }

    static renderAvailableCities(locations) {
        return (
            <div style={{ padding: '10px' }}>
                <lable for="cities" style={{ margin: '5px' }}>Choose a city</lable>
                <select name="cities" id="cities" onChange={this.updateCity}>
                    {
                        locations.map(location =>
                            <option key={location.cityName} value={location.cityName}>{location.cityName}</option>)
                    }
                </select>
            </div>
        );
    }
    async populateWeatherData() {
        const token = await authService.getAccessToken();
        const response = await fetch('weatherforecast', {
            method: 'post',
            headers: new Headers({
                'Authorization': `Bearer ${token}`,
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }),
            mode: 'cors',
            cache: 'default',
            body: JSON.stringify(this.state.chosenCity)
        });
        const data = await response.json();
        this.setState({ forecasts: data, loading: false });
    }

    async getAvailableCities() {
        const response = await fetch('api/city')
        const data = await response.json();
        this.setState({ locations: data })

    }
}
