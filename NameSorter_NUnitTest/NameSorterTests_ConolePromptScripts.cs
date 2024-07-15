using Moq;
using Microsoft.Extensions.DependencyInjection;
using Name_Sorter;
using Name_Sorter.ConsolePrompt;
using Name_Sorter.FactoriesAndRegistrations;
using Name_Sorter.Sorting;
using System.Security.Cryptography.X509Certificates;
using Name_Sorter.NameClasses;

namespace NameSorter_NUnitTest
{
    public class NameSorterTests_ConolePrompts
    {

        //unit testing external names list because ./unsorted.nameslist
        string userInputTest = "name-sorter C:\\Users\\koree\\OneDrive\\Pictures\\Documents\\Test\\unsorted-names-list.txt";
        string invalidUserInputTest = "Invalid text yo";
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GetFileName_ReturnsCorrectFilePath()
        {
            // Arrange
            var fileName = "unsorted-names-list.txt";
            var expectedFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            var services = new ServiceCollection();
            MyServiceRegistration.RegisterServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var fileLocator = serviceProvider.GetService<IFileLocator>();

            // Act
            var actualFilePath = fileLocator.GetFileName(fileName);

            // Assert
            Assert.AreEqual(expectedFilePath, actualFilePath);
        }

        


        [Test]
        public void ReadNamesOnString()
        {
            var services = new ServiceCollection();
            MyServiceRegistration.RegisterServices(services);
            var serviceProvider = services.BuildServiceProvider();
          

            var readUserInput = serviceProvider.GetService<IUserInputReader>();
            readUserInput.ReadUserInput(userInputTest);

            // Act
            Action action = () => readUserInput.ReadUserInput(userInputTest);

            // Assert
         
            Assert.Pass();
        }

        [Test]
        public void GenerateFile_ValidInput_GeneratesFileWithExpectedContent()
        {
            var services = new ServiceCollection();
            MyServiceRegistration.RegisterServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var writer = serviceProvider.GetService<INameListWriteFile>();
            var personFactory = serviceProvider.GetService<IPersonFactory>();

            var names = personFactory.PersonList;

            names.Add(personFactory.CreatePerson("John", "Doe"));
            names.Add(personFactory.CreatePerson("Jane", "Mary", "Doe"));
            names.Add(personFactory.CreatePerson("Bob", "Smith"));
            names.Add(personFactory.CreatePerson("Alice", "Johnson"));
         

            var directory = Path.GetTempPath();

            // Generate the file
            writer.GenerateFile(names, directory);

            // Read the content of the file
            var filePath = Path.Combine(directory, "sorted-names-list-.txt");
            var fileContent = File.ReadAllText(filePath);

            // Assert the file content
            var expectedContent = "John Doe \nJane Mary Doe \nBob Smith \nAlice Johnson \n";
            Assert.AreEqual(expectedContent, fileContent);
        }

        [Test]
        public void ReadInvalidNamesOnString()
        {
            var services = new ServiceCollection();
            MyServiceRegistration.RegisterServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var readUserInput = serviceProvider.GetService<IUserInputReader>();
            readUserInput.ReadUserInput(invalidUserInputTest);
           
            // Assert that an exception is thrown

            Assert.Throws<Exception>(() => readUserInput.ReadUserInput(invalidUserInputTest));

            // If the exception is not thrown, the following line will fail the test with the given message
            Assert.Fail("Expected an exception to be thrown."); Assert.Fail(invalidUserInputTest);

        }


      
    }
}