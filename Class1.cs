using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Table1
{
    public abstract class ApplicationSettings
    {
        protected internal String file;

        protected abstract void Reset();
        public void SetDefault()
        {
            Reset();
        }
        public abstract void PostLoad();
        public void Save()
        {
            XmlSerializer serializer;
            FileStream fileStream = null;
            try
            {
                serializer = new XmlSerializer(this.GetType(), new XmlRootAttribute("Settings"));
                fileStream = new FileStream(file, FileMode.Create, FileAccess.Write);
                serializer.Serialize(fileStream, this);
                fileStream.Close();
            }
            catch
            {
            }

            if (fileStream != null)
            {
                fileStream.Close();
            }
        }
    }
}
