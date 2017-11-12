using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.CustomConfig
{
    public class FolderCollection : ConfigurationElementCollection
    {
        public FolderCollection()
        {
            this.AddElementName = "Folder";
        }

        public Folder this[int index]
        {
            get { return base.BaseGet(index) as Folder; }
        }
        protected override ConfigurationElement CreateNewElement()
        {
            return new Folder();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as Folder).Name;
        }
    }
}
