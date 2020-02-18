using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDataStorage.Utils;
using UserDataStorage.Models;
using UserDataStorage.Logic;

namespace UserDataStorage.ViewModels
{
    class UserInformationViewModel : BaseUserViewModel
    {
        private SystemInfo selectedSystem;
        private DelegateCommand saveUserSystem;
        private DelegateCommand addNewSystemCommand;
        private DelegateCommand removeSystemCommand;
        private ObservableCollection<SystemInfo> systemInfoCollection;
        XmlService xml = new XmlService();
        public UserInformationViewModel()
        {
            systemInfoCollection = new ObservableCollection<SystemInfo>();
            systemInfoCollection = xml.UserStorageList;
        }
        public SystemInfo SelectedSystem
        {
            get { return selectedSystem; }
            set
            {
                if (value != selectedSystem)
                {
                    selectedSystem = value;
                    OnPropertyChanged(nameof(SelectedSystem));
                    RemoveUserSystemCommand.RaiseCanExecuteChanged();
                    saveUserSystem.RaiseCanExecuteChanged();
                }
            }
        }
        public ObservableCollection<SystemInfo> UserAccountInformations
        {
            get { return systemInfoCollection; }
            set
            {
                systemInfoCollection = value;
                OnPropertyChanged(nameof(UserAccountInformations));
            }
        }

        #region Command

        /// <summary>
        /// Команда добавления новой системы в лист пользовательских систем.
        /// </summary>
        public DelegateCommand AddNewSystemCommand
        {
            get
            {
                return addNewSystemCommand ?? (addNewSystemCommand = new DelegateCommand(param => AddNewSystem()));
            }
        }

        /// <summary>
        /// Метод который используется в команде для добавления новой системы. Он отвечает за логику.
        /// </summary>
        private void AddNewSystem()
        {
            systemInfoCollection.Add(new SystemInfo() { AuthSystem = "Новая", Login = "Новый", PasswordSystem = "Новый", Website = "Сайт" });
        }

        /// <summary>
        /// Команда для сохранения данных о системе пользователя.
        /// </summary>
        public DelegateCommand SaveUserSystemCommand
        {
            get
            {
                if (SelectedSystem == null)
                {
                    saveUserSystem = new DelegateCommand(SaveUserSystems, CanSaveUserSystem);
                }
                return saveUserSystem;
            }
        }

        /// <summary>
        /// Метод сохранения данных о системе. Пишет данные в xml.
        /// </summary>
        /// <param name="args"></param>
        private void SaveUserSystems(object args)
        {
            xml.SaveUserSystemToXml(SelectedSystem, UserAccountInformations);
        }

        /// <summary>
        /// Метод, который проверяет можем ли мы сохранять систему и если можем, то даёт добро для команды.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private bool CanSaveUserSystem(object args)
        {
            if (SelectedSystem == null)
            {
                return false;
            }
            if (String.IsNullOrEmpty(SelectedSystem.AuthSystem) || String.IsNullOrEmpty(SelectedSystem.Login)
                || String.IsNullOrEmpty(SelectedSystem.PasswordSystem))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Команда по удалению системы из общего списка.
        /// </summary>
        public DelegateCommand RemoveUserSystemCommand
        {
            get
            {
                return removeSystemCommand ?? (removeSystemCommand = new DelegateCommand(RemoveSystem, CanRemoveSystem));
            }
        }

        /// <summary>
        /// Метод удаления системы из общего списка, а так из xml.
        /// </summary>
        /// <param name="args"></param>
        private void RemoveSystem(object args)
        {
            xml.RemoveSystemFromXml(SelectedSystem);
            UserAccountInformations.Remove(SelectedSystem);
        }

        /// <summary>
        /// Метод, который проверяет можно ли удалить нам систему из списка. Ищёт её и удаляет из списка и xml.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private bool CanRemoveSystem(object args)
        {
            if (SelectedSystem == null)
            {
                return false;
            }
            var systemInfo = FindSystem(SelectedSystem);
            if (systemInfo == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        /// <summary>
        /// Метод поиска нужной системы в списке систем пользователя.
        /// </summary>
        /// <param name="usersystem"></param>
        /// <returns></returns>
        private SystemInfo FindSystem(SystemInfo usersystem)
        {
            if (usersystem == null)
            {
                return null;
            }
            else
            {
                return systemInfoCollection.FirstOrDefault(UserInformation => UserInformation.AuthSystem == usersystem.AuthSystem
                    && UserInformation.Login == usersystem.Login
                    && UserInformation.PasswordSystem == usersystem.PasswordSystem
                    && UserInformation.Website == usersystem.Website);
            }
        }
    }
}
