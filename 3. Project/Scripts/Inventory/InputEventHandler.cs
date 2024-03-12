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
        // ī�װ��� �´� �������� ���������� ó���ؾ� �Ѵ�.

        inventory.SetTab(tab.Category);

        System.Enum.TryParse<BitCategory>(tab.Category.ToString(), out BitCategory bitCategory);

        // ��Ʈ���� �´� ������ ����Ʈ�� �߷��� �޴´�.
        List<ItemInfo> itemInfo = GameDB.GetItems((int)bitCategory);

        // ���� �����Ͱ����� �κ��丮�� ����
        inventory.SetItemList(itemInfo, GameDB.userInfo.jobType);
    }


    void Update()
    {
        if ( EventSystem.current != null )
        {
            if ( Input.anyKeyDown )
            {
                // ���� ������ UI�� ���� �� ( ���콺Ű, Ű����Ű)�� �Է��ߴٸ� �˾�â�� �ݵ��� ó���ϴ� �ڵ�
                if ( EventSystem.current.currentSelectedGameObject == null ) 
                {
                    /// �˾�â�� �ݵ��� ó��
                }
            }
        }
       
    }
}
