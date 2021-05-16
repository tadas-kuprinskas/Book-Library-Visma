using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaBookLibrary.Application;
using VismaBookLibrary.Application.Writers;
using VismaBookLibrary.Domain.Factories;
using VismaBookLibrary.Domain.Interfaces;
using VismaBookLibrary.Domain.Services;

namespace VismaBookLibrary.ConsoleRunner
{
    public static class DepedencyInjection
    {
        public static ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<BookLibraryCLI>()
                .AddSingleton<CommandFactory>()               
                .AddSingleton<FilterCommandFactory>()               
                .AddSingleton<IWriter, ConsoleWriter>()
                .AddSingleton<IFileService, JsonFileService>()
                .AddSingleton<IPrintService, PrintingService>()
                .AddSingleton<IValidationService, ValidationService>()
                .BuildServiceProvider();
        }
    }
}
