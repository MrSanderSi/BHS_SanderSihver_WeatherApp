import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { forecasts: [], loading: true };
  }

  componentDidMount() {
    this.populateWeatherData();
  }

    static renderForecastsTable(forecasts) {
        var weekDays = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
    return (
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
    );
    }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderForecastsTable(this.state.forecasts);

    return (
      <div>
        <h1 id="tabelLabel" >Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }
  

    async populateWeatherData() {
        var city = "Miami";
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
            body: JSON.stringify(city),
    });
    const data = await response.json();
    this.setState({ forecasts: data, loading: false });
  }

}
