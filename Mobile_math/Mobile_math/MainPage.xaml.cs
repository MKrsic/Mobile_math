using System;
using Xamarin.Forms;
using Mobile_math.Models;
using Mobile_math.AppLogic;

namespace Mobile_math
{
    public partial class MainPage : ContentPage
    {
        MainLogic mainLogic = new MainLogic();
        Zadatak zadatak = new Zadatak();

        public MainPage()
        {
            InitializeComponent();

            //localization setup
            entryAnswer.Placeholder = AppResources.Answer;
            btnCheckAnswer.Text = AppResources.CheckAnswer;
            btnSettings.Text = AppResources.Settings;
        }
        protected override void OnAppearing()
        {
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

            //lblFirstNum.Text = zadatak.X.ToString();
            //lblSecondNum.Text = zadatak.Y.ToString();
        }
    }
}
