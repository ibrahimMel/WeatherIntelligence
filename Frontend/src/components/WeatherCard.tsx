import type { WeatherData } from '../types/weather';

interface WeatherCardProps {
  weather: WeatherData;
}

export const WeatherCard = ({ weather }: WeatherCardProps) => {
  const sourceDot: Record<string, string> = {
    OpenWeather: 'bg-orange-500',
    WeatherAPI: 'bg-sky-500',
    Weatherstack: 'bg-emerald-500',
  };

  const getWeatherEmoji = (description: string) => {
    const desc = description.toLowerCase();
    if (desc.includes('clear') || desc.includes('sunny')) return '☀️';
    if (desc.includes('cloud')) return '☁️';
    if (desc.includes('rain')) return '🌧️';
    if (desc.includes('storm') || desc.includes('thunder')) return '⛈️';
    if (desc.includes('snow')) return '❄️';
    if (desc.includes('fog') || desc.includes('mist')) return '🌫️';
    return '🌤️';
  };

  const dotClass = sourceDot[weather.source] ?? 'bg-slate-400';

  return (
    <div className="rounded-xl border border-slate-200 bg-white p-5 shadow-sm transition hover:shadow-md">
      <div className="flex items-start justify-between gap-4">
        <div>
          <div className="flex items-center gap-2 text-xs font-semibold text-slate-600">
            <span className={`h-2 w-2 rounded-full ${dotClass}`} />
            <span className="uppercase tracking-wide">{weather.source}</span>
          </div>

          <h2 className="mt-2 text-xl font-semibold text-slate-900">{weather.city}</h2>
          <p className="text-sm text-slate-500">{weather.country}</p>
        </div>

        <div className="text-4xl">{getWeatherEmoji(weather.description)}</div>
      </div>

      <div className="mt-5 flex items-end justify-between gap-4">
        <div>
          <div className="text-4xl font-bold text-slate-900">
            {Math.round(weather.temperature)}°
          </div>
          <div className="mt-1 text-sm font-medium text-slate-600 capitalize">
            {weather.description}
          </div>
        </div>
      </div>

      <div className="mt-5 grid grid-cols-2 gap-3">
        <div className="rounded-lg bg-slate-50 p-3">
          <div className="text-xs text-slate-500">Humidite</div>
          <div className="mt-1 text-lg font-semibold text-slate-900">{weather.humidity}%</div>
        </div>

        <div className="rounded-lg bg-slate-50 p-3">
          <div className="text-xs text-slate-500">Vent</div>
          <div className="mt-1 text-lg font-semibold text-slate-900">
            {Math.round(weather.windSpeed)}
            <span className="ml-1 text-xs font-medium text-slate-500">km/h</span>
          </div>
        </div>
      </div>
    </div>
  );
};
