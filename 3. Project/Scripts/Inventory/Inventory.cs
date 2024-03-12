using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Equipment <= Inventory

public class Inventory : UIBase
{
    private InvenTabButtons invenTabButtons;
    private Transform focusline;
    private Category current;
    private InvenItem invenItemPrefab;
    private ScrollRect scrollRect;
    // 인벤토리에 배치된 슬롯 리스트 
    private List<InvenItem> invenItems = new List<InvenItem>();

    // 빠르게 데이터를 찾고 갱신하기 위한 변수
    private Dictionary<int, InvenItem> itemDic = new Dictionary<int, InvenItem>();


    public override void Initialize()
    {
        // 인벤토리가 초기화되는 시점에서 랭크에서 사용될 이미지를 로드한다.
        InvenItem.LoadRankImages();

        focusline = transform.Find("Background/FocusLine");
        invenTabButtons = GetComponentInChildren<InvenTabButtons>();
        scrollRect = GetComponentInChildren<ScrollRect>();

        invenItemPrefab = Resources.Load<InvenItem>("InvenItem");

        if ( invenTabButtons != null )
        {
            invenTabButtons.Initialize();
            Vector3 position = invenTabButtons.GetFocusPosition(Category.WEAPON);
            SetFocusLinePos(position);
            focusline.gameObject.SetActive(true);
        }

        CreateEmptySlots(54);
    }

    public void CreateEmptySlots(int count)
    {
        if (invenItemPrefab == null) return;

        for ( int i = 0; i < count; ++i )
        {
            InvenItem item = Instantiate(invenItemPrefab, scrollRect.content);
            item.Initialize();
            invenItems.Add(item);
        }
    }
    public void Clear()
    {
        // 아이템을 순회하면서 빈 슬롯으로 보이도록 설정.
        foreach (var item in invenItems)
            item.Clear();
    }
    public void SetItemList(List<ItemInfo> itemList, int characterJob)
    {
        // 빈슬롯으로 정보를 구성
        Clear();
        // 데이터 리스트를 갱신하기 위해서 삭제
        itemDic.Clear();

        // 넘겨받은 정보값으로 아이템을 보여지게 처리
        for ( int i =0; i < itemList.Count; ++i )
        {
            invenItems[i].SetInfo(itemList[i], characterJob);
            itemDic.Add(itemList[i].uniqueID, invenItems[i]);
        }
    }

    void SetFocusLinePos( Vector3 position ) => focusline.position = position;

    public void SetTabClickEvent( System.Action<TabButton> onClick ) => invenTabButtons.SetListener( onClick );
    public void SetTab( Category category )
    {
        // 현재 카테고리와 동일한 값이라면 함수를 종료
        if (current == category) return;
           current = category;

        if (invenTabButtons == null)
            return;

        invenTabButtons.SetTab( category );
        Vector3 position = invenTabButtons.GetFocusPosition(current);
        SetFocusLinePos(position);
    }

}
