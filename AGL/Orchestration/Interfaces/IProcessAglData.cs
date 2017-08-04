using AGL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGL.Orchestration.Interfaces
{
   public interface IProcessAglData
    {
       Task<AGLData> Process();
       List<Person> ProcessData(List<Person> person, string type);
    }
}
