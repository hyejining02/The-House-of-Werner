using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����� �������� ������ ���������� �����ϴ� Ŭ����

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

    // �������� �����Ű�� �Լ� ( UI���� ��� )
    public void EquipItem( ItemInfo info )
    {
        if ( itemDic.ContainsKey(info.category) )
        {
            itemDic[info.category].SetInfo(info);

            // ���Ծ������� ���������ʵ��� ó���Ѵ�.
            itemDic[info.category].SetSlotActive(false);
        }
    }

    public void OffEquipItem( ItemInfo info )
    {
        if (itemDic.ContainsKey(info.category))
        {
            // �������� ��� ���ֵ��� ó��
            itemDic[info.category].Clear();

            // ������ ���� ���� �ʵ��� ����
            itemDic[info.category].SetInfo(null);

            // ���Ծ������� ���������ʵ��� ó���Ѵ�.
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
