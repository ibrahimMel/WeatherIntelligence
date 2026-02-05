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
    if (city.trim()) {
      dispatch(fetchWeather(city));
    }
  };

  return (
    <div className="min-h-screen relative overflow-hidden">
      <div className="absolute inset-0 app-bg"></div>
      <div className="absolute inset-0 grid-overlay opacity-50"></div>

      <div
        className="absolute -top-24 -left-24 h-80 w-80 rounded-full blur-3xl opacity-70 animate-float"
        style={{
          background: 'radial-gradient(circle, rgba(14,165,165,0.55) 0%, transparent 65%)',
        }}
      />
      <div
        className="absolute top-20 right-[-120px] h-[420px] w-[420px] rounded-full blur-3xl opacity-60 animate-float animation-delay-2000"
        style={{
          background: 'radial-gradient(circle, rgba(248,113,113,0.35) 0%, transparent 65%)',
        }}
      />
      <div
        className="absolute bottom-[-200px] left-1/3 h-[520px] w-[520px] -translate-x-1/3 rounded-full blur-3xl opacity-60 animate-float animation-delay-4000"
        style={{
          background: 'radial-gradient(circle, rgba(253,186,116,0.5) 0%, transparent 70%)',
        }}
      />

      <div className="relative z-10">
        <div className="max-w-6xl mx-auto px-6 py-12">
          <header className="text-center animate-fade-in">
            <div className="mx-auto mb-6 inline-flex items-center gap-2 rounded-full px-4 py-2 text-xs font-semibold uppercase tracking-[0.2em] chip text-slate-600">
              <span className="h-2 w-2 rounded-full bg-emerald-500"></span>
              Comparateur météo
            </div>
            <h1 className="font-display text-4xl sm:text-5xl lg:text-6xl font-semibold text-slate-900">
              Weather <span className="text-gradient">Intelligence</span>
            </h1>
            <p className="mt-4 text-lg sm:text-xl text-slate-600 max-w-2xl mx-auto">
              Trois sources, un seul regard. Trouvez la météo la plus fiable pour votre ville.
            </p>
          </header>

          <form onSubmit={handleSearch} className="mt-10 max-w-3xl mx-auto">
            <div className="glass-panel rounded-[28px] p-2">
              <div className="flex flex-col md:flex-row gap-3">
                <div className="flex-1 relative">
                  <span className="absolute left-6 top-1/2 -translate-y-1/2 text-xl">📍</span>
                  <input
                    type="text"
                    value={city}
                    onChange={(e) => setCity(e.target.value)}
                    placeholder="Rabat, Paris, Tokyo, New York..."
                    className="w-full pl-14 pr-6 py-4 rounded-2xl bg-white/90 text-lg text-slate-900 placeholder-slate-400 shadow-sm ring-1 ring-slate-200 focus:outline-none focus:ring-2 focus:ring-teal-400 transition"
                  />
                </div>
                <button
                  type="submit"
                  disabled={loading}
                  className="group flex items-center justify-center gap-3 rounded-2xl bg-slate-900 px-8 py-4 text-white text-lg font-semibold shadow-lg transition hover:-translate-y-0.5 hover:shadow-xl disabled:opacity-50 disabled:cursor-not-allowed disabled:transform-none"
                >
                  {loading ? (
                    <>
                      <div className="animate-spin text-xl">⏳</div>
                      <span>Recherche...</span>
                    </>
                  ) : (
                    <>
                      <span className="text-xl">🔍</span>
                      <span>Rechercher</span>
                    </>
                  )}
                </button>
              </div>
            </div>
          </form>

          {error && (
            <div className="max-w-3xl mx-auto mt-6 animate-shake">
              <div className="rounded-3xl border border-rose-200/60 bg-rose-50/80 px-6 py-5 text-rose-900 shadow-sm backdrop-blur">
                <div className="flex items-center gap-3">
                  <span className="text-2xl">⚠️</span>
                  <div>
                    <p className="font-semibold">Erreur de recherche</p>
                    <p className="text-sm opacity-80">{error}</p>
                  </div>
                </div>
              </div>
            </div>
          )}

          {loading && (
            <div className="text-center py-16">
              <div className="mx-auto inline-flex flex-col items-center gap-4 rounded-3xl border border-white/70 bg-white/70 px-10 py-8 shadow-lg backdrop-blur">
                <div className="text-5xl animate-spin-slow">🌀</div>
                <p className="text-slate-700 text-lg font-medium">Chargement des données météo...</p>
              </div>
            </div>
          )}

          {!loading && data.length > 0 && (
            <div className="mt-12 grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 animate-fade-in-up">
              {data.map((weather: WeatherData, index: number) => (
                <WeatherCard key={`${weather.source}-${index}`} weather={weather} index={index} />
              ))}
            </div>
          )}

          {!loading && data.length === 0 && !error && (
            <div className="text-center py-16">
              <div className="mx-auto inline-flex flex-col items-center gap-4 rounded-3xl border border-white/70 bg-white/70 px-10 py-8 shadow-lg backdrop-blur">
                <div className="text-5xl">🌤️</div>
                <p className="text-slate-700 text-lg font-medium">
                  Entrez une ville pour découvrir la météo.
                </p>
              </div>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}

export default App;
