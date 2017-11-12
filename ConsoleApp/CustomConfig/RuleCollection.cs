using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.CustomConfig
{
    public class RuleCollection : ConfigurationElementCollection
    {
        public RuleCollection()
        {
            this.AddElementName = "Rule";
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new Rule();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as Rule).Destination;
        }

        public Rule this[int index]
        {
            get { return base.BaseGet(index) as Rule; }
        }
    }
}
