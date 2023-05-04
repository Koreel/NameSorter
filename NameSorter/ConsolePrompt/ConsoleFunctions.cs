using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Name_Sorter.FactoriesAndRegistrations;
using Name_Sorter.NameClasses;
using Name_Sorter.Sorting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Name_Sorter.ConsolePrompt
{
    public class ConsoleFactory : IConsoleFunctionsFactory
    {
        private readonly IFileReader fileReader;

        public bool usedCorrectFunctionName { get; set; } = false;



        public ConsoleFactory(IFileReader fileReader)
        {
            this.fileReader = fileReader;
        }



        public IConsoleFunctions GetConsoleFunctionClass(string functionName, string directory)
        {
            var services = new ServiceCollection();
            MyServiceRegistration.RegisterServices(services);
            var serviceProvider = services.BuildServiceProvider();
           
            switch (functionName)
            {
                case "name-sorter":
                    usedCorrectFunctionName = true;
                    return new ConsoleCommand_NameSorter(fileReader, directory);

                default:
                    
                    return new  ConsoleCommand_Error("Wrong input, try again");

                    break;
            }
        }
    }


    public abstract class ConsoleCommand : IConsoleFunctions
    {


        public string directory { get; set; }

        public abstract void InputFunction();

        public ConsoleCommand(string _directory)
        {
            directory = _directory;
          
        }
    }


    public class ConsoleCommand_NameSorter : ConsoleCommand
    {
        private readonly IFileReader fileReader;
        private readonly string directory;

        public ConsoleCommand_NameSorter(IFileReader fileReader, string _directory) : base(_directory)
        {
            this.fileReader = fileReader;
            this.directory = _directory;
        }


        public override void InputFunction()
        {
            var services = new ServiceCollection();
            MyServiceRegistration.RegisterServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var fullNameService = serviceProvider.GetService<IPersonFactory>();
            var displayList = serviceProvider.GetService<IDisplayNames>();
            var nameListSorter = serviceProvider.GetService<INameSorter>();
            var nameExtractor = serviceProvider.GetService<INameExtractor>();
            var writeNameListToFile = serviceProvider.GetService<INameListWriteFile>();
            var fileLocator = serviceProvider.GetRequiredService<IFileLocator>();

            List<IPerson> extractedNames = fullNameService.PersonList;  
            List<IPerson> sortedNames = fullNameService.PersonList;

            // Read file text from directory

            string filePath = fileLocator.GetFileName(directory);
            string fileContents = fileReader.FileContentsString(filePath);
            extractedNames = nameExtractor.ExtractNames(fileContents);
            sortedNames = nameListSorter.SortNameList(extractedNames);

            displayList.DisplayNameList(sortedNames);
            writeNameListToFile.GenerateFile(sortedNames, filePath);
        }
    }


    public class ConsoleCommand_Error : IConsoleFunctions
    {
        public string directory { get; set; }
        private readonly string errorMessage;

        public ConsoleCommand_Error(string errorMessage)
        {
            this.errorMessage = errorMessage;
            InputFunction();
        }

        public void InputFunction()
        {
            var services = new ServiceCollection();
            MyServiceRegistration.RegisterServices(services);
            var serviceProvider = services.BuildServiceProvider();
            var log = serviceProvider.GetService<ILogger>();
            log.Log(errorMessage);
        }
    }


}
