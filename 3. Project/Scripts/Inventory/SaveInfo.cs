using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Category
{
    WEAPON,
    ARMOR,
    BOOTS,
    HELMET,
    ACCESSORY,
    ALL,
    NONE,
}

// 값의 영역이 중복되지 않도록 처리하기 위함. ( csv 아이템 파일에서 비트값을 사용하고 있다 )
public enum BitCategory
{
    WEAPON = 1,
    ARMOR = 2,
    BOOTS = 4,
    HELMET = 8,
    ACCESSORY = 16,
    ALL = WEAPON | ARMOR | BOOTS | HELMET | ACCESSORY
}

public enum Rank
{
    N,
    R,
    SR,
    SSR,
}

public enum Job
{
    NONE,
    KNIGHT,
    WARRIOR,
    ASSASSIN,
    PRIEST,
    ARCHER,
    WIZARD,
}

public enum JobBit
{
    NONE = 0,
    KNIGHT = 1,
    WARRIOR = 2,
    ASSASSIN = 4,
    PRIEST = 8,
    ARCHER = 16,
    WIZARD = 32,
    ALL = KNIGHT | WARRIOR | ASSASSIN | PRIEST | ARCHER | WIZARD
}

public enum AttackInfo
{
    BRONZE,
    SILVER,
    GOLD,
    PLATINUM,
    DIAMOND,
}
public enum AttackBit
{
    Hwany = 0,
    Sangoh = 1,
    Jinny = 2,
    Angella = 4,
    Sunny = 9,
    ALL = Hwany | Sangoh | Jinny | Angella | Sunny
}

// 아이템의 저장 정보
[System.Serializable]
public class SaveInfo
{
    // 아이템을 구별하기 위한 고유한 식별 값
    public int uniqueID;

    // 데이터테이블에서의 고유한 식별 값
    public int tableID;

    // 아이템의 등급 ( 강화 )
    public int grade;

    // 아이템의 레벨
    public int level;

    // 아이템의 랭크
    public Rank rank;

    // 아이템의 착용 여부
    public bool equip = false;

    // 착용하고 있는 캐릭터의 아이디
    public int equipCharacter;
}

[System.Serializable]
public class SaveCharacter : SaveInfo
{
    public int jobType;
    public int[] equipItemArray = new int[5] { 0, 0, 0, 0, 0 };

    // 착용하고 있는 아이디를 넘겨주는 함수

    // category값은 enum Category의 값을 가리킨다.
    public int GetEquip(int category) => equipItemArray[category];

    public void SetWearing( int category, int itemUniqueID )
    {
        equipItemArray[category] = itemUniqueID;
    }

    // 카테고리에 해당하는 아이템을 착용하고 있는지 체크해보는 함수
    public bool WearingTheEquipment( int category)
    {
        return equipItemArray[category] != 0;

    }

}

[System.Serializable]

public class ItemInfo : SaveInfo
{
    // 아이템의 카테고리
    public Category category;

    // 연산에 사용할 변수
    public BitCategory bitCategory;

    // 아이템의 이미지
    public Sprite sprite;

    // 아이템의 설명글
    public string explain;

    // 아이템의 이름
    public string name;

    // 아이템의 착용 타입
    public int wearType;
}
