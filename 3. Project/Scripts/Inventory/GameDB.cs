using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class GameDB
{
    // ���� ����
    public static UserInfo userInfo = new UserInfo();

    public static Dictionary<int, ItemInfo> itemDic = new Dictionary<int, ItemInfo>();

    public static int uniqueCount = 0;

    public static int charUniqueCount = 0;

    public static ItemInfo AddItem( LowBase lowBase, int tableID, int uniqueID, int level, int grade, Rank rank, bool equip, int equipCharacter )
    {
        ItemInfo itemInfo = new ItemInfo();
        // �����͸� ã�Ƽ� �������ִ� �κ�.
        Category category = lowBase.ToEnum<Category>(tableID, "CATEGORY");
        BitCategory bitCategory = lowBase.ToEnum<BitCategory>(tableID, "CATEGORY");

        string icon = lowBase.ToString(tableID, "ICON");
        string name = lowBase.ToString(tableID, "NAME");
        string explain = lowBase.ToString(tableID, "EXPLAIN");

        // ������ �� �ִ� �������� ���ͼ� "|"������ ���ڿ��� �и�.
        string wearStr = lowBase.ToString(tableID, "WEAR");
        string[] jobArray = wearStr.Split("|");

        int wearType = 0;
        for ( int i = 0; i < jobArray.Length; ++i )
        {
            // ���ڿ��� �´� ��Ʈ���� ������Ų��.
            System.Enum.TryParse<JobBit>(jobArray[i], out JobBit jobBit);
            wearType += (int)jobBit;
        }

        itemInfo.tableID = tableID;
        itemInfo.uniqueID = uniqueID;
        itemInfo.name = name;
        itemInfo.explain = explain;
        itemInfo.rank = rank;
        itemInfo.grade = grade;
        itemInfo.bitCategory = bitCategory;
        itemInfo.category = category;
        itemInfo.wearType = wearType;
        itemInfo.level = level;
        itemInfo.sprite = Resources.Load<Sprite>("Images/ItemIcon/" + icon);
        // �̹����ε尡 ���� �ʾ��� �� �α׸� ���ܼ� ã�ƺ� �� �ֵ��� ó��.
        if (itemInfo.sprite == null) Debug.Log("�̹������ Ȯ�� : Images/ItemIcon/" + icon);
        itemInfo.equip = equip;
        itemInfo.equipCharacter = equipCharacter;
        return itemInfo;
    }

    public static ItemInfo AddItem(SaveInfo saveInfo, int uniqueID)
    {
        LowBase lowbase = DataManager.Get(TableType.Item);
        return AddItem(lowbase, saveInfo.uniqueID, uniqueID, saveInfo.level, saveInfo.grade, saveInfo.rank, saveInfo.equip, saveInfo.equipCharacter);
    }

    // ������ ���� �Է¹޾Ƽ� �Ϻ� ������ ó���ϱ� ���� �Լ�
    public static ItemInfo AddItem( int tableID, int uniqueID, int level)
    {
        LowBase lowbase = DataManager.Get(TableType.Item);
        int grade = lowbase.ToInteger(tableID, "GRADE");
        Rank rank = lowbase.ToEnum<Rank>(tableID, "RANK");
        return AddItem(lowbase, tableID, uniqueID, level, grade, rank, false, 0);
    }

    // �����ϰ� �������� ������ �����ϴ� �Լ�( �׽�Ʈ �뵵 )
    public static void CreateRandomItems()
    {
        int uniqueCount = 0;
        itemDic.Clear();
        for (int i = 0; i < 50; ++ i)
        {
            ++uniqueCount;
            // �����ϰ� ���̺� ���̵��� ���Ѵ�.
            int tableID = Random.Range(1, 101);
            int level = Random.Range(1, 31);
            itemDic.Add(uniqueCount, AddItem(tableID, uniqueCount, level));

        }
    }
    public static string GetPath ( string filename)
    {
        string filePath = Application.persistentDataPath;

        // ���� �������� ���°� ����� ���°� �ƴ϶� �����ͳ����� �������̶�� ������ ��θ� ����ϵ��� ó��.
        if (Application.isEditor)
            filePath = Application.dataPath;

        // PC, �ȵ���̵�, IOS
        // ���� ���μ����� �°� ��θ� �����Ѵ�.
        return Path.Combine(filePath, filename);
    }

    // 1. JsonUtility�� ����ϸ� Ŭ������ �����ϰ� �ִ� ���� json���ڿ� ���·� �ٲ� �� �ִ�.
    // 2. ���ڿ��� �ѹ��� ����ǵ��� ó��
    // path : ������ ���, content : json������ ������ ���ڿ�
    public static void WriteFile(string path, string content)
    {
        // ������ �����ϱ� ���� �����. ( ����ִ� ���� ����� ) 
        FileStream fileStream = new FileStream(path, FileMode.Create);

        // ���� ����� Ŭ������ ������� ����.
        // �����ϰ� ������� ����� ����ϸ� �ȴ�.
        using( StreamWriter writer = new StreamWriter(fileStream) )

            // ���ϳ��� �������� �����Ѵ�.
            writer.Write(content);
    }
    public static string ReadFile ( string path )
    {
        if (!File.Exists(path)) return string.Empty;
        FileStream fileStream = new FileStream(path, FileMode.Open);
        string readAllText = string.Empty;

        // ������ ó������ �������� ������ ���ڿ� ������ ���
        using ( StreamReader reader = new StreamReader(fileStream) )
            readAllText = reader. ReadToEnd();

        return readAllText;
    }

    public static void Load( string filename)
    {
        // �ؽ�Ʈ ����(json)�� �ε��ϴ� �ڵ�
        string json = ReadFile(GetPath(filename));

        // ���̽������� �����Ͽ� UserInfoŬ���� ������ �����Ѵ�.
        //1) Lit, SimpleJson => ���¶��̺귯��, C#
        userInfo = JsonUtility.FromJson<UserInfo>(json);

        uniqueCount = 1;
        
        foreach ( var item in userInfo.listOfItems )
        {                            // 2)
            itemDic.Add(uniqueCount, AddItem(item, uniqueCount));
            ++uniqueCount;
        }
    }

    // UserInfo�����͸� �����ϴ� �Լ�
    public static void Save ( string filename )
    {
        if (userInfo == null) return;
        // �������� ������  ����
        userInfo.listOfItems.Clear();

        // ����Ʈ�� Dictionary ����� �� ����Ʈ�� �Ѱ��ش�.
        userInfo.listOfItems.AddRange(itemDic.Values);

        // Ŭ������ ������ ���̽� ������ ���ڿ��� �����Ѵ�
        string json = JsonUtility.ToJson(userInfo, true);
        WriteFile(GetPath(filename), json);
    }

    // �� �Լ��� ���� �����͸� ����� ���� ���� �׽�Ʈ �Լ�
    public static void SaveSampleFile(string filename )
    {
        userInfo.listOfItems.Clear();

        userInfo.charUniqueID = 100;
        userInfo.listOfChar.Add(
            new SaveCharacter()
            {
                equip = true,
                grade = 3,
                level = 60,
                rank = Rank.SR,
                tableID = 1,
                uniqueID = 100,
                jobType = (int)JobBit.KNIGHT
            }
            );
        userInfo.listOfItems.Clear();
        userInfo.listOfItems.AddRange(itemDic.Values);
        string json  = JsonUtility.ToJson (userInfo , true);
        WriteFile(GetPath(filename), json);
    }


    // ����Ʈ�� ����� �������� ������ �����ϱ� ���� ���� �Լ�
    public static int Comparer(ItemInfo left, ItemInfo right)
    {
        // ���ı��� : 1. ī�װ�(�������� ���ĵǰ� ��ó�� ), 2. ��ũ, 3. ����, 4. ����
        if (left.bitCategory > right.bitCategory) return 1;
        else if (left.bitCategory < right.bitCategory) return -1;
        else
        {
            int leftValue = (int)left.rank * 100000 + left.grade * 10000 + left.level;
            int rightValue = (int)right.rank *100000 + right.grade * 10000 + right.level;

            if ( leftValue < rightValue ) return 1;
            else if ( leftValue > rightValue ) return -1;
            return 0;
        }
    }

    // ĳ���͸� ã��, ĳ���Ͱ� �����ϰ� �ִ� �������� ������ �������ִ� �Լ�
    // �κ��丮 ���� �гο��� �����ϰ� �ִ� ������ ������ �޾Ƽ� �����ϱ� ���� ���Ǿ� ����.
    public static List<ItemInfo> GetEquipItems(int uniqueID)
    {
        List<ItemInfo> itemList = new List<ItemInfo>();

        // ĳ������ ������ ã�´�.
        SaveCharacter character = userInfo.Get(uniqueID);

        if (character == null) return itemList;

        // �迭�� ��ȸ�ϸ鼭 �������� itemDic�� ��ϵ� �������̶�� itemList�� ������ �� �Ѱ��ֵ��� �Ѵ�.
        foreach ( var itemID in character.equipItemArray )
        {
            if ( itemDic.ContainsKey(itemID) )
            {
                itemList.Add( itemDic[itemID] );
            }
        }
        return itemList;
    }

    // ���� ĳ���Ͱ� �����ϰ� �ִ� �������� �����ϴ� �Լ�
    public static List<ItemInfo> CurrentCharacterItems()
    {
        return GetEquipItems( userInfo.charUniqueID );
    }

    // �ĺ� ���̵𰪿� �´� ������ ������ �����ϴ� �Լ�
    public static ItemInfo GetItem( int uniqueID)
    {
        if ( itemDic.ContainsKey (uniqueID) )
            return itemDic[uniqueID];
        return null;
    }

    // ī�װ��� �´� ������ ���� ����Ʈ�� �����ϴ� �Լ�
    // ��Ʈ �����ڸ� ���ؼ� ��ó�� �� ����
    // BitCategory = ( WEAPON | ARMOR )
    public static List<ItemInfo> GetItems(int category)
    {
        List<ItemInfo> itemList = new List<ItemInfo>();
        foreach ( var item in itemDic.Values )
        {
            int state = category & (int)item.bitCategory;
            if (state == (int)item.bitCategory)
                itemList.Add(item);
        }
        itemList.Sort( Comparer );
        return itemList;
    }

    public static void AddCharacter(int tableID)
    {
        SaveCharacter character = new SaveCharacter();
        character.tableID = tableID;
        character.uniqueID = ++charUniqueCount;
        userInfo.AddCharacter(character);
    }

    
}
