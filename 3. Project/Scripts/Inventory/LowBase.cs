using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class LowBase
{
    private Dictionary< int, Dictionary<string, string>> table = new Dictionary< int, Dictionary<string, string>>();

    public Dictionary<int, Dictionary<string, string>>.KeyCollection keys => table.Keys;
    public Dictionary<int, Dictionary<string, string>>.ValueCollection Values => table.Values;

    public void Load( string file)  // file�� �ε�� ���ڿ��� �Էµǰ� �ȴ�.
    {
        // ���๮�ڸ� �������� �� ������ �и��Ѵ�.
        string[] rows = file.Split("\n");

        // ������ �и����� �ʴ´ٸ� �߸��� ���� ���. �Լ��� ����
        if ( rows.Length ==0 ) return;

        // ���ڿ��� �����ִ� \r ���ڸ� �����Ѵ�.
        for (int i = 0; i < rows.Length; ++i)
            rows[i] = rows[i].Replace("\r", "");

        int count = 0;
        string lastRow = rows[rows.Length - 1];

        // ���ڿ��� ������ ���� �� ���ڿ��̶��("") ��ȸ���� �ʵ��� ī��Ʈ�� 1 �����Ѵ�.
        if (string.IsNullOrEmpty(lastRow))
            ++count;

        int rowLength = rows.Length - count;

        // ��ǥ(,)�� �������� ���ڿ��� �и��Ѵ�.
        string[] subjects = rows[0].Split(",");

        for ( int i = 1; i < rowLength; ++i )
        {
            // ��ǥ�� �������� ���ڿ��� �и�
            string[] columns = rows[i].Split(",");
            // ù��° ���� ���̺��� ���̵�. ���������� ����
            int.TryParse(columns[0], out int tableID);

            if ( ! table.ContainsKey(tableID))
                table.Add(tableID, new Dictionary<string, string>());

            // ���� ������ ���̺��� ���̵� ����ǵ��� ó��.
            for ( int c = 0; c < columns.Length; ++c)
            {
                table[tableID].Add(subjects[c], columns[c]);
            }
        }

    }
    public int ToInteger ( int tableID, string subject)
    {
        // ���̺� ���̵��� ��ϵǾ� ���� �ʴٸ� -1���� ����
        if ( !table.ContainsKey(tableID))
            return -1;

        // ���̺� ���̵��� ���� �����Ϳ� subject���ڿ��� ���ٸ� -1���� ����
        if (!table[tableID].ContainsKey(subject))
            return -1;

        int.TryParse(table[tableID][subject], out int returnValue);
        return returnValue;
    }
    public float ToFloat(int tableID, string subject)
    {
        // ���̺� ���̵��� ��ϵǾ� ���� �ʴٸ� -1���� ����
        if (!table.ContainsKey(tableID))
            return -1;

        // ���̺� ���̵��� ���� �����Ϳ� subject���ڿ��� ���ٸ� -1���� ����
        if (!table[tableID].ContainsKey(subject))
            return -1;

        float.TryParse(table[tableID][subject], out float returnValue);
        return returnValue;
    }
    public string ToString(int tableID, string subject)
    {
        // ���̺� ���̵��� ��ϵǾ� ���� �ʴٸ� -1���� ����
        if (!table.ContainsKey(tableID))
            return string.Empty;

        // ���̺� ���̵��� ���� �����Ϳ� subject���ڿ��� ���ٸ� -1���� ����
        if (!table[tableID].ContainsKey(subject))
            return string.Empty;

        return table[tableID][subject]; 
    }
    public T ToEnum<T> (int tableID, string subject) where T : struct
    {
        // ���̺� ���̵��� ��ϵǾ� ���� �ʴٸ� -1���� ����
        if (!table.ContainsKey(tableID))
            return default(T);

        // ���̺� ���̵��� ���� �����Ϳ� subject���ڿ��� ���ٸ� -1���� ����
        if (!table[tableID].ContainsKey(subject))
            return default(T);

        string enumValue = table[tableID][subject];
        System.Enum.TryParse<T>(enumValue, out T t);

        return t;
    }


}