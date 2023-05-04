using Name_Sorter.NameClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Name_Sorter.ConsolePrompt
{
    public interface ILogger
    {
        public string Log(string message);

    }
     public interface IUserInputReader
    {
        void ReadUserInput(string readline);
    }
    public interface IFileLocator
    {
      
        string GetFileName(string fileName);
    }

    public interface IFileReader
    {
        public string FileContentsString(string _directory);

    }

    public interface INameListWriteFile
    {
      
        public void GenerateFile(List<IPerson> nameList, string _directory);
    }


    public interface IConsoleFunctions
    {
        public string directory { get; set; }
        public void InputFunction();
    }

    public interface IConsoleFunctionsFactory
    {


        public IConsoleFunctions GetConsoleFunctionClass(string functionName, string directory);
    }



}


