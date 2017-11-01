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

        public Zadatak RandomZadatak()
        {
            Random rand = new Random();
            zadatak.X = rand.Next(0, 11);
            zadatak.Y = rand.Next(0, 11);
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
