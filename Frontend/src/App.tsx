import { useState } from 'react';
import { fetchWeather } from './redux/weatherSlice';
import { useAppDispatch, useAppSelector } from './redux/hooks';
import { WeatherCard } from './components/WeatherCard';
import type { WeatherData } from './types/weather';

function App() {
  const [city, setCity] = useState('');
  const dispatch = useAppDispatch();
  const { data, loading, error } = useAppSelector((state) => state.weather);

  const handleSearch = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (city.trim()) dispatch(fetchWeather(city));
  };

  return (
    <div className="min-h-screen bg-slate-50">
      <div className="max-w-5xl mx-auto px-4 py-10">
        <header className="text-center">
          <h1 className="text-3xl sm:text-4xl font-bold text-slate-900">
            Weather Intelligence
          </h1>
          <p className="mt-2 text-slate-600">
            Comparez 3 sources météo en temps réel.
          </p>
        </header>

        <form onSubmit={handleSearch} className="mt-8">
          <div className="mx-auto max-w-2xl flex flex-col sm:flex-row gap-3">
            <label className="sr-only" htmlFor="city">
              Ville
            </label>
            <input
              id="city"
              type="text"
              value={city}
              onChange={(e) => setCity(e.target.value)}
              placeholder="Rabat, Paris, Tokyo, New York..."
              className="flex-1 rounded-lg border border-slate-300 bg-white px-4 py-3 text-slate-900 shadow-sm outline-none focus:ring-2 focus:ring-sky-500"
            />
            <button
              type="submit"
              disabled={loading}
              className="rounded-lg bg-sky-600 px-6 py-3 font-semibold text-white shadow-sm hover:bg-sky-700 disabled:opacity-50 disabled:cursor-not-allowed"
            >
              {loading ? 'Recherche...' : 'Rechercher'}
            </button>
          </div>
        </form>

        {error && (
          <div className="mx-auto mt-6 max-w-2xl rounded-lg border border-red-200 bg-red-50 px-4 py-3 text-red-800">
            {error}
          </div>
        )}

        {loading && (
          <div className="mt-10 text-center text-slate-600">
            <div className="mx-auto h-8 w-8 animate-spin rounded-full border-4 border-slate-200 border-t-sky-600"></div>
            <p className="mt-3">Chargement...</p>
          </div>
        )}

        {!loading && data.length > 0 && (
          <div className="mt-10 grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
            {data.map((weather: WeatherData, index: number) => (
              <WeatherCard key={`${weather.source}-${index}`} weather={weather} />
            ))}
          </div>
        )}

        {!loading && data.length === 0 && !error && (
          <p className="mt-10 text-center text-slate-600">
            Entrez une ville pour voir la météo.
          </p>
        )}
      </div>
    </div>
  );
}

export default App;
