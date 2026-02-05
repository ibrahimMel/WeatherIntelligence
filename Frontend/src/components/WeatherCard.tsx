import type { WeatherData } from '../types/weather';

interface WeatherCardProps {
  weather: WeatherData;
  index: number;
}

export const WeatherCard = ({ weather, index }: WeatherCardProps) => {
  const sourceStyles: Record<string, { bar: string; badge: string }> = {
    OpenWeather: {
      bar: 'from-amber-400 to-orange-500',
      badge: 'border-amber-200 bg-amber-50 text-amber-700',
    },
    WeatherAPI: {
      bar: 'from-sky-400 to-cyan-500',
      badge: 'border-sky-200 bg-sky-50 text-sky-700',
    },
    Weatherstack: {
      bar: 'from-emerald-400 to-teal-500',
      badge: 'border-emerald-200 bg-emerald-50 text-emerald-700',
    },
    default: {
      bar: 'from-slate-400 to-slate-600',
      badge: 'border-slate-200 bg-slate-50 text-slate-700',
    },
  };

  const style = sourceStyles[weather.source] ?? sourceStyles.default;

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

  const animationDelay = `${index * 150}ms`;

  return (
    <div className="group animate-fade-in-up" style={{ animationDelay }}>
      <div className="relative overflow-hidden rounded-3xl border border-white/70 bg-white/70 p-6 shadow-[0_20px_60px_-40px_rgba(15,23,42,0.35)] backdrop-blur-xl transition duration-500 hover:-translate-y-1 hover:shadow-[0_26px_70px_-40px_rgba(15,23,42,0.45)]">
        <div className={`absolute inset-x-0 top-0 h-1 bg-gradient-to-r ${style.bar}`} />

        <div className="flex items-start justify-between gap-4">
          <div>
            <div
              className={`inline-flex items-center gap-2 rounded-full border px-3 py-1 text-xs font-semibold uppercase tracking-[0.16em] ${style.badge}`}
            >
              <span className="h-1.5 w-1.5 rounded-full bg-current opacity-60"></span>
              {weather.source}
            </div>
            <h2 className="mt-4 font-display text-3xl text-slate-900">{weather.city}</h2>
            <p className="mt-1 text-sm text-slate-500">{weather.country}</p>
          </div>
          <div className="text-6xl drop-shadow-sm transition-transform duration-500 group-hover:scale-110">
            {getWeatherEmoji(weather.description)}
          </div>
        </div>

        <div className="mt-6 flex items-end justify-between">
          <div>
            <div className="text-6xl font-semibold text-slate-900">
              {Math.round(weather.temperature)}°
            </div>
            <div className="text-sm font-medium text-slate-500 capitalize">
              {weather.description}
            </div>
          </div>
        </div>

        <div className="mt-6 grid grid-cols-2 gap-3">
          <div className="stat-card rounded-2xl p-4">
            <div className="flex items-center gap-2 text-sm text-slate-500">
              <span className="text-lg">💧</span>
              Humidité
            </div>
            <div className="mt-2 text-2xl font-semibold text-slate-900">{weather.humidity}%</div>
          </div>

          <div className="stat-card rounded-2xl p-4">
            <div className="flex items-center gap-2 text-sm text-slate-500">
              <span className="text-lg">💨</span>
              Vent
            </div>
            <div className="mt-2 text-2xl font-semibold text-slate-900">
              {Math.round(weather.windSpeed)}
              <span className="ml-1 text-sm font-medium text-slate-500">km/h</span>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};
