using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class FileRule
    {
        public string Pattern { get; set; }
        public string FolderDestination { get; set; }
        public bool ShouldAddNumber { get; set; }
        public bool ShouldAddMovingDate { get; set; }
    }
}
