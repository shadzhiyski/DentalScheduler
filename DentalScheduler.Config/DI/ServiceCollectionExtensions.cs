using DentalScheduler.DAL;
using DentalScheduler.DAL.Repositories;
using DentalScheduler.Interfaces.Gateways;
using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Interfaces.UseCases;
using DentalScheduler.Interfaces.UseCases.Validation;
using DentalScheduler.UseCases;
using DentalScheduler.UseCases.Validation;
using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            // Mappings
            var config = new TypeAdapterConfig();
            services.AddSingleton<TypeAdapterConfig>(config);
            
            // DAL
            services.AddTransient<DbContext, DentalSchedulerDbContext>();
            services.AddTransient<DentalSchedulerDbContext>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // UseCases
            services.AddTransient<AbstractValidator<ICreateRoleInput>, CreateRoleValidator>();
            services.AddTransient<AbstractValidator<IUserCredentialsInput>, UserCredentialsValidator>();
            services.AddTransient(typeof(IApplicationValidator<>), typeof(ApplicationValidator<>));
            services.AddTransient<ILoginCommand, LoginCommand>();
            services.AddTransient<IRegisterUserCommand, RegisterUserCommand>();

            return services;
        }
    }
}