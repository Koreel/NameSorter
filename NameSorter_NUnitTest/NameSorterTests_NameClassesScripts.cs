using Microsoft.Extensions.DependencyInjection;
using Name_Sorter.FactoriesAndRegistrations;
using Name_Sorter.NameClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter_NUnitTest
{
    public class NameSorterTests_NameClassesScripts
    {

        [Test]
        public void CreatePerson_ValidNumberOfNameParts_ThrowsArgumentException()
        {
            var services = new ServiceCollection();
            MyServiceRegistration.RegisterServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var personFactory = serviceProvider.GetService<IPersonFactory>();
            // Arrange
            var nameParts = new string[] { "John", "Doe" };

            // Act
            var person = personFactory.CreatePerson(nameParts);

            // Assert
            Assert.IsNotNull(person);
        }


        [Test]
        public void CreatePerson_InvalidNumberOfNameParts_ThrowsArgumentException()
        {
            var services = new ServiceCollection();
            MyServiceRegistration.RegisterServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var personFactory = serviceProvider.GetService<IPersonFactory>();
            // Arrange
            var nameParts = new string[] { "John", "Doe", "Middle", "Last" };


            // Act and Assert
            Assert.Throws<ArgumentException>(() => personFactory.CreatePerson(nameParts));
        }
        [Test]
        public void CreateValidPersonList_()
        {
            var services = new ServiceCollection();
            MyServiceRegistration.RegisterServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var personFactory = serviceProvider.GetService<IPersonFactory>();

            var nameList = personFactory.PersonList;

            nameList.Add(personFactory.CreatePerson("John", "Doe"));
            nameList.Add(personFactory.CreatePerson("Jane", "Mary", "Doe"));
            nameList.Add(personFactory.CreatePerson("Bob", "Smith"));
            nameList.Add(personFactory.CreatePerson("Alice", "Johnson"));

            // Arrange - create an invalid list of names
            

            // Act and Assert - create a person for each invalid name and assert that an ArgumentException is thrown
            foreach (var name in nameList)
            {
                // Assert
                Assert.IsNotNull(name);
            }
        }

        [Test]
        public void CreateInvalidPersonList_ThrowsArgumentException()
        {
            var services = new ServiceCollection();
            MyServiceRegistration.RegisterServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var fullNameFactory = serviceProvider.GetService<IPersonFactory>();
            var invalidNamesList = fullNameFactory.PersonList;
            invalidNamesList.Add(fullNameFactory.CreatePerson("John"));
            invalidNamesList.Add(fullNameFactory.CreatePerson(" \"John\", \"Doe\", \"Middle\", \"Last\""));
            invalidNamesList.Add(fullNameFactory.CreatePerson("\"John\", \"Doe\", \"Middle\""));
            invalidNamesList.Add(fullNameFactory.CreatePerson(""));


            // Act nMW Assert - create a person for each invalid name and assert that an ArgumentException is thrown
            foreach (IPerson name in invalidNamesList)
            {
                Assert.IsNull(name);
            }
        }
    }

}
