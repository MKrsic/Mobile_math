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
        Languages.Croatian language = new Languages.Croatian();
        MainLogic mainLogic = new MainLogic();
        SettingsHandler settings = new SettingsHandler();

        public SettingsPage()
        {
            InitializeComponent();

            //localization setup
            //lblMinNum.Text= language.MinimalNumber;
            //lblMaxNum.Text = language.MaximalNumber;
            //btnSave.Text = language.Save;

            lblMinNum.Text = AppResources.MinimalNumber;
            lblMaxNum.Text = AppResources.MaximalNumber;
            btnSave.Text = AppResources.Save;

            entryMinNum.Text = settings.GetData("MinimalNumber") != null ? settings.GetData("MinimalNumber").ToString() : "0";
            entryMaxNum.Text = settings.GetData("MaximalNumber") != null ? settings.GetData("MaximalNumber").ToString() : "0";
        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            settings.SaveData("MinimalNumber", Int32.Parse(entryMinNum.Text));
            settings.SaveData("MaximalNumber", Int32.Parse(entryMaxNum.Text));

            Navigation.PopAsync();
        }
    }
}