import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import type { PayloadAction } from '@reduxjs/toolkit';
import { weatherApi } from '../services/weatherApi';
import type { WeatherData } from '../types/weather';

interface WeatherState {
  data: WeatherData[];
  loading: boolean;
  error: string | null;
}

const initialState: WeatherState = {
  data: [],
  loading: false,
  error: null,
};

export const fetchWeather = createAsyncThunk(
  'weather/fetchWeather',
  async (city: string) => {
    const response = await weatherApi.getAllWeather(city);
    return response;
  }
);

const weatherSlice = createSlice({
  name: 'weather',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchWeather.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchWeather.fulfilled, (state, action: PayloadAction<WeatherData[]>) => {
        state.loading = false;
        state.data = action.payload;
      })
      .addCase(fetchWeather.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message || 'Une erreur est survenue';
      });
  },
});

export default weatherSlice.reducer;