using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UI �Ŵ����� ���� ������ TestŬ�������� Equipment�� Initialize�� ȣ��
public class Equipment : UIBase
{
    private Inventory inventory;
    private InputEventHandler eventHandler;
    private EquipPanel equipPanel;

    public override void Initialize()
    {
        inventory = GetComponentInChildren<Inventory>();
        inventory.Initialize();

        eventHandler = GetComponent<InputEventHandler>();
        eventHandler.Initialize();

        equipPanel = GetComponentInChildren<EquipPanel>();
        equipPanel.Initialize();

        // ���� ������ ĳ���Ͱ� �ִٸ� ĳ���Ͱ� �����ϰ� �ִ� �����۸���Ʈ�� �޴´�.
        List<ItemInfo> currentItems = GameDB.CurrentCharacterItems();
        equipPanel.EquipItems(currentItems.ToArray());

    }

    public void SetItemList( List<ItemInfo> itemList, int jobType)
    {
        inventory.SetItemList(itemList, jobType);
    }


    void Update()
    {
        
    }
}
