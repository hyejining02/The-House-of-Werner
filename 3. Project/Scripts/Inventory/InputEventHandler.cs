using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputEventHandler : MonoBehaviour
{
    private Inventory inventory;
    public void Initialize()
    {
        inventory = GetComponentInChildren<Inventory>();
        if ( inventory != null )
        {
            inventory.SetTabClickEvent(OnTabClicked);
        }
    }

    void OnTabClicked( TabButton tab )
    {
        // 카테고리에 맞는 아이템이 보여지도록 처리해야 한다.

        inventory.SetTab(tab.Category);

        System.Enum.TryParse<BitCategory>(tab.Category.ToString(), out BitCategory bitCategory);

        // 비트값에 맞는 아이템 리스트를 추려서 받는다.
        List<ItemInfo> itemInfo = GameDB.GetItems((int)bitCategory);

        // 받은 데이터값으로 인벤토리를 갱신
        inventory.SetItemList(itemInfo, GameDB.userInfo.jobType);
    }


    void Update()
    {
        if ( EventSystem.current != null )
        {
            if ( Input.anyKeyDown )
            {
                // 현재 선택한 UI가 없을 때 ( 마우스키, 키보드키)를 입력했다면 팝업창을 닫도록 처리하는 코드
                if ( EventSystem.current.currentSelectedGameObject == null ) 
                {
                    /// 팝업창을 닫도록 처리
                }
            }
        }
       
    }
}
