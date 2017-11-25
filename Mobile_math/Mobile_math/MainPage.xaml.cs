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
        SettingsHandler settings = new SettingsHandler();
        int numOfCurrentTask = 1;
        int numOfTasks = 0;
        int numOfCorrect = 0;
        int numOfWrong = 0;
        int tempWrong = 0;

        public MainPage()
        {
            InitializeComponent();

            //localization setup
            entryAnswer.Placeholder = AppResources.Answer;
            btnCheckAnswer.Text = AppResources.CheckAnswer;
            btnSettings.Text = AppResources.Settings;

            //numOfTasks = settings.GetData("NumOfTasksInSeries") != null ? Int32.Parse(settings.GetData("NumOfTasksInSeries").ToString()) : 0;
        }
        protected override void OnAppearing()
        {
            SetRandomZadatakDisplay();
            numOfTasks = settings.GetData("NumOfTasksInSeries") != null ? Int32.Parse(settings.GetData("NumOfTasksInSeries").ToString()) : 0;
        }

        private void BtnCheckAnswer_Clicked(object sender, EventArgs e)
        {
            int answer = 0;
            Int32.TryParse(entryAnswer.Text, out answer);

            if (mainLogic.CheckAnswer(answer))
            {
                DisplayAlert("", "Točno!!!", "OK");
                SetRandomZadatakDisplay();
                numOfCurrentTask++;
                numOfCorrect++;
                if (tempWrong < numOfWrong)
                {
                    tempWrong = numOfWrong;
                }
                else if (numOfCurrentTask >= numOfTasks)
                {
                    DisplayAlert("", "Točno: " + numOfCorrect.ToString() + " Krivo: " + numOfWrong, "OK");
                    numOfCurrentTask = 0;
                    numOfCorrect = 0;
                    numOfWrong = 0;
                }
            }
            else
            {
                DisplayAlert("", "Krivo!!!", "OK");
                entryAnswer.Focus();
                numOfWrong++;
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
