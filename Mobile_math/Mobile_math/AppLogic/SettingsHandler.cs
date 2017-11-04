using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile_math.AppLogic
{
    public class SettingsHandler
    {
        public bool SaveData(string dataName, int data)
        {
            try
            {
                Application.Current.Properties[dataName] = data;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool SaveData(string dataName, string data)
        {
            try
            {
                Application.Current.Properties[dataName] = data;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public object GetData(string dataName)
        {
            if (Application.Current.Properties.ContainsKey(dataName))
            {
                return Application.Current.Properties[dataName];
            }
            else
                return null;
        }
    }
}
