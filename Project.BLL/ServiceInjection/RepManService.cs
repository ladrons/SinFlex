using Microsoft.Extensions.DependencyInjection;
using Project.BLL.ManagerServices.Abstracts;
using Project.BLL.ManagerServices.Concretes;
using Project.DAL.Repositories.Abstracts;
using Project.DAL.Repositories.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ServiceInjection
{
    public static class RepManService
    {
        public static IServiceCollection AddRepManServices(this IServiceCollection services)
        {
            //Bu method Repository ve Manager işlemlerini servis eder.

            //Repository
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<ISaloonRepository, SaloonRepository>();
            services.AddScoped<ISeanceRepository, SeanceRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ISoldTicketRepository, SoldTicketRepository>();
            services.AddScoped<IBoxOfficeRepository, BoxOfficeRepository>();
            services.AddScoped<IFormatRepository, FormatRepository>();
            services.AddScoped<IMovieFormatRepository, MovieFormatRepository>();
            //Manager
            services.AddScoped(typeof(IManager<>), typeof(BaseManager<>));
            services.AddScoped<IAppUserManager, AppUserManager>();
            services.AddScoped<IMovieManager, MovieManager>();
            services.AddScoped<IGenreManager, GenreManager>();
            services.AddScoped<ISaloonManager, SaloonManager>();
            services.AddScoped<ISeanceManager, SeanceManager>();
            services.AddScoped<ITicketManager, TicketManager>();
            services.AddScoped<ISoldTicketManager, SoldTicketManager>();
            services.AddScoped<IBoxOfficeManager, BoxOfficeManager>();
            services.AddScoped<IFormatManager, FormatManager>();
            services.AddScoped<IMovieFormatManager, MovieFormatManager>();

            return services;
        }
    }
}
