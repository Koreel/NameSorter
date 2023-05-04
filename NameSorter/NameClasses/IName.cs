using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Name_Sorter.NameClasses
{
   
    public interface IPerson
    {
        public string firstName { get; set; }
       
        public string lastName { get; set; }
        public string MyName();
    }

    public interface IPersonFactory
    {
       IPerson CreatePerson( params string[] namePart);
        List<IPerson> PersonList { get; set; }
    }

   
}
