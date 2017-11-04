using Mobile_math.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_math.AppLogic
{
    class MainLogic
    {
        Zadatak zadatak = new Zadatak();
        SettingsHandler settings = new SettingsHandler();

        public Zadatak RandomZadatak()
        {
            Random rand = new Random();
            int minNum = 0;
            var tempMin = settings.GetData("MinimalNumber") != null ? Int32.TryParse(settings.GetData("MinimalNumber").ToString(), out minNum) : false;
            int maxNum = 0;
            var tempMax = settings.GetData("MaximalNumber") != null ? Int32.TryParse(settings.GetData("MaximalNumber").ToString(), out maxNum) : false;

            zadatak.X = rand.Next(minNum, maxNum);
            zadatak.Y = rand.Next(minNum, maxNum);
            zadatak.Result = zadatak.X + zadatak.Y; //TODO implementirati ostale operacije osim zbrajanja
            zadatak.ZadatakString = zadatak.X.ToString() + "+" + zadatak.Y.ToString();
            return zadatak;
        }

        public bool CheckAnswer(int answer)
        {
            if (answer == zadatak.Result)
            {
                return true;
            }
            else
                return false;
        }
    }
}
