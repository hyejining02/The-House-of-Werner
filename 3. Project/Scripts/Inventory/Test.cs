using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 랜덤하게 아이텀의 정보를 생성
// 저장하고 로드
public class Test : MonoBehaviour
{
    void InventorySetting()
    {
        Equipment equipment = GameObject.FindFirstObjectByType<Equipment>();
        equipment.Initialize();
        List <ItemInfo> items = GameDB.GetItems((int)BitCategory.WEAPON);
        // 아이템리스트 설정
        equipment.SetItemList(items,GameDB.userInfo.jobType);
    }

    void Start()
    {
        DataManager.Load(TableType.Item);

        GameDB.Load("userInfo.txt");
        print( DataManager.ToInteger(TableType.Item, 1,"HP"));
        Invoke("InventorySetting", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.K) ) 
        {
            // 아이템에 대한 정보를 랜덤하게 설정.
            // 아이템에 대한 정보를 저장.
            GameDB.CreateRandomItems();
            GameDB.SaveSampleFile("userInfo.txt");
        }

        if ( Input.GetKeyDown(KeyCode.L) )
        {
            GameDB.Load("userInfo.txt");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            // 아이템에 대한 현재 정보를 저장.
        }

    }
}
