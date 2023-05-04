﻿using Microsoft.Extensions.DependencyInjection;
using Name_Sorter.ConsolePrompt;
using Name_Sorter.FactoriesAndRegistrations;
using Name_Sorter.NameClasses;
using Name_Sorter.Sorting;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace NameSorter
{


    class Program
    {
    

        static void Main(string[] args)
        {

            bool shouldExit = false;

            

                var services = new ServiceCollection();
                MyServiceRegistration.RegisterServices(services);
                var serviceProvider = services.BuildServiceProvider();

               
                var log = serviceProvider.GetService<ILogger>();
              
               var readUserInput = serviceProvider.GetService<IUserInputReader>();
               

            var fileWriter = serviceProvider.GetService<INameListWriteFile>();
            var displayNames = serviceProvider.GetService<IDisplayNames>();

            log.Log(GreetUser());


            while (!shouldExit)
            {



                while (NS_ProgramHelper.displayTaskComplete == false && NS_ProgramHelper.writeTaskComplete == false )

                {
                    
                    readUserInput.ReadUserInput(Console.ReadLine());
                    if (NS_ProgramHelper.displayTaskComplete == true && NS_ProgramHelper.writeTaskComplete == true)
                    {
                        break;
                    }
                   
                }
                while (NS_ProgramHelper.displayTaskComplete == true && NS_ProgramHelper.writeTaskComplete == true)
                {
                    log.Log(ExitPrompt());
                    string answer = Console.ReadLine();
                    if (answer == "Y")
                    {
                        shouldExit = true;
                        log.Log("Exiting program...");
                        break;
                    }
                    else if (answer == "N")
                    {
                        log.Log("Resetting...");
                        NS_ProgramHelper.Reset();

                       //new  lines
                        log.Log("\n\n\n" + GreetUser());
                        break;
                        
                    }
                    else
                    {
                        log.Log("Invalid input. Please enter Y or N.");
                    }

                    Console.ReadKey();
                }


            }
          
           
          
            
            Console.ReadLine();
        }

      public static string GreetUser()
        {
            return "Welcome to the Name Sorter!\r\nTo sort a list of names, please enter the command:\r\nname-sorter <filename>\r\n\r\nYou can provide a relative or absolute path to the file, or use ./ to specify the current directory.\r\n";
        }
        
        public static string ExitPrompt()
        {
            return "Task completed! Do you want to exit? (Y/N)";
        }
        
    }
}