
using System;

public class IllegalLevelMapExeption : Exception
{
    private int ID;

    public IllegalLevelMapExeption(int iD)
    {
        ID = iD;
    }

    public int GetInvalidID()
    {
        return ID;
    }
}
