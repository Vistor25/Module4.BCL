using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.CustomConfig
{
    public class Rule: ConfigurationElement
    {
        private const string ATTRIBUTE_PATTERN = "pattern";
        private const string ATTRIBUTE_DESTINATION = "folderDestination";
        private const string ATTRIBUTE_NUMBER = "shouldAddNumber";
        private const string ATTRIBUTE_MOVINGDATE = "shouldAddMovingDate";

        [ConfigurationProperty(ATTRIBUTE_PATTERN, DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Pattern
        {
            get
            {
                return this[ATTRIBUTE_PATTERN].ToString();
            }
            
        }

        [ConfigurationProperty(ATTRIBUTE_DESTINATION, DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Destination
        {
            get
            {
                return this[ATTRIBUTE_DESTINATION].ToString();
            }
           
        }

        [ConfigurationProperty(ATTRIBUTE_NUMBER, DefaultValue = "", IsKey = false, IsRequired = false)]
        public string ShouldAddNumber
        {
            get
            {
                return this[ATTRIBUTE_NUMBER].ToString();
            }
           
        }

        [ConfigurationProperty(ATTRIBUTE_MOVINGDATE, DefaultValue = "", IsKey = false, IsRequired = false)]
        public string ShouldAddMovingDate
        {
            get
            {
                return this[ATTRIBUTE_MOVINGDATE].ToString();
            }
           
        }
    }
}
