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

    // 캐릭터의 위치를 받아서 스크린좌표로 변경하고 그 스크린좌표값으로 설정
    public void SetCharPosition(Vector3 characterPosition)
    {
        // 유니티좌표계를 받아서 UI좌표계로 변경하는 함수
        transform.position = Camera.main.WorldToScreenPoint(characterPosition);
    }

    public void SetInfo(Sprite sprite, string name)
    {
        icon.sprite = sprite;
        //icon.SetNativeSize(); //원본이미지로 출력되도록 처리
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
