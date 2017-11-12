using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.CustomConfig
{
    public class Folder: ConfigurationElement
    {
        private const string ATTRIBUTE_NAME = "name";

        [ConfigurationProperty(ATTRIBUTE_NAME, DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Name
        {
            get
            {
                return this[ATTRIBUTE_NAME].ToString();
            }
           
        }
    }
}
