using Microsoft.Extensions.DependencyInjection;
using Name_Sorter.ConsolePrompt;
using Name_Sorter.FactoriesAndRegistrations;
using Name_Sorter.NameClasses;
using Name_Sorter.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter_NUnitTest
{
    public class NameSorterTests_SortingClasses
    {
        [Test]
        public void ExtractInvalidNameExtractor()
        {
            // Arrange
            var fileText = "John Doe\nJane Jimmyson\nInvalid Name\n";

            var services = new ServiceCollection();
            MyServiceRegistration.RegisterServices(services);
            var serviceProvider = services.BuildServiceProvider();
            

            var nameExtractor = serviceProvider.GetService<INameExtractor>();

            nameExtractor.ExtractNames(fileText);

            // Act and Assert
            Assert.Throws<ArgumentException>(() => nameExtractor.ExtractNames(fileText));
        }


        [Test]
        public void ExtractValidNameExtractor()
        {
            // Arrange
            var fileText = "John Smith\nJane Doe\nBob Johnson\nSome Cool Name";

            var services = new ServiceCollection();
            MyServiceRegistration.RegisterServices(services);
            var serviceProvider = services.BuildServiceProvider();
            var familyFactory = serviceProvider.GetService<IPersonFactory>();

            var myPersonList = familyFactory.PersonList;

            var nameExtractor = serviceProvider.GetService<INameExtractor>();

            myPersonList = nameExtractor.ExtractNames(fileText);

            // Assert
            Assert.NotNull(myPersonList);
            Assert.AreEqual(4, myPersonList.Count);


        }
        [Test]
        public void ValidNameSorter()
        {
            var services = new ServiceCollection();
            MyServiceRegistration.RegisterServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var personFactory = serviceProvider.GetService<IPersonFactory>();
            var nameSorterClass = serviceProvider.GetService<INameSorter>();

            var names = personFactory.PersonList;

            names.Add(personFactory.CreatePerson("John", "Doe"));
            names.Add(personFactory.CreatePerson("Jane", "Mary", "Doe"));
            names.Add(personFactory.CreatePerson("Bob", "Smith"));
            names.Add(personFactory.CreatePerson("Alice", "Johnson"));
           var sortedList = nameSorterClass.SortNameList(names);
            // Assert
            Assert.AreEqual("Doe", sortedList[0].lastName);
            Assert.AreEqual("Doe", sortedList[1].lastName);
            Assert.AreEqual("Johnson", sortedList[2].lastName);
            Assert.AreEqual("Smith", sortedList[3].lastName);


        }

        [Test]
        public void ValidDisplayNameTest()
        {

            var services = new ServiceCollection();
            MyServiceRegistration.RegisterServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var personFactory = serviceProvider.GetService<IPersonFactory>();
          
            var displayNameClass = serviceProvider.GetService<IDisplayNames>();
           
            var names = personFactory.PersonList;

            names.Add(personFactory.CreatePerson("John", "Doe"));
            names.Add(personFactory.CreatePerson("Jane", "Mary", "Doe"));
            names.Add(personFactory.CreatePerson("Bob", "Smith"));
            names.Add(personFactory.CreatePerson("Alice", "Johnson"));
          

            // Redirect console output to StringWriter
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Display names
                displayNameClass.DisplayNameList(names);

                // Get console output as string
                string consoleOutput = sw.ToString();

                // Assert that the console output contains the expected names
                Assert.IsTrue(consoleOutput.Contains("Alice Johnson"));
                Assert.IsTrue(consoleOutput.Contains("Bob Smith"));
                Assert.IsTrue(consoleOutput.Contains("Jane Mary Doe"));
                Assert.IsTrue(consoleOutput.Contains("John Doe"));
            }

        }


    }
}
