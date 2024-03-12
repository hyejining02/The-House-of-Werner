using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// JsonUtility를 사용하여 클래스의 정보를 Json파일로 만들기 위해서는 [System.Serializable]를 사용해야함
// 저장소는 배열 또는 List를 사용해야 함 (Dictionary 안되고 List<List<int>>처럼 리스트내의 리스트 또한 지원되지 않는다.

[System.Serializable]
public class UserInfo
{
    // 마지막에 선택했던 캐릭터 아이디
    public int charUniqueID;

    // 캐릭터의 직업
    public int jobType = (int)JobBit.KNIGHT;

    // 갖고 있는 캐릭터 리스트
    public List<SaveCharacter>listOfChar = new List<SaveCharacter>();

    // 갖고 있는 아이템 리스트 
    public List<SaveInfo> listOfItems = new List<SaveInfo>();

    public SaveCharacter Current => Get(charUniqueID);

    public void AddCharacter( SaveCharacter character )
    {
        listOfChar.Add(character);
    }


    // 캐릭터 아이디를 찾아서 정보를 리턴하는 함수
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
        // 캐릭터가 없다면 -1을 리턴
        if (character == null) return -1;

        // 캐릭터가 갖고있는 카테고리의 아이템 아이디를 리턴한다.
        // 착용하고 있는 아이템이 없다면 0값을 리턴한다.
        return character.GetEquip(category);
    }

    // 현재 캐릭터가 카테고리에 착용중인 아이템을 넘겨주도록 처리하는 함수
    public int GetEquip( int category)
    {
        // 현재 선택한 캐릭터가 없다면 -1값을 리턴한다.
        if ( Current == null)
            return -1;

        return Current.GetEquip(category);
    }
}
