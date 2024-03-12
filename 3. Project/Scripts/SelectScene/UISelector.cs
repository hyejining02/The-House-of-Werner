using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISelector : MonoBehaviour
{
    private IconGroup prefab;
    private VerticalLayoutGroup layoutGroup;
    private List<IconGroup> iconList = new List<IconGroup>();
    private Button leftButton;
    private Button rightButton;

    private Button selectButton;

    private TMP_Text number;

    // 캐릭터의 직업이미지를 출력하기 위한 데이터
    private Image jobIcon;

    private NameGroup nameGroup;

    // 캐릭터의 위치값, 이름(공격력), 아이콘(검)
    public void SetNameGroup(Vector3 position, string name, Sprite sprite)
    {
        nameGroup.SetCharPosition(position);
        nameGroup.SetInfo(sprite, name);
    }

    public void ShowNameGroup() => nameGroup.Show();
    public void HideNameGroup() => nameGroup.Hide();

    public void CanvasGroupAlpha(float alpha) => nameGroup.CanvasGroupAlpha(alpha);
 
    public void Initialize()
    {
        prefab = Resources.Load<IconGroup>("RenderTexturePrefabs/IconGroup");
        layoutGroup = GetComponentInChildren<VerticalLayoutGroup>();
        nameGroup = GetComponentInChildren<NameGroup>();
        nameGroup.Initialize();

        number = transform.Find("ArrowGroup/Number").GetComponent<TMP_Text>();
        leftButton = transform.Find("ArrowGroup/Left").GetComponent<Button>();
        rightButton = transform.Find("ArrowGroup/Right").GetComponent<Button>();
        selectButton = transform.Find("Select").GetComponent<Button>();

    }

    public void AddIcon(int tableID, int slotIndex, string name, RenderTexture texture)
    {
        if (prefab == null) return;

        IconGroup newIcon = Instantiate(prefab, layoutGroup.transform);
        newIcon.Initialize();
        newIcon.SetName(name);
        newIcon.SetTableID(tableID);
        newIcon.SetSlotIndex(slotIndex);
        newIcon.SetRenderTexture(texture);

        iconList.Add(newIcon);
    }

    // 숫자의 범위는 1부터 출력되도록 처리
    public void SetNumberText( int current , int maximum )
    {
        number.text = string.Format($"{current+1} / {maximum}");
    }

    // 캐릭터를 모두 생성하고 호출할 함수
    public void SetClickEvent(System.Action<IconGroup> action)
    {
        foreach ( var icon in iconList)
        {
            icon.SetClickEvent(action);
        }
    }

    public void SetSelectClickEvent( System.Action action )
    {
        selectButton.onClick.AddListener( ()=> { action(); });


    }

    public void SetArrowClickEvent(System.Action leftAction,  System.Action rightAction)
    {
        leftButton.onClick.AddListener(() => { leftAction(); });
        rightButton.onClick.AddListener(() => { rightAction(); });
    }

    public void SetFocusActive( int index )
    {
        for ( int i = 0; i < iconList.Count; ++i )
        {
            if ( index == i )
            {
                iconList[i].SetFocusActive(true);
            }
            else
            {
                iconList[i].SetFocusActive(false);
            }
        }
    }
}
