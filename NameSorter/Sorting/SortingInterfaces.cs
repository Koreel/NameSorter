using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Name_Sorter.NameClasses;

namespace Name_Sorter.Sorting
{
  

   

    public interface INameExtractor
    {
        public List<IPerson> ExtractNames(string fileText);
    }

    public interface INameSorter
    {
        public List<IPerson> SortNameList(List<IPerson> nameList);
    }

    public interface IDisplayNames
    {
    
        
        public void DisplayNameList(List<IPerson> nameList);
    }


}
