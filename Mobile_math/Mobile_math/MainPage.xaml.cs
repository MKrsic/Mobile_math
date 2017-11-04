using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Mobile_math.Models;
using Mobile_math.AppLogic;

namespace Mobile_math
{
    public partial class MainPage : ContentPage
    {
        Languages.Croatian language = new Languages.Croatian();
        MainLogic mainLogic = new MainLogic();
        Zadatak zadatak = new Zadatak();

        public MainPage()
        {
            InitializeComponent();

            //localization setup
            entryAnswer.Placeholder = language.Answer;
            btnCheckAnswer.Text = language.CheckAnswer;
            btnSettings.Text = language.Settings;

            //first task setup
            SetRandomZadatakDisplay();
        }

        private void BtnCheckAnswer_Clicked(object sender, EventArgs e)
        {
            int answer = 0;
            Int32.TryParse(entryAnswer.Text, out answer);

            if (mainLogic.CheckAnswer(answer))
            {
                DisplayAlert("", "Točno!!!", "OK");
                SetRandomZadatakDisplay();
            }
            else
            {
                DisplayAlert("", "Krivo!!!", "OK");
                entryAnswer.Focus();
            }
        }

        private void BtnSettings_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage());
        }

        private void SetRandomZadatakDisplay()
        {
            zadatak = mainLogic.RandomZadatak();
            labelZadatak.Text = zadatak.ZadatakString;
            entryAnswer.Text = "";
            entryAnswer.Focus();
        }
    }
}
