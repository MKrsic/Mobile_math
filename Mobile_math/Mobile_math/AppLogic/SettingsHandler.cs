using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Mobile_math.AppLogic
{
    public class SettingsHandler
    {
        /// <summary>
        /// Saves integer data with entered name
        /// </summary>
        /// <param name="dataName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Saves string data with entered name
        /// </summary>
        /// <param name="dataName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Saves bool data with entered name
        /// </summary>
        /// <param name="dataName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SaveData(string dataName, bool data)
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
        /// <summary>
        /// Saves list of strings data with entered name
        /// </summary>
        /// <param name="dataName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SaveData(string dataName, List<string> data)
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

        /// <summary>
        /// Gets data with entered name from memory
        /// </summary>
        /// <param name="dataName"></param>
        /// <returns></returns>
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
