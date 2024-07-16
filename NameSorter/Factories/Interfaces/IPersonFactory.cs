using Name_Sorter.NameClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Name_Sorter.Factories
{
    public interface IPersonFactory
    {
        IPerson CreatePerson(params string[] namePart);
        List<IPerson> PersonList { get; set; }
    }
}
