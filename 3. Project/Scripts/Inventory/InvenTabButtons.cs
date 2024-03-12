using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �κ� > �κ��ǹ�ư > �ǹ�ư
// InputEventHandler <- �Է¿� ���� ó���� �� Ŭ�������� ����
// �Է¿� ���� �κ��� �ܺο��� ó���� �� �ֵ��� �����Ͽ���.

public class InvenTabButtons : UIBase
{
    private List<TabButton> tabBtns = new List<TabButton>();
    private Vector3 focusPos = Vector3.zero;

    public Vector3 FocusPos => focusPos;

    public override void Initialize()
    {
        tabBtns.AddRange( GetComponentsInChildren<TabButton>(true) );
        foreach ( var tab in tabBtns )
            tab.Initialize();
    }

    public void SetListener( System.Action<TabButton> action )
    {
        for (int i = 0; i < tabBtns.Count; ++i)
            tabBtns[i].SetListener( action );
    }

    // ī�װ��� ������ �� ��ư�� ��Ŀ���� Ȱ��ȭ���ִ� �Լ�
    public void SetTab ( Category category )
    {
        for ( int i =0;  i < tabBtns.Count; ++i )
        {
            if (tabBtns[i].Category == category)
            {
                focusPos = tabBtns[i].FocusPos;
                tabBtns[i].SetFocus(true);
            }

            else
                tabBtns[i].SetFocus(false);

        }
    }

    // ī�װ��� ���� ��Ŀ�� ��ġ�� �����ϴ� �Լ�
    public Vector3 GetFocusPosition( Category category )
    {
        for (int i = 0; i < tabBtns.Count; ++i)
        {
            if (tabBtns[i].Category == category)
            {
                focusPos = tabBtns[i].FocusPos;
            }
        }
        return focusPos;
    }




}
