using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameGroup : UIBase
{
    private Image icon;
    private new TMP_Text name;
    private CanvasGroup group;

    private bool update = false;
    [SerializeField]

    private float speed = 1;
    private float elapsed = 0;

    private float start = 0;
    private float end = 0;


    public override void Initialize()
    {
        group = GetComponent<CanvasGroup>();
        group.alpha = 0;
        icon = transform.Find("Icon").GetComponent<Image>();
        name = transform.Find("Name").GetComponent<TMP_Text>();
    }

    public void CanvasGroupAlpha(float alpha)
    {
        group.alpha = alpha;
    }

    // ĳ������ ��ġ�� �޾Ƽ� ��ũ����ǥ�� �����ϰ� �� ��ũ����ǥ������ ����
    public void SetCharPosition(Vector3 characterPosition)
    {
        // ����Ƽ��ǥ�踦 �޾Ƽ� UI��ǥ��� �����ϴ� �Լ�
        transform.position = Camera.main.WorldToScreenPoint(characterPosition);
    }

    public void SetInfo(Sprite sprite, string name)
    {
        icon.sprite = sprite;
        //icon.SetNativeSize(); //�����̹����� ��µǵ��� ó��
        this.name.text = name;
    }

    public void Hide()
    {
        update = true;
        elapsed = 0;
        start = 1;
        end = 0;
    }

    public void Show()
    {
        update = true;
        elapsed = 0;
        start = 0;
        end = 1;
    }

    void Update()
    {
        if (!update) return;

        elapsed += Time.deltaTime / speed;
        group.alpha = Mathf.Lerp(start, end, elapsed);

        if ( group.alpha >= 1)
        {
            update = false; 

        }


        
    }
}
