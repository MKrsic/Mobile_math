using System;
using Xamarin.Forms;
using Mobile_math.Models;
using Mobile_math.AppLogic;
using System.Collections.Generic;

namespace Mobile_math
{
    public partial class MainPage : ContentPage
    {
        MainLogic mainLogic = new MainLogic();
        Zadatak zadatak = new Zadatak();
        SettingsHandler settings = new SettingsHandler();
        /// <summary>
        /// Number of current task
        /// </summary>
        int numOfCurrentTask = 1;
        /// <summary>
        /// Total number of tasks in series
        /// </summary>
        int numOfTasks = 0;
        /// <summary>
        /// Total number of correct answers
        /// </summary>
        int numOfCorrect = 0;
        /// <summary>
        /// Number of wrong answers
        /// </summary>
        int numOfWrong = 0;
        /// <summary>
        /// Temporary number of wrong answers for detection of multiple answers on same question
        /// </summary>
        int tempWrong = 0;
        /// <summary>
        /// Total number of wrong answers
        /// </summary>
        int totalWrongCount = 0;
        /// <summary>
        /// List of all generated tasks used for display after finishing task series
        /// </summary>
        List<string> QA = new List<string>();

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
            SetTaskNumDisplay(numOfCurrentTask);
            numOfTasks = settings.GetData("NumOfTasksInSeries") != null ? Int32.Parse(settings.GetData("NumOfTasksInSeries").ToString()) : 0;
        }

        private void BtnCheckAnswer_Clicked(object sender, EventArgs e)
        {
            int answer = 0;
            Int32.TryParse(entryAnswer.Text, out answer);

            if (mainLogic.CheckAnswer(answer))
            {
                numOfCorrect++;
                QA.Add(zadatak.ZadatakString + "= " + entryAnswer.Text + " \u221A");

                //if taks was first answered wrong, dont count it as correct
                if (tempWrong < numOfWrong)
                {
                    tempWrong = numOfWrong;
                    totalWrongCount++;
                    QA[QA.Count - 1] = zadatak.ZadatakString + "= " + entryAnswer.Text;
                }
                //if all tasks in series are solved, show final message
                if (numOfCurrentTask >= numOfTasks)
                {
                    string QAString = "";
                    foreach (var QA in QA)
                    {
                        QAString = QAString + QA + "\n";
                    }
                    QA.Clear();
                    DisplayAlert("", String.Format(AppResources.SummaryMessage, (numOfTasks - totalWrongCount), numOfTasks, "\n" ,QAString), "OK");
                    numOfCurrentTask = 1;
                    numOfCorrect = 0;
                    numOfWrong = 0;
                    totalWrongCount = 0;
                    tempWrong = 0;
                    SetRandomZadatakDisplay();
                    SetTaskNumDisplay(1);

                    return;
                }
                DisplayAlert("", AppResources.Correct, "OK");
                SetRandomZadatakDisplay();
                numOfCurrentTask++;
                SetTaskNumDisplay(numOfCurrentTask);
            }
            else
            {
                DisplayAlert("", AppResources.Wrong, "OK");
                entryAnswer.Focus();
                numOfWrong++;
            }
        }

        private void BtnSettings_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage());
        }

        /// <summary>
        /// Displays random task
        /// </summary>
        private void SetRandomZadatakDisplay()
        {
            if (numOfCurrentTask == 1)
                SetTaskNumDisplay(numOfCurrentTask);
            zadatak = mainLogic.RandomZadatak();
            labelZadatak.Text = zadatak.ZadatakString;
            entryAnswer.Text = "";
            entryAnswer.Focus();
        }

        /// <summary>
        /// Displays number of current task
        /// </summary>
        /// <param name="taskNum"></param>
        private void SetTaskNumDisplay(int taskNum)
        {
            labelTaskNum.Text = AppResources.TaskNumber + taskNum.ToString() + "/" + numOfTasks.ToString();
        }
    }
}
