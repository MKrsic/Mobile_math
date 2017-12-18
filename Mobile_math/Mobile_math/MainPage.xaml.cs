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
        int totalWrongCount = 0;

        public MainPage()
        {
            InitializeComponent();

            //localization setup
            entryAnswer.Placeholder = AppResources.Answer;
            btnCheckAnswer.Text = AppResources.CheckAnswer;
            btnSettings.Text = AppResources.Settings;

            numOfTasks = settings.GetData("NumOfTasksInSeries") != null ? Int32.Parse(settings.GetData("NumOfTasksInSeries").ToString()) : 0;
            SetTaskNumDisplay(1);
        }
        protected override void OnAppearing()
        {
            SetRandomZadatakDisplay();
            SetTaskNumDisplay(1);
            numOfTasks = settings.GetData("NumOfTasksInSeries") != null ? Int32.Parse(settings.GetData("NumOfTasksInSeries").ToString()) : 0;
        }

        private void BtnCheckAnswer_Clicked(object sender, EventArgs e)
        {
            int answer = 0;
            Int32.TryParse(entryAnswer.Text, out answer);

            if (mainLogic.CheckAnswer(answer))
            {
                numOfCorrect++;

                //if taks was first answered wrong, dont count it as correct
                if (tempWrong < numOfWrong)
                {
                    tempWrong = numOfWrong;
                    totalWrongCount++;
                }
                //if all tasks in series are solved, show final message
                else if (numOfCurrentTask >= numOfTasks)
                {
                    //DisplayAlert("", "Točno: " + numOfCorrect.ToString() + " Krivo: " + totalWrongCount, "OK");
                    DisplayAlert("", "Točno riješeni svi zadatci, a od toga " + (numOfTasks - totalWrongCount) + " od prve.", "OK");
                    ////--
                    //var modalPage = new PopupCorrect();
                    //Navigation.PushModalAsync(modalPage);
                    ////--
                    numOfCurrentTask = 1;
                    numOfCorrect = 0;
                    numOfWrong = 0;
                    totalWrongCount = 0;
                    tempWrong = 0;
                    SetRandomZadatakDisplay();
                    SetTaskNumDisplay(1);
                    return;
                }
                DisplayAlert("", "Točno!!!", "OK");
                SetRandomZadatakDisplay();
                numOfCurrentTask++;
                SetTaskNumDisplay(numOfCurrentTask);
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
            if (numOfCurrentTask == 1)
                SetTaskNumDisplay(numOfCurrentTask);
            zadatak = mainLogic.RandomZadatak();
            labelZadatak.Text = zadatak.ZadatakString;
            entryAnswer.Text = "";
            entryAnswer.Focus();
        }

        private void SetTaskNumDisplay(int taskNum)
        {
            labelTaskNum.Text = AppResources.TaskNumber + taskNum.ToString() + "/" + numOfTasks.ToString();
        }
    }
}
