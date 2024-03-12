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
    // �κ��丮�� ��ġ�� ���� ����Ʈ 
    private List<InvenItem> invenItems = new List<InvenItem>();

    // ������ �����͸� ã�� �����ϱ� ���� ����
    private Dictionary<int, InvenItem> itemDic = new Dictionary<int, InvenItem>();


    public override void Initialize()
    {
        // �κ��丮�� �ʱ�ȭ�Ǵ� �������� ��ũ���� ���� �̹����� �ε��Ѵ�.
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
        // �������� ��ȸ�ϸ鼭 �� �������� ���̵��� ����.
        foreach (var item in invenItems)
            item.Clear();
    }
    public void SetItemList(List<ItemInfo> itemList, int characterJob)
    {
        // �󽽷����� ������ ����
        Clear();
        // ������ ����Ʈ�� �����ϱ� ���ؼ� ����
        itemDic.Clear();

        // �Ѱܹ��� ���������� �������� �������� ó��
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
        // ���� ī�װ��� ������ ���̶�� �Լ��� ����
        if (current == category) return;
           current = category;

        if (invenTabButtons == null)
            return;

        invenTabButtons.SetTab( category );
        Vector3 position = invenTabButtons.GetFocusPosition(current);
        SetFocusLinePos(position);
    }

}
