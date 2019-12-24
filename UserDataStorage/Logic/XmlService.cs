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
        public ObservableCollection<SystemInfo> userstoragelist = new ObservableCollection<SystemInfo>();
        private XDocument xml_system_db;
        private XDocument xml_password_db;
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
                xml_system_db = xDoc;
                GenerateUserStorageToShow(xml_system_db);
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
                XAttribute nameSystemAttribute = systemElement.Attribute("name");
                XElement loginSystemElement = systemElement.Element("login");
                XElement passwordSystemElement = systemElement.Element("password");
                XElement websiteSystemElement = systemElement.Element("website");
                onesysteminfo.AuthSystem = nameSystemAttribute.Value;
                onesysteminfo.Login = loginSystemElement.Value;
                onesysteminfo.PasswordSystem = passwordSystemElement.Value;
                onesysteminfo.Website = websiteSystemElement.Value;
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
                XElement root = xml_system_db.Element("systems");
                foreach (XElement xe in root.Elements("system").ToList())
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
                    xml_system_db.Save("UserSystems.xml");
                }
                else
                {
                    xml_system_db.Save("UserSystems.xml");
                }
            }
            else
            {
                foreach (SystemInfo sys in userAccountInformations)
                {
                    XDocument xdoc = new XDocument();
                    XElement one_system = new XElement("system");
                    XAttribute one_systemAttribute = new XAttribute("name", sys.AuthSystem);
                    XElement one_systemLogin = new XElement("login", sys.Login);
                    XElement one_systemPassword = new XElement("password", sys.PasswordSystem);
                    XElement one_systemsite = new XElement("website", sys.Website);
                    one_system.Add(one_systemAttribute);
                    one_system.Add(one_systemLogin);
                    one_system.Add(one_systemPassword);
                    one_system.Add(one_systemsite);
                    XElement systems = new XElement("systems");
                    systems.Add(one_system);
                    xdoc.Add(systems);
                    xdoc.Save("UserSystems.xml");
                }
                xml_system_db = XDocument.Load(path);
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
                XElement root = xml_system_db.Element("systems");
                foreach (XElement xe in root.Elements("system").ToList())
                {
                    if (xe.Attribute("name").Value == usersystem.AuthSystem)
                    {
                        xe.Remove();
                        xml_system_db.Save("UserSystems.xml");
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
            xml_password_db = xDocPassword;
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
            xml_password_db = xDocPassword;
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
                xml_password_db = Xpass;
                XElement root = xml_password_db.Element("password");
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
