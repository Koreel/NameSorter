using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Name_Sorter.ConsolePrompt;
using Name_Sorter.NameClasses;

//for adding sorting files to services
using Name_Sorter.Sorting;


namespace Name_Sorter.Factories
{
    //name factory that returns type based off the number of name parametres
    public class FullNameFactory : IPersonFactory
    {
        public FullNameFactory()
        {
            PersonList = new List<IPerson>();
        }

        public IPerson CreatePerson(params string[] nameParts)
        {
            switch (nameParts.Length)
            {
                case 2:
                    //return Full Name
                    return new FullName(nameParts[0], nameParts[1]);
                    
                case 3:
                    //return Full Name With Middle
                    return new FullNameWithMiddle(nameParts[0], nameParts[1], nameParts[2]);

                default:
                    //return Invalid Person
                    return new InvalidPerson(nameParts[0]);
            }

        }
        public List<IPerson> PersonList { get; set; }

    }
}

