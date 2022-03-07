using System;

namespace Logistix.Utils
{
    class BoxNotExistsExeption : Exception
    {
        private int ID;

        public BoxNotExistsExeption(int id)
        {
            ID = id;
        }

        public int GetID()
        {
            return ID;
        }

        public override string ToString()
        {
            return $"Box not found with this id: {ID}";
        }
    }
}