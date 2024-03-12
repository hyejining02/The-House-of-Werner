using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static SaveCharacter;

public class InvenItem : UIBase
{
    private TMP_Text level;

    // ���������
    private Image icon;

    // ������ ��ũ �̹���
    private Image rank;

    // ������ ����ų �̹���
    private List<Transform> stars = new List<Transform>();

    // ��Ŀ�� �̹���
    private GameObject focus;

    // �׸��� �̹��� ( ���밡�� ���� )
    private GameObject shadow;

    // ���⿡ �´� ���� ������
    private GameObject slotIcon;

    // �⺻���� null���� ������ ����
    private static List<Sprite> rankImages;

    private Button button;

    // �� �̹������� ��Ʈ ( �θ� )
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

    // ���̹����� ã�Ƽ� ����
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
        // �ʱ�ȭ�� �Ҷ� �׷��� ���ش�.
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

    // �ܺο��� ȣ��� �Լ�
    public void SetListener ( System.Action<InvenItem> action )
    {
        if (button == null) return;
        // ��ư�� Ŭ���Ǿ��� �� ����Ǳ� ���� �Լ��� ����
        button.onClick.AddListener( ()=> { action(this); });
    }

    public void SetShadowActive( bool active) => shadow.SetActive(active);
    public void SetFocusActive(bool active) => focus.SetActive(active);

    // ������ �޾Ƽ� ui�� �ð����� �κ��� �����ϴ� �Լ�
    public void SetInfo( ItemInfo info )
    {
        if ( info == null) return;

        itemInfo = info;

        if ( focus != null )
        { // �������� �������̶�� ���� ǥ�� �̹����� �����ش�.
            if ( info.equip )
                focus.SetActive(true);
            else
                focus.SetActive(false);
        }

        // ��ũ�̹����� �����ϴ� �ڵ�
        if ( rank != null )
        {
            rank.sprite = rankImages[(int)info.rank];
            rank.gameObject.SetActive(true);
        }

        // ���� ã�� ���� ���� �̹����� �����ϴ� �ڵ�
        starRoot.SetActive(true);
        for ( int i = stars.Count -1; i >=0; --i)
        {
            // ���� �������� ����� ���ؼ� ���� �������� ���� ����̸� ���� ������ �ʵ��� ó��.
            if ( (info.grade - 1) < i )
                stars[i].gameObject.SetActive(false);
            else
                stars[i].gameObject.SetActive(true);
        }

        // �������� ������ ����
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

        // �������� ������ Ȯ������ �� ĳ���� �������� ������ �� ���� �������̶��
        // ��Ӱ� ǥ�õǴ� �̹����� ���ִ� �ڵ�
        if ((info.wearType & characterJob) != characterJob)
            if ( shadow != null) shadow.SetActive(true);

        // ������ �� �ִ� �������̶�� �������̹���(�������� �����ִ� ������ �̹��� ) �� ���ش�.
        else
             if (shadow != null) shadow.SetActive(false);    
    }

    // �ܺο��� ȣ�� �� �Լ�
    public void Clear()
    {
        // �� �������� ó���ϱ� ���� �Լ�
        SetGroupActive(false);
    }

}
