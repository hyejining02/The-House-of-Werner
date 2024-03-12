using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class LowBase
{
    private Dictionary< int, Dictionary<string, string>> table = new Dictionary< int, Dictionary<string, string>>();

    public Dictionary<int, Dictionary<string, string>>.KeyCollection keys => table.Keys;
    public Dictionary<int, Dictionary<string, string>>.ValueCollection Values => table.Values;

    public void Load( string file)  // file은 로드된 문자열이 입력되게 된다.
    {
        // 개행문자를 기준으로 행 단위로 분리한다.
        string[] rows = file.Split("\n");

        // 행으로 분리되지 않는다면 잘못된 파일 양식. 함수를 종료
        if ( rows.Length ==0 ) return;

        // 문자열의 끝에있는 \r 문자를 삭제한다.
        for (int i = 0; i < rows.Length; ++i)
            rows[i] = rows[i].Replace("\r", "");

        int count = 0;
        string lastRow = rows[rows.Length - 1];

        // 문자열의 마지막 행이 빈 문자열이라면("") 순회하지 않도록 카운트를 1 감소한다.
        if (string.IsNullOrEmpty(lastRow))
            ++count;

        int rowLength = rows.Length - count;

        // 쉼표(,)를 기준으로 문자열을 분리한다.
        string[] subjects = rows[0].Split(",");

        for ( int i = 1; i < rowLength; ++i )
        {
            // 쉼표를 기준으로 문자열을 분리
            string[] columns = rows[i].Split(",");
            // 첫번째 값은 테이블의 아이디값. 정수값으로 변경
            int.TryParse(columns[0], out int tableID);

            if ( ! table.ContainsKey(tableID))
                table.Add(tableID, new Dictionary<string, string>());

            // 값의 범위에 테이블의 아이디도 저장되도록 처리.
            for ( int c = 0; c < columns.Length; ++c)
            {
                table[tableID].Add(subjects[c], columns[c]);
            }
        }

    }
    public int ToInteger ( int tableID, string subject)
    {
        // 테이블 아이디값이 등록되어 있지 않다면 -1값을 리턴
        if ( !table.ContainsKey(tableID))
            return -1;

        // 테이블 아이디값을 갖는 데이터에 subject문자열이 없다면 -1값을 리턴
        if (!table[tableID].ContainsKey(subject))
            return -1;

        int.TryParse(table[tableID][subject], out int returnValue);
        return returnValue;
    }
    public float ToFloat(int tableID, string subject)
    {
        // 테이블 아이디값이 등록되어 있지 않다면 -1값을 리턴
        if (!table.ContainsKey(tableID))
            return -1;

        // 테이블 아이디값을 갖는 데이터에 subject문자열이 없다면 -1값을 리턴
        if (!table[tableID].ContainsKey(subject))
            return -1;

        float.TryParse(table[tableID][subject], out float returnValue);
        return returnValue;
    }
    public string ToString(int tableID, string subject)
    {
        // 테이블 아이디값이 등록되어 있지 않다면 -1값을 리턴
        if (!table.ContainsKey(tableID))
            return string.Empty;

        // 테이블 아이디값을 갖는 데이터에 subject문자열이 없다면 -1값을 리턴
        if (!table[tableID].ContainsKey(subject))
            return string.Empty;

        return table[tableID][subject]; 
    }
    public T ToEnum<T> (int tableID, string subject) where T : struct
    {
        // 테이블 아이디값이 등록되어 있지 않다면 -1값을 리턴
        if (!table.ContainsKey(tableID))
            return default(T);

        // 테이블 아이디값을 갖는 데이터에 subject문자열이 없다면 -1값을 리턴
        if (!table[tableID].ContainsKey(subject))
            return default(T);

        string enumValue = table[tableID][subject];
        System.Enum.TryParse<T>(enumValue, out T t);

        return t;
    }


}