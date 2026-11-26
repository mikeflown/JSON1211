using System;

[Serializable]
public class DataBase
{
    public Data[] Items;
    public DataBase()
    {
        Items = new Data[0];
    }
    public DataBase(Data[] items)
    {
        Items = items;
    }
}
