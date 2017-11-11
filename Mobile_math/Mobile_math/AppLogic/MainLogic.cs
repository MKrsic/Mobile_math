using Mobile_math.Models;
using System;
using System.Collections.Generic;

namespace Mobile_math.AppLogic
{
    class MainLogic
    {
        Zadatak zadatak = new Zadatak();
        SettingsHandler settings = new SettingsHandler();
        enum Operations : int
        {
            Add = 1,
            Substract = 2,
            Multiply = 3,
            Divide = 4
        };

        public Zadatak RandomZadatak()
        {
            Random rand = new Random();
            int minNum = 0;
            var tempMin = settings.GetData("MinimalNumber") != null ? Int32.TryParse(settings.GetData("MinimalNumber").ToString(), out minNum) : false;
            int maxNum = 0;
            var tempMax = settings.GetData("MaximalNumber") != null ? Int32.TryParse(settings.GetData("MaximalNumber").ToString(), out maxNum) : false;

            zadatak.X = rand.Next(minNum, maxNum);
            zadatak.Y = rand.Next(minNum, maxNum);

            int randOper = RandomOperation();

            bool allowNegativeValues = settings.GetData("AllowNegativeValues") != null ? Boolean.Parse(settings.GetData("AllowNegativeValues").ToString()) : false;

            switch (randOper)
            {
                case 1:
                    zadatak.Result = zadatak.X + zadatak.Y;
                    zadatak.ZadatakString = zadatak.X.ToString() + "+" + zadatak.Y.ToString();
                    break;
                case 2:
                    if(zadatak.Y > zadatak.X && !allowNegativeValues)
                    {
                        var temp = zadatak.X;
                        zadatak.X = zadatak.Y;
                        zadatak.Y = temp;
                    }
                    zadatak.Result = zadatak.X - zadatak.Y;
                    zadatak.ZadatakString = zadatak.X.ToString() + "-" + zadatak.Y.ToString();
                    break;
                case 3:
                    zadatak.Result = zadatak.X * zadatak.Y;
                    zadatak.ZadatakString = zadatak.X.ToString() + "*" + zadatak.Y.ToString();
                    break;
                case 4:
                    //NoDecimalDivision();
                    zadatak.Result = zadatak.X / zadatak.Y;
                    zadatak.ZadatakString = zadatak.X.ToString() + "/" + zadatak.Y.ToString();
                    break;
                default:
                    zadatak.Result = zadatak.X + zadatak.Y;
                    zadatak.ZadatakString = zadatak.X.ToString() + "+" + zadatak.Y.ToString();
                    break;
            }

            return zadatak;
        }

        //TODO: popraviti funkciju koja će osigurati da generirani brojevi za dijeljenje budu djeljivi tako da rezultat bude cjelobrojan
        private void NoDecimalDivision()
        {
            var temp = Double.Parse(zadatak.X.ToString()) / Double.Parse(zadatak.Y.ToString());

            var diff = Math.Abs(Math.Truncate(temp) - temp);
            bool x = (diff > 0) && (diff < 1);
            if (x)
            {
                int minNum = 0;
                var tempMin = settings.GetData("MinimalNumber") != null ? Int32.TryParse(settings.GetData("MinimalNumber").ToString(), out minNum) : false;
                int maxNum = 0;
                var tempMax = settings.GetData("MaximalNumber") != null ? Int32.TryParse(settings.GetData("MaximalNumber").ToString(), out maxNum) : false;

                if (zadatak.X > minNum)
                    zadatak.X--;
                else if (zadatak.Y < maxNum)
                    zadatak.Y++;
                else if (zadatak.X == maxNum && zadatak.Y == maxNum)
                {
                    zadatak.X = maxNum;
                    zadatak.Y = 1;
                }

                NoDecimalDivision();
            }
        }

        private int RandomOperation()
        {
            bool add = settings.GetData("FuncAdd") != null ? Boolean.Parse(settings.GetData("FuncAdd").ToString()) : false;
            bool substract = settings.GetData("FuncSubstract") != null ? Boolean.Parse(settings.GetData("FuncSubstract").ToString()) : false;
            bool multiply = settings.GetData("FuncMultiply") != null ? Boolean.Parse(settings.GetData("FuncMultiply").ToString()) : false;
            bool divide = settings.GetData("FuncDivide") != null ? Boolean.Parse(settings.GetData("FuncDivide").ToString()) : false;

            Random rand = new Random();

            List<int> selectedOperations = new List<int>();
            if (add) selectedOperations.Add(1);
            if (substract) selectedOperations.Add(2);
            if (multiply) selectedOperations.Add(3);
            if (divide) selectedOperations.Add(4);

            int randomOperation = rand.Next(selectedOperations.ToArray().Length);
            int operation = 0;
            try
            {
                operation = selectedOperations[randomOperation];
            }
            catch
            {
                return 0;
            }
            return operation;
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
