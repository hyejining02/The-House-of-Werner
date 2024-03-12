using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����ϰ� �������� ������ ����
// �����ϰ� �ε�
public class Test : MonoBehaviour
{
    void InventorySetting()
    {
        Equipment equipment = GameObject.FindFirstObjectByType<Equipment>();
        equipment.Initialize();
        List <ItemInfo> items = GameDB.GetItems((int)BitCategory.WEAPON);
        // �����۸���Ʈ ����
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
            // �����ۿ� ���� ������ �����ϰ� ����.
            // �����ۿ� ���� ������ ����.
            GameDB.CreateRandomItems();
            GameDB.SaveSampleFile("userInfo.txt");
        }

        if ( Input.GetKeyDown(KeyCode.L) )
        {
            GameDB.Load("userInfo.txt");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            // �����ۿ� ���� ���� ������ ����.
        }

    }
}
