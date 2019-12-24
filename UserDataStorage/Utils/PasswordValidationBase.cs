using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDataStorage.Utils
{
    public class PasswordValidationBase : INotifyDataErrorInfo
    {
        private Dictionary<String, List<String>> errors =
        new Dictionary<string, List<string>>();
        private const string SYSTEM_ERROR = "Нужно заполнить поле";
        public void IsPasswordContainsAnything(string value)
        {
            if (value.Length < 1)
            {
                AddError("PasswordSystem", SYSTEM_ERROR, false);
            }
            else
            {
                RemoveError("PasswordSystem", SYSTEM_ERROR);
            }
        }
        /// <summary>
        /// Метод добавления ошибки в лист со всеми соответствующими проверками
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="error"></param>
        /// <param name="isWarning"></param>
        public void AddError(string propertyName, string error, bool isWarning)
        {
            if (!errors.ContainsKey(propertyName))
                errors[propertyName] = new List<string>();

            if (!errors[propertyName].Contains(error))
            {
                if (isWarning) errors[propertyName].Add(error);
                else errors[propertyName].Insert(0, error);
                RaiseErrorsChanged(propertyName);
            }
        }
        /// <summary>
        /// Метод удаления ошибки из листа
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="error"></param>
        private void RemoveError(string propertyName, string error)
        {
            if (errors.ContainsKey(propertyName) &&
                errors[propertyName].Contains(error))
            {
                errors[propertyName].Remove(error);
                if (errors[propertyName].Count == 0) errors.Remove(propertyName);
                RaiseErrorsChanged(propertyName);
            }
        }
        private void RaiseErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #region INotifyDataErrorInfo Members
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        /// <summary>
        /// Возвращаем коллекцию ошибок определенного типа
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (String.IsNullOrEmpty(propertyName) ||
                !errors.ContainsKey(propertyName)) return null;
            return errors[propertyName];
        }
        /// <summary>
        /// Наличие ошибок в листе
        /// </summary>
        public bool HasErrors
        {
            get { return errors.Count > 0; }
        }
        #endregion
    }
}
