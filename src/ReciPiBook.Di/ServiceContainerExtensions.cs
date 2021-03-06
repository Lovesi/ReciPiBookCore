﻿using System;
using LightInject;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using ReciPiBook.Entities;
using ReciPiBook.Repository;
using ReciPiBook.Services.Authorization;
using ReciPiBook.Services.UnitOfMeasure;
using ReciPiBook.Services.User;

namespace ReciPiBook.Di
{
    public static class ServiceContainerExtensions
    {
        public static IServiceContainer RegisterRepositories(this IServiceContainer container)
        {
            container.Register<IInfrastructure<IServiceProvider>, RecipesDb>("recipesdb");
            container.Register<IRepository<UnitOfMeasure>>((factory) => 
                new Repository<UnitOfMeasure>(factory.GetInstance<IInfrastructure<IServiceProvider>>("recipesdb")));
            return container;
        }

        public static IServiceContainer RegisterConfiguration(this IServiceContainer container, IConfiguration configuration)
        {
            container.RegisterInstance(configuration);
            return container;
        }

        public static IServiceContainer RegisterServices(this IServiceContainer container)
        {
            container.Register<IUnitOfMeasureService, UnitOfMeasureService>(new PerRequestLifeTime());
            container.Register<IAuthorizationService, AuthorizationService>(new PerRequestLifeTime());
            container.Register<ITicketService, TicketService>(new PerRequestLifeTime());
            container.Register<IUserRegistrationService, UserService>(new PerRequestLifeTime());
            return container;
        }
    }
}
