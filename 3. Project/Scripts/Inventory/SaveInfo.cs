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

// ���� ������ �ߺ����� �ʵ��� ó���ϱ� ����. ( csv ������ ���Ͽ��� ��Ʈ���� ����ϰ� �ִ� )
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

// �������� ���� ����
[System.Serializable]
public class SaveInfo
{
    // �������� �����ϱ� ���� ������ �ĺ� ��
    public int uniqueID;

    // ���������̺����� ������ �ĺ� ��
    public int tableID;

    // �������� ��� ( ��ȭ )
    public int grade;

    // �������� ����
    public int level;

    // �������� ��ũ
    public Rank rank;

    // �������� ���� ����
    public bool equip = false;

    // �����ϰ� �ִ� ĳ������ ���̵�
    public int equipCharacter;
}

[System.Serializable]
public class SaveCharacter : SaveInfo
{
    public int jobType;
    public int[] equipItemArray = new int[5] { 0, 0, 0, 0, 0 };

    // �����ϰ� �ִ� ���̵� �Ѱ��ִ� �Լ�

    // category���� enum Category�� ���� ����Ų��.
    public int GetEquip(int category) => equipItemArray[category];

    public void SetWearing( int category, int itemUniqueID )
    {
        equipItemArray[category] = itemUniqueID;
    }

    // ī�װ��� �ش��ϴ� �������� �����ϰ� �ִ��� üũ�غ��� �Լ�
    public bool WearingTheEquipment( int category)
    {
        return equipItemArray[category] != 0;

    }

}

[System.Serializable]

public class ItemInfo : SaveInfo
{
    // �������� ī�װ�
    public Category category;

    // ���꿡 ����� ����
    public BitCategory bitCategory;

    // �������� �̹���
    public Sprite sprite;

    // �������� �����
    public string explain;

    // �������� �̸�
    public string name;

    // �������� ���� Ÿ��
    public int wearType;
}
