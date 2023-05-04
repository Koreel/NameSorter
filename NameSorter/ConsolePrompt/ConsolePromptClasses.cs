﻿using Microsoft.Extensions.DependencyInjection;
using Name_Sorter.FactoriesAndRegistrations;
using Name_Sorter.NameClasses;
using Name_Sorter.Sorting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Name_Sorter.ConsolePrompt
{
    public class FileLocator : IFileLocator
    {

        public bool fileExists { get; set; }= false;
        public string GetFileName(string fileName)
        {
            
            string filePath = "";

            var services = new ServiceCollection();
            MyServiceRegistration.RegisterServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var log = serviceProvider.GetService<ILogger>();

            if (fileName.StartsWith("./") || fileName.StartsWith("../"))
            {
                // Get the absolute path of the parent directory
                string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;
                filePath = Path.Combine(executableDirectory, fileName.TrimStart('.', '/', '\\'));

                
            }
            else if (fileName.StartsWith("/") || fileName.Contains(":\\"))
            {
                // Check if the file path is outside the project directory
                string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName;
                if (!filePath.StartsWith(projectDirectory))
                {
                    log.Log("Grabbing file outside of current directory");
                }
                // Use the absolute path specified in the command-line argument
                filePath = fileName;
            }
            else
            {
                string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;
                filePath = Path.Combine(executableDirectory, fileName.TrimStart('.', '/', '\\'));
            }
            if(File.Exists(filePath))
            {
                fileExists = true;
                return filePath;
               
            }
            else
            {
                return "File Does not exist, pleast try inputing the correct name";
            }

          


          

            //return filePath;

        }
    }

    public class UserInputReader : IUserInputReader
    {



        public void ReadUserInput(string readLine)
        {
            var services = new ServiceCollection();
            MyServiceRegistration.RegisterServices(services);
            var serviceProvider = services.BuildServiceProvider();
            var log = serviceProvider.GetService<ILogger>();

           

            var consoleFunctionsFactory = serviceProvider.GetService<IConsoleFunctionsFactory>();

            string[] parts = readLine.Split(' ');

            if (parts.Length < 2)
            {
               log.Log("Invalid input format, please try again.");

            }
            else
            {
                string command = parts[0];
                string fileName = parts[1];

                // Get the appropriate console function based on the command
                var consoleFunction = consoleFunctionsFactory.GetConsoleFunctionClass(command, fileName);

                consoleFunction.InputFunction();
            }

         

        }


    }

    public class Console_Logger : ILogger
    {
        public string Log(string message)
        {
            Console.WriteLine(message);
            return message;
        }
    }

    public class GetFileContents : IFileReader
    {

        public string FileContentsString(string fileName)
        {
           

            var services = new ServiceCollection();
            MyServiceRegistration.RegisterServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var log = serviceProvider.GetService<ILogger>();

            string contents = "";

            if (File.Exists(fileName))
            {
                using StreamReader reader = new StreamReader(fileName);
                contents = reader.ReadToEnd();
            }
            else
            {
                log.Log($"File '{fileName}' does not exist.");
            }

            return contents;
        }


    }


    public class WriteNameOnFile : INameListWriteFile
    {
      
        public void GenerateFile(List<IPerson> nameList, string _directory)
        {
            string parentDirectory = Directory.GetParent(_directory).FullName;


            var services = new ServiceCollection();
            MyServiceRegistration.RegisterServices(services);
            var serviceProvider = services.BuildServiceProvider();
            var log = serviceProvider.GetService<ILogger>();

            string _text = string.Empty;
            for (int i = 0; i < nameList.Count; i++)
            {
                _text += nameList[i].MyName() + " " + "\n";
            }

            try
            {
                using (StreamWriter writer = new StreamWriter(parentDirectory + "./sorted-names-list-.txt"))
                {
                    writer.Write(_text);
                    
                    log.Log( $"Successfully written script @ {parentDirectory}");
                    if(NS_ProgramHelper.writeTaskComplete == false )
                    {
                        NS_ProgramHelper.writeTaskComplete = true;
                    }
                  
                    
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here
            }

        }


  
    }

    //static class to oversee program
    public static class NS_ProgramHelper
    {
       
        public static bool displayTaskComplete { get; set; } = false;
        public static bool writeTaskComplete { get; set; } = false;

        public static void Reset()
        {
            displayTaskComplete = false;
            writeTaskComplete = false;
        }


    }




}
