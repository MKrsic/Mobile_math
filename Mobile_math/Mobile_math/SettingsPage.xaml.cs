using Mobile_math.AppLogic;
using Mobile_math.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile_math
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    { 
        MainLogic mainLogic = new MainLogic();
        SettingsHandler settings = new SettingsHandler();

        public SettingsPage()
        {
            InitializeComponent();

            //localization setup
            lblMinNum.Text = AppResources.MinimalNumber;
            lblMaxNum.Text = AppResources.MaximalNumber;
            lblAdd.Text = AppResources.Add;
            lblSubstract.Text = AppResources.Substract;
            lblMultiply.Text = AppResources.Multiply;
            lblDivide.Text = AppResources.Divide;
            lblAllowNegativeValues.Text = AppResources.AllowNegativeValues;

            btnSave.Text = AppResources.Save;

            entryMinNum.Text = settings.GetData("MinimalNumber") != null ? settings.GetData("MinimalNumber").ToString() : "0";
            entryMaxNum.Text = settings.GetData("MaximalNumber") != null ? settings.GetData("MaximalNumber").ToString() : "0";

            checkAdd.IsToggled = settings.GetData("FuncAdd") != null ? Boolean.Parse(settings.GetData("FuncAdd").ToString()) : false;
            checkSubstract.IsToggled = settings.GetData("FuncSubstract") != null ? Boolean.Parse(settings.GetData("FuncSubstract").ToString()) : false;
            checkMultiply.IsToggled = settings.GetData("FuncMultiply") != null ? Boolean.Parse(settings.GetData("FuncMultiply").ToString()) : false;
            checkDivide.IsToggled = settings.GetData("FuncDivide") != null ? Boolean.Parse(settings.GetData("FuncDivide").ToString()) : false;
            checkAllowNegativeValues.IsToggled = settings.GetData("AllowNegativeValues") != null ? Boolean.Parse(settings.GetData("AllowNegativeValues").ToString()) : false;
        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            SaveSettings();
            Navigation.PopAsync();
        }

        private void SaveSettings()
        {
            settings.SaveData("MinimalNumber", Int32.Parse(entryMinNum.Text));
            settings.SaveData("MaximalNumber", Int32.Parse(entryMaxNum.Text));

            settings.SaveData("FuncAdd", checkAdd.IsToggled);
            settings.SaveData("FuncSubstract", checkSubstract.IsToggled);
            settings.SaveData("FuncMultiply", checkMultiply.IsToggled);
            settings.SaveData("FuncDivide", checkDivide.IsToggled);
            settings.SaveData("AllowNegativeValues", checkAllowNegativeValues.IsToggled);
        }
    }
}