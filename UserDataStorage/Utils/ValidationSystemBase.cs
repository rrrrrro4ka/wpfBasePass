using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDataStorage.Utils
{
    public class ValidationSystemBase : INotifyDataErrorInfo
    {
        private Dictionary<String, List<String>> errors =
        new Dictionary<string, List<string>>();
        private const string SYSTEM_ERROR = "Нужно заполнить поле";
        public void IsSystemValid(string value)
        {
            if (value.Length < 1)
            {
                AddError("AuthSystem", SYSTEM_ERROR, false);
            }
            else
            {
                RemoveError("AuthSystem", SYSTEM_ERROR);
            }
        }
        public void IsLoginContainsAnything(string value)
        {
            if (value.Length < 1)
            {
                AddError("Login", SYSTEM_ERROR, false);
            }
            else
            {
                RemoveError("Login", SYSTEM_ERROR);
            }
        }
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
        private void AddError(string propertyName, string error, bool isWarning)
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

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (String.IsNullOrEmpty(propertyName) ||
                !errors.ContainsKey(propertyName)) return null;
            return errors[propertyName];
        }

        public bool HasErrors
        {
            get { return errors.Count > 0; }
        }
        #endregion
    }
}
