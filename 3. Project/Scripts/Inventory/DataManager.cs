using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TableType
{
    Item,
    CharacterTable,
    LocalTable,
    SelectTable,
}

public static class DataManager
{
    private static Dictionary<TableType, LowBase> tableDic = new Dictionary<TableType, LowBase>();

    public static LowBase Get(TableType type) => tableDic[type];

    public static ICollection<int> GetKeys(TableType table)
    {
        if (tableDic.ContainsKey(table))
            return tableDic[table].keys;

        return null;
    }

    public static LowBase Load(TableType table, string path = "Data/")
    {
        // 이미 로드되어 있다면 리턴처리.
        if ( tableDic.ContainsKey(table) )
            return tableDic[table];

        TextAsset asset = Resources.Load<TextAsset>(path + table);

        // 에셋이 로드되지 못했다면 null값을 리턴.
        if ( asset ==null ) return null;

        LowBase lowbase = new LowBase();
        lowbase.Load(asset.text);
        tableDic.Add(table, lowbase);

        return lowbase;
    }

    public static int ToInteger(TableType table, int tableID, string subject)
    {
        if (!tableDic.ContainsKey(table))
            return -1;
        return tableDic[table].ToInteger(tableID, subject);
    }

    public static float ToFloat(TableType table, int tableID, string subject)
    {
        if (!tableDic.ContainsKey(table))
            return -1;
        return tableDic[table].ToFloat(tableID, subject);
    }

    public static string ToString(TableType table, int tableID, string subject)
    {
        if (!tableDic.ContainsKey(table))
            return string.Empty;
        return tableDic[table].ToString(tableID, subject);
    }

    public static T ToEnum<T> (TableType table, int tableID, string subject) where T : struct
    {
        if (!tableDic.ContainsKey(table))
            return default(T);
        return tableDic[table].ToEnum<T>(tableID, subject);
    }

}
