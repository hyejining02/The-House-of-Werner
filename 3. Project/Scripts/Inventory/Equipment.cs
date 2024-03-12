using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UI 매니저가 없기 때문에 Test클래스에서 Equipment의 Initialize를 호출
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

        // 현재 선택한 캐릭터가 있다면 캐릭터가 착용하고 있는 아이템리스트를 받는다.
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
