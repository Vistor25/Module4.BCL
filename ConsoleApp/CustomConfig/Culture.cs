using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.CustomConfig
{
    public class Culture: ConfigurationElement
    {
        private const string ATTRIBUTE_LANGUAGE = "language";

        [ConfigurationProperty(ATTRIBUTE_LANGUAGE, DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Language
        {
            get
            {
                return this[ATTRIBUTE_LANGUAGE].ToString();
            }
            
        }
    }
}
