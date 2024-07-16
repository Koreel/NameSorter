using Microsoft.Extensions.DependencyInjection;
using Name_Sorter.ConsolePrompt;
using Name_Sorter.Sorting;
using Name_Sorter.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Name_Sorter.Registrations
{
    public class MyServiceRegistration
    {
        public static void RegisterServices(IServiceCollection services)
        {

            //Console Classes Factory
            services.AddSingleton<IFileReader, GetFileContents>();
            services.AddSingleton<ILogger, Console_Logger>();
            services.AddSingleton<IFileLocator, FileLocator>();

            services.AddSingleton<IConsoleFunctionsFactory, ConsoleFactory>();
            services.AddScoped<IUserInputReader, UserInputReader>();


            //Name Class Factory
            services.AddScoped<IPersonFactory, FullNameFactory>();

            //Sorting Classes
            services.AddScoped<INameSorter, LastNameOnListSorter>();
            services.AddSingleton<INameListWriteFile>(new WriteNameOnFile());
            services.AddScoped<INameExtractor, ExtractNamesOnString>();

            services.AddSingleton<IDisplayNames>(new DisplayNamesOnList());

        }
    }
}
