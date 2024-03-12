using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// JsonUtility�� ����Ͽ� Ŭ������ ������ Json���Ϸ� ����� ���ؼ��� [System.Serializable]�� ����ؾ���
// ����Ҵ� �迭 �Ǵ� List�� ����ؾ� �� (Dictionary �ȵǰ� List<List<int>>ó�� ����Ʈ���� ����Ʈ ���� �������� �ʴ´�.

[System.Serializable]
public class UserInfo
{
    // �������� �����ߴ� ĳ���� ���̵�
    public int charUniqueID;

    // ĳ������ ����
    public int jobType = (int)JobBit.KNIGHT;

    // ���� �ִ� ĳ���� ����Ʈ
    public List<SaveCharacter>listOfChar = new List<SaveCharacter>();

    // ���� �ִ� ������ ����Ʈ 
    public List<SaveInfo> listOfItems = new List<SaveInfo>();

    public SaveCharacter Current => Get(charUniqueID);

    public void AddCharacter( SaveCharacter character )
    {
        listOfChar.Add(character);
    }


    // ĳ���� ���̵� ã�Ƽ� ������ �����ϴ� �Լ�
    public SaveCharacter Get ( int characterID )
    {
        foreach ( var character in listOfChar )
        {
            if ( character.uniqueID == characterID )
                return character;
        }
        return null;
    }

    public int GetIDOfItem( int characterID, int category )
    {
        SaveCharacter character = Get(characterID);
        // ĳ���Ͱ� ���ٸ� -1�� ����
        if (character == null) return -1;

        // ĳ���Ͱ� �����ִ� ī�װ��� ������ ���̵� �����Ѵ�.
        // �����ϰ� �ִ� �������� ���ٸ� 0���� �����Ѵ�.
        return character.GetEquip(category);
    }

    // ���� ĳ���Ͱ� ī�װ��� �������� �������� �Ѱ��ֵ��� ó���ϴ� �Լ�
    public int GetEquip( int category)
    {
        // ���� ������ ĳ���Ͱ� ���ٸ� -1���� �����Ѵ�.
        if ( Current == null)
            return -1;

        return Current.GetEquip(category);
    }
}
