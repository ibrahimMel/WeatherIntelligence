import axios from 'axios';
import type { WeatherData } from '../types/weather';

const API_BASE_URL = 'http://localhost:5026/api';

export const weatherApi = {
  getAllWeather: async (city: string): Promise<WeatherData[]> => {
    const response = await axios.get(`${API_BASE_URL}/Weather/${city}`);
    return response.data;
  },

  getWeatherBySource: async (source: string, city: string): Promise<WeatherData> => {
    const response = await axios.get(`${API_BASE_URL}/Weather/source/${source}/${city}`);
    return response.data;
  },

  getAdapters: async (): Promise<{ name: string }[]> => {
    const response = await axios.get(`${API_BASE_URL}/Weather/adapters`);
    return response.data;
  }
};