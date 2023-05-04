using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Name_Sorter.NameClasses;
using Name_Sorter.ConsolePrompt;
using Microsoft.Extensions.DependencyInjection;
using Name_Sorter.FactoriesAndRegistrations;
using System.Text.RegularExpressions;

namespace Name_Sorter.Sorting
{
  

   

    public class ExtractNamesOnString : INameExtractor
    {
        public List<IPerson> ExtractNames(string fileText)
        {

            var services = new ServiceCollection();
            MyServiceRegistration.RegisterServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var fullNameService = serviceProvider.GetService<IPersonFactory>();


            //split \n or \r\n 
            string[] lines = Regex.Split(fileText, @"\r\n|\n");


            if (fullNameService != null)
                {
                    foreach (string line in lines)
                    {
                        string[] nameParts = line.Split(' ');

                        fullNameService.PersonList.Add(fullNameService.CreatePerson(nameParts));
                    }
                }
                else
                {
                    throw new Exception("Services returned null, please ensure it's cretated");
                }

                return fullNameService.PersonList;
           
        }
    }
       
    public class LastNameOnListSorter :INameSorter
    {
        public List<IPerson> SortNameList(List<IPerson> sortedList )
        {
            return sortedList.OrderBy(IPerson => IPerson.lastName).ToList();

         
        }
    }

    public class DisplayNamesOnList : IDisplayNames
    {
     
        public void DisplayNameList(List<IPerson> displayList)
        {
            var services = new ServiceCollection();
            MyServiceRegistration.RegisterServices(services);
            var serviceProvider = services.BuildServiceProvider();

       
            var logger = serviceProvider.GetService<ILogger>();

            foreach(IPerson person in displayList)
            {
                logger.Log(person.MyName());
            }

          if(NS_ProgramHelper.displayTaskComplete == false)
            {
                NS_ProgramHelper.displayTaskComplete = true;
            }

        }
    }
}
