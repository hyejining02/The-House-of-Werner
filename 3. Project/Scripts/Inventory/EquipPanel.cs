using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 착용된 아이템의 정보만 보여지도록 설정하는 클래스

public class EquipPanel : UIBase
{
    private Dictionary<Category, InvenItem> itemDic = new Dictionary<Category, InvenItem>();

    public override void Initialize()
    {
        InvenItem[] items = GetComponentsInChildren<InvenItem>();
        for ( int i = 0; i < items.Length; ++i )
        {
            items[i].Initialize();
            if ( !itemDic.ContainsKey(items[i].Category) )
                itemDic.Add(items[i].Category, items[i]);
        }
    }
    public void SetClickEvent(System.Action<InvenItem> OnClick)
    {
        foreach( var item in itemDic.Values )
        {
            item.SetListener(OnClick);
        }
    }

    // 아이템을 착용시키는 함수 ( UI적인 요소 )
    public void EquipItem( ItemInfo info )
    {
        if ( itemDic.ContainsKey(info.category) )
        {
            itemDic[info.category].SetInfo(info);

            // 슬롯아이콘이 보여지지않도록 처리한다.
            itemDic[info.category].SetSlotActive(false);
        }
    }

    public void OffEquipItem( ItemInfo info )
    {
        if (itemDic.ContainsKey(info.category))
        {
            // 아이콘을 모두 꺼주도록 처리
            itemDic[info.category].Clear();

            // 정보를 갖고 있지 않도록 리셋
            itemDic[info.category].SetInfo(null);

            // 슬롯아이콘이 보여지지않도록 처리한다.
            itemDic[info.category].SetSlotActive(true);
        }
    }

    public void EquipItems(ItemInfo[] infoArray)
    {
        foreach ( var item in infoArray)
        {
            EquipItem(item);
        }
    }
}
