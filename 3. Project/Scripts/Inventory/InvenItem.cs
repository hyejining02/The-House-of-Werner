using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static SaveCharacter;

public class InvenItem : UIBase
{
    private TMP_Text level;

    // 무기아이콘
    private Image icon;

    // 아이템 랭크 이미지
    private Image rank;

    // 성급을 가리킬 이미지
    private List<Transform> stars = new List<Transform>();

    // 포커스 이미지
    private GameObject focus;

    // 그림자 이미지 ( 착용가능 여부 )
    private GameObject shadow;

    // 무기에 맞는 슬롯 아이콘
    private GameObject slotIcon;

    // 기본값은 null값을 갖도록 설정
    private static List<Sprite> rankImages;

    private Button button;

    // 별 이미지들의 루트 ( 부모 )
    private GameObject starRoot;

    private ItemInfo itemInfo;

    [SerializeField]

    private Category category;
    public ItemInfo ItemInfo => itemInfo;
    public Category Category => category;

    public static void LoadRankImages()
    {
        if (rankImages != null) return;

        rankImages = new List<Sprite>();
        rankImages.Add(Resources.Load<Sprite>("Images/Rank/equipment_grade_blue"));
        rankImages.Add(Resources.Load<Sprite>("Images/Rank/equipment_grade_purple"));
        rankImages.Add(Resources.Load<Sprite>("Images/Rank/equipment_grade_red"));
        rankImages.Add(Resources.Load<Sprite>("Images/Rank/equipment_grade_gold"));
    }

    // 별이미지를 찾아서 저장
    private void FindStarImages()
    {
        Transform t = transform.Find("Stars");
        if ( t != null)
        {
            starRoot = t.gameObject;
            for (int i = 0; i < t.childCount; ++i)
                stars.Add(t.GetChild(i));
        }
    }

    
    public override void Initialize()
    {
        FindStarImages();

        icon = transform.Find("Icon").GetComponent<Image>();
        level = transform.Find("Level").GetComponent<TMP_Text>();
        rank = transform.Find("Rank").GetComponent<Image>();
        focus = transform.Find("Focus").gameObject;

        Transform t = transform.Find("Shadow");
        if ( t != null)
            shadow = gameObject;

        t = transform.Find("SlotIcon");
        if (t != null)
            slotIcon = gameObject;

        button = GetComponent<Button>();
        // 초기화를 할때 그룹을 꺼준다.
        SetGroupActive(false);
    }

    public void SetGroupActive( bool active)
    {
        icon.gameObject.SetActive(active);
        level.gameObject.SetActive(active);
        rank.gameObject.SetActive(active);
        focus.SetActive(active);

        if( shadow != null )
            shadow.SetActive(active);

        if( slotIcon != null )
            slotIcon.SetActive(active);

        starRoot.SetActive(active);
    }

    public void SetSlotActive( bool active )
    {
        slotIcon.SetActive(active);
    }

    // 외부에서 호출될 함수
    public void SetListener ( System.Action<InvenItem> action )
    {
        if (button == null) return;
        // 버튼이 클릭되었을 때 실행되기 위한 함수를 연결
        button.onClick.AddListener( ()=> { action(this); });
    }

    public void SetShadowActive( bool active) => shadow.SetActive(active);
    public void SetFocusActive(bool active) => focus.SetActive(active);

    // 정보를 받아서 ui의 시각적인 부분을 설정하는 함수
    public void SetInfo( ItemInfo info )
    {
        if ( info == null) return;

        itemInfo = info;

        if ( focus != null )
        { // 착용중인 아이템이라면 착용 표시 이미지를 보여준다.
            if ( info.equip )
                focus.SetActive(true);
            else
                focus.SetActive(false);
        }

        // 랭크이미지를 설정하는 코드
        if ( rank != null )
        {
            rank.sprite = rankImages[(int)info.rank];
            rank.gameObject.SetActive(true);
        }

        // 별을 찾고 별에 대한 이미지를 셋팅하는 코드
        starRoot.SetActive(true);
        for ( int i = stars.Count -1; i >=0; --i)
        {
            // 현재 아이템의 등급을 비교해서 별의 개수보다 작은 등급이면 별이 보이지 않도록 처리.
            if ( (info.grade - 1) < i )
                stars[i].gameObject.SetActive(false);
            else
                stars[i].gameObject.SetActive(true);
        }

        // 아이템의 레벨을 설정
        if ( level != null )
        {
            level.text = info.level.ToString();
            level.gameObject.SetActive(true);
        }
        if ( icon != null )
        {
            icon.gameObject.SetActive(true);
            icon.sprite = info.sprite;
        }

    }

    public void SetInfo(ItemInfo info, int characterJob)
    {
        if ( info == null) return;
        SetInfo( info );

        // 아이템의 정보를 확인했을 때 캐릭터 직업군이 착용할 수 없는 아이템이라면
        // 어둡게 표시되는 이미지를 켜주는 코드
        if ((info.wearType & characterJob) != characterJob)
            if ( shadow != null) shadow.SetActive(true);

        // 착용할 수 있는 아이템이라면 쉐도우이미지(아이템을 덮고있는 검은색 이미지 ) 를 꺼준다.
        else
             if (shadow != null) shadow.SetActive(false);    
    }

    // 외부에서 호출 될 함수
    public void Clear()
    {
        // 빈 슬롯으로 처리하기 위한 함수
        SetGroupActive(false);
    }

}
