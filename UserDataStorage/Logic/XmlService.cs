using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UserDataStorage.Models;
using System.Windows;

namespace UserDataStorage.Logic
{
    public class XmlService
    {
        private ObservableCollection<SystemInfo> userstoragelist = new ObservableCollection<SystemInfo>();
        private XDocument xmlSystemDb;
        private XDocument xmlPasswordDb;
        public ObservableCollection<SystemInfo>  UserStorageList
        {
            get { return userstoragelist; }
        }
        
        /// <summary>
        /// Конструктор, проверяем наличие файла. Если есть, то отображаем данные из него соответственно.
        /// </summary>
        public XmlService()
        {
            string path = @"UserSystems.xml";
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                XDocument xDoc = XDocument.Load(path);
                xmlSystemDb = xDoc;
                GenerateUserStorageToShow(xmlSystemDb);
            }
            else
            {

            }
        }

        /// <summary>
        /// Метод отображения данных при входе в личный кабинет
        /// </summary>
        /// <param name="XmlData"></param>
        private void GenerateUserStorageToShow(XDocument XmlData)
        {
            foreach (XElement systemElement in XmlData.Element("systems").Elements("system"))
            {
                SystemInfo onesysteminfo = new SystemInfo();
                onesysteminfo.AuthSystem = systemElement.Attribute("name").Value;
                onesysteminfo.Login = systemElement.Element("login").Value;
                onesysteminfo.PasswordSystem = systemElement.Element("password").Value;
                onesysteminfo.Website = systemElement.Element("website").Value;
                userstoragelist.Add(onesysteminfo);
            }
        }

        /// <summary>
        /// Метод сохранения данных по системам в xml.
        /// </summary>
        /// <param name="usersystem"></param>
        /// <param name="userAccountInformations"></param>
        public void SaveUserSystemToXml(SystemInfo usersystem, ObservableCollection<SystemInfo> userAccountInformations)
        {
            string path = @"UserSystems.xml";
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                bool changedobject = false;
                XElement root = xmlSystemDb.Element("systems");
                foreach (XElement xe in root.Elements("system"))
                {
                    if (xe.Attribute("name").Value == usersystem.AuthSystem)
                    {
                        xe.Element("login").Value = usersystem.Login;
                        xe.Element("password").Value = usersystem.PasswordSystem;
                        xe.Element("website").Value = usersystem.Website;
                        changedobject = true;
                        break;
                    }
                }
                if (!changedobject)
                {
                    root.Add(new XElement("system",
                                new XAttribute("name", usersystem.AuthSystem),
                                new XElement("login", usersystem.Login),
                                new XElement("password", usersystem.PasswordSystem),
                                new XElement("website", usersystem.Website)));
                    xmlSystemDb.Save("UserSystems.xml");
                }
                else
                {
                    xmlSystemDb.Save("UserSystems.xml");
                }
            }
            else
            {
                foreach (SystemInfo sys in userAccountInformations)
                {
                    XDocument xdoc = new XDocument();
                    XElement systems = new XElement("systems");
                    systems.Add(new XElement("system",
                    new XAttribute("name", sys.AuthSystem),
                    new XElement("login", sys.Login),
                    new XElement("password", sys.PasswordSystem),
                    new XElement("website", sys.Website)));
                    xdoc.Add(systems);
                    xdoc.Save("UserSystems.xml");
                }
                xmlSystemDb = XDocument.Load(path);
            }

        }

        /// <summary>
        /// Метод для удаления системы из xml.
        /// </summary>
        /// <param name="usersystem"></param>
        public void RemoveSystemFromXml(SystemInfo usersystem)
        {
            string path = @"UserSystems.xml";
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                XElement root = xmlSystemDb.Element("systems");
                foreach (XElement xe in root.Elements("system").ToList())
                {
                    if (xe.Attribute("name").Value == usersystem.AuthSystem)
                    {
                        xe.Remove();
                        xmlSystemDb.Save("UserSystems.xml");
                    }
                }
            }
        }

        /// <summary>
        /// Метод сохранения пароля в xml.
        /// </summary>
        /// <param name="password"></param>
        public void SaveUserPasswordToXml(CreateNewPassword password)
        {
            XDocument xDocPassword = new XDocument();
            XElement root = new XElement("password");
            root.Add(new XElement("pass",
                new XAttribute("value", password.AddPassword),
                new XElement("secret", password.AddSecretWord)));
            xDocPassword.Add(root);
            xDocPassword.Save("UserPassword.xml");
            xmlPasswordDb = xDocPassword;
        }

        /// <summary>
        /// Перегруженный метод заменяет пароль в xml.
        /// </summary>
        /// <param name="password"></param>
        public void SaveUserPasswordToXml(ChangeUserPassword password)
        {
            XDocument xDocPassword = new XDocument();
            XElement root = new XElement("password");
            root.Add(new XElement("pass",
                new XAttribute("value", password.Password),
                new XElement("secret", password.NewSecretWord)));
            xDocPassword.Add(root);
            xDocPassword.Save("UserPassword.xml");
            xmlPasswordDb = xDocPassword;
        }

        /// <summary>
        /// Удаление файла с паролем
        /// </summary>
        public void DeleteUserSystemsXml()
        {
            string path = @"UserSystems.xml";
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                fileInf.Delete();
            }
        }

        /// <summary>
        /// Метод сравнивает ключевые слова чтобы сменить пароль
        /// </summary>
        /// <param name="oldsecretword"></param>
        /// <returns></returns>
        public bool CompareSecretWords(string oldsecretword)
        {
            string path = @"UserPassword.xml";
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                XDocument Xpass = XDocument.Load(path);
                xmlPasswordDb = Xpass;
                XElement root = xmlPasswordDb.Element("password");
                foreach (XElement secret in root.Elements("pass").ToList())
                {
                    if (secret.Element("secret").Value == oldsecretword)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Метод сравнивает пароли, в БД и что вводит пользователь
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CompareUserPassword(string password)
        {
            string path = @"UserPassword.xml";
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                XDocument xPass = XDocument.Load(path);
                XElement root = xPass.Element("password");
                foreach (XElement pas in root.Elements("pass").ToList())
                {
                    if (pas.Attribute("value").Value == password)
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
            return false;
        }
    }
}
