using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Table1
{
    public class Settings<T> where T : ApplicationSettings, new()
    {
        private T mSettings;
        private String file;
        public T Load()
        {
            return Load("config.xml");
        }

        public T Load(String fileName)
        {
            file = fileName;

            mSettings = new T();

            XmlSerializer serializer = null;
            FileStream reader = null;

            try
            {
                reader = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            }
            catch
            {
                //файл настроек отсутствует

                //инициализация настроек по умолчанию
                mSettings.file = fileName;
                mSettings.SetDefault();
                mSettings.PostLoad();
                mSettings.Save();
                return mSettings;
            }
            try
            {
                serializer = new XmlSerializer(typeof(T), new XmlRootAttribute("Settings"));
                mSettings = (T)serializer.Deserialize(reader);
            }
            catch
            {
                //файл настроек повреждён или имеет неверную структуру

                try
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                    //создание резервной копии файла с ошибками
                    if (File.Exists(fileName))
                    {
                        File.Copy(fileName, BackupFileName(fileName));
                    }
                }
                catch
                {
                }

                //инициализация настроек по умолчанию
                mSettings.file = fileName;
                mSettings.SetDefault();
                mSettings.PostLoad();
                mSettings.Save();
                return mSettings;
            }

            if (reader != null)
            {
                reader.Close();
            }

            //установка имени файла
            mSettings.file = fileName;

            //установка дополнительных связей между полями в объекте настроек
            mSettings.PostLoad();

            return mSettings;
        }

        public void Save()
        {
            XmlSerializer serializer;
            FileStream fileStream = null;
            try
            {
                serializer = new XmlSerializer(typeof(T), new XmlRootAttribute("Settings"));
                fileStream = new FileStream(file, FileMode.Create, FileAccess.Write);
                serializer.Serialize(fileStream, mSettings);
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

        private static String BackupFileName(String fileName)
        {
            int i = 1;
            while (File.Exists(fileName + i))
            {
                i++;
            }
            return fileName + i;
        }
    }
}

       
