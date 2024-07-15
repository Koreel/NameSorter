
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Name_Sorter.NameClasses
{
    //name classes
    public class FullName : IPerson
    {

        public string firstName { get; set; }
        public string lastName { get; set; }

        public FullName(string _firstName, string _lastName)
        {
            firstName = _firstName;
            lastName = _lastName;
        }
       
        public virtual string MyName()
        {
            
            return $"{firstName} {lastName}";
            
        }

    }

    public class FullNameWithMiddle : FullName
    {

        public string middleName { get; set; }
        public FullNameWithMiddle(string _firstName, string _middleName, string _lastName) : base(_firstName, _lastName)
        {
            firstName = _firstName;
            middleName = _middleName;
            lastName = _lastName;
        }

        public override string MyName()
        {
            return $"{firstName} {middleName} {lastName}";
        }


    }
    public class InvalidPerson : IPerson
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        private string invalidInput;

        public InvalidPerson(string input)
        {             
            invalidInput = input;
        }

        public string MyName()
        {
            return $"Invalid input: {invalidInput}";
        }
    }


}
