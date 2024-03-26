using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3.QA.Models
{
    public class DIPMC
    {
        public string MCCode { get; set; }
        public string MCName { get; set; }

        public static List<DIPMC> Gets()
        {
            return new List<DIPMC>() 
            { 
                new DIPMC() { MCCode = "S-8-1", MCName = "S-8-1" },
                new DIPMC() { MCCode = "S-8-2", MCName = "S-8-2" }
            };
        }
    }
}
