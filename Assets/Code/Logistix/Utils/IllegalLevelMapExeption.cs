
using System;

namespace Logistix.Utils
{
    public class IllegalLevelMapExeption : Exception
    {
        private string ID;

        public IllegalLevelMapExeption(string iD)
        {
            ID = iD;
        }

        public string GetInvalidID()
        {
            return ID;
        }
    }
}