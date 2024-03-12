using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class GameDB
{
    // 유저 정보
    public static UserInfo userInfo = new UserInfo();

    public static Dictionary<int, ItemInfo> itemDic = new Dictionary<int, ItemInfo>();

    public static int uniqueCount = 0;

    public static int charUniqueCount = 0;

    public static ItemInfo AddItem( LowBase lowBase, int tableID, int uniqueID, int level, int grade, Rank rank, bool equip, int equipCharacter )
    {
        ItemInfo itemInfo = new ItemInfo();
        // 데이터를 찾아서 설정해주는 부분.
        Category category = lowBase.ToEnum<Category>(tableID, "CATEGORY");
        BitCategory bitCategory = lowBase.ToEnum<BitCategory>(tableID, "CATEGORY");

        string icon = lowBase.ToString(tableID, "ICON");
        string name = lowBase.ToString(tableID, "NAME");
        string explain = lowBase.ToString(tableID, "EXPLAIN");

        // 착용할 수 있는 직업군을 얻어와서 "|"단위로 문자열을 분리.
        string wearStr = lowBase.ToString(tableID, "WEAR");
        string[] jobArray = wearStr.Split("|");

        int wearType = 0;
        for ( int i = 0; i < jobArray.Length; ++i )
        {
            // 문자열에 맞는 비트값을 누적시킨다.
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
        // 이미지로드가 되지 않았을 때 로그를 남겨서 찾아볼 수 있도록 처리.
        if (itemInfo.sprite == null) Debug.Log("이미지경로 확인 : Images/ItemIcon/" + icon);
        itemInfo.equip = equip;
        itemInfo.equipCharacter = equipCharacter;
        return itemInfo;
    }

    public static ItemInfo AddItem(SaveInfo saveInfo, int uniqueID)
    {
        LowBase lowbase = DataManager.Get(TableType.Item);
        return AddItem(lowbase, saveInfo.uniqueID, uniqueID, saveInfo.level, saveInfo.grade, saveInfo.rank, saveInfo.equip, saveInfo.equipCharacter);
    }

    // 가상의 값을 입력받아서 일부 내용을 처리하기 위한 함수
    public static ItemInfo AddItem( int tableID, int uniqueID, int level)
    {
        LowBase lowbase = DataManager.Get(TableType.Item);
        int grade = lowbase.ToInteger(tableID, "GRADE");
        Rank rank = lowbase.ToEnum<Rank>(tableID, "RANK");
        return AddItem(lowbase, tableID, uniqueID, level, grade, rank, false, 0);
    }

    // 랜덤하게 아이템의 정보를 생성하는 함수( 테스트 용도 )
    public static void CreateRandomItems()
    {
        int uniqueCount = 0;
        itemDic.Clear();
        for (int i = 0; i < 50; ++ i)
        {
            ++uniqueCount;
            // 랜덤하게 테이블 아이디값을 구한다.
            int tableID = Random.Range(1, 101);
            int level = Random.Range(1, 31);
            itemDic.Add(uniqueCount, AddItem(tableID, uniqueCount, level));

        }
    }
    public static string GetPath ( string filename)
    {
        string filePath = Application.persistentDataPath;

        // 현재 실행중인 상태가 빌드된 상태가 아니라 에디터내에서 실행중이라면 에디터 경로를 사용하도록 처리.
        if (Application.isEditor)
            filePath = Application.dataPath;

        // PC, 안드로이드, IOS
        // 현재 프로세스에 맞게 경로를 결합한다.
        return Path.Combine(filePath, filename);
    }

    // 1. JsonUtility를 사용하면 클래스가 저장하고 있는 값을 json문자열 형태로 바꿀 수 있다.
    // 2. 문자열을 한번에 저장되도록 처리
    // path : 저장할 경로, content : json형태의 저장할 문자열
    public static void WriteFile(string path, string content)
    {
        // 파일을 저장하기 모드로 만든다. ( 비어있는 파일 만들기 ) 
        FileStream fileStream = new FileStream(path, FileMode.Create);

        // 파일 입출력 클래스의 사용방식은 많다.
        // 이해하고 방식으로 몇가지만 사용하면 된다.
        using( StreamWriter writer = new StreamWriter(fileStream) )

            // 파일내에 콘텐츠를 저장한다.
            writer.Write(content);
    }
    public static string ReadFile ( string path )
    {
        if (!File.Exists(path)) return string.Empty;
        FileStream fileStream = new FileStream(path, FileMode.Open);
        string readAllText = string.Empty;

        // 파일의 처음부터 끝까지의 내용을 문자열 값으로 계산
        using ( StreamReader reader = new StreamReader(fileStream) )
            readAllText = reader. ReadToEnd();

        return readAllText;
    }

    public static void Load( string filename)
    {
        // 텍스트 파일(json)을 로드하는 코드
        string json = ReadFile(GetPath(filename));

        // 제이슨파일을 참고하여 UserInfo클래스 정보를 구성한다.
        //1) Lit, SimpleJson => 오픈라이브러리, C#
        userInfo = JsonUtility.FromJson<UserInfo>(json);

        uniqueCount = 1;
        
        foreach ( var item in userInfo.listOfItems )
        {                            // 2)
            itemDic.Add(uniqueCount, AddItem(item, uniqueCount));
            ++uniqueCount;
        }
    }

    // UserInfo데이터를 저장하는 함수
    public static void Save ( string filename )
    {
        if (userInfo == null) return;
        // 아이템의 정보를  삭제
        userInfo.listOfItems.Clear();

        // 리스트에 Dictionary 저장된 값 리스트를 넘겨준다.
        userInfo.listOfItems.AddRange(itemDic.Values);

        // 클래스의 파일을 제이슨 형식의 문자열로 변경한다
        string json = JsonUtility.ToJson(userInfo, true);
        WriteFile(GetPath(filename), json);
    }

    // 이 함수는 샘플 데이터를 만들어 내기 위한 테스트 함수
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


    // 리스트에 저장된 아이템의 정보를 정렬하기 위한 비교자 함수
    public static int Comparer(ItemInfo left, ItemInfo right)
    {
        // 정렬기준 : 1. 카테고리(오름차순 정렬되게 비교처리 ), 2. 랭크, 3. 성급, 4. 레벨
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

    // 캐릭터를 찾고, 캐릭터가 착용하고 있는 아이템의 정보를 리턴해주는 함수
    // 인벤토리 왼쪽 패널에서 착용하고 있는 아이템 정보를 받아서 설정하기 위해 사용되어 진다.
    public static List<ItemInfo> GetEquipItems(int uniqueID)
    {
        List<ItemInfo> itemList = new List<ItemInfo>();

        // 캐릭터의 정보를 찾는다.
        SaveCharacter character = userInfo.Get(uniqueID);

        if (character == null) return itemList;

        // 배열을 순회하면서 아이템이 itemDic에 등록된 아이템이라면 itemList에 저장한 수 넘겨주도록 한다.
        foreach ( var itemID in character.equipItemArray )
        {
            if ( itemDic.ContainsKey(itemID) )
            {
                itemList.Add( itemDic[itemID] );
            }
        }
        return itemList;
    }

    // 현재 캐릭터가 착용하고 있는 아이템을 리턴하는 함수
    public static List<ItemInfo> CurrentCharacterItems()
    {
        return GetEquipItems( userInfo.charUniqueID );
    }

    // 식별 아이디값에 맞는 아이템 정보를 리턴하는 함수
    public static ItemInfo GetItem( int uniqueID)
    {
        if ( itemDic.ContainsKey (uniqueID) )
            return itemDic[uniqueID];
        return null;
    }

    // 카테고리에 맞는 아이템 정보 리스트를 리턴하는 함수
    // 비트 연산자를 통해서 비교처리 할 예정
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
