using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 인벤 > 인벤탭버튼 > 탭버튼
// InputEventHandler <- 입력에 대한 처리를 이 클래스에서 진행
// 입력에 대한 부분은 외부에서 처리될 수 있도록 구현하였다.

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

    // 카테고리와 동일한 탭 버튼의 포커스를 활성화해주는 함수
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

    // 카테고리에 대한 포커스 위치를 리턴하는 함수
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
