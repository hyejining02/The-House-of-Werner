using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobIcon : UIBase
{
    public Image background;
    public Image icon;
    public Transform frame;
    public override void Initialize()
    {
        background = GetComponent<Image>();
        Transform t = transform.Find("Icon");
        if (t != null) icon = t.GetComponent<Image>();
        frame = transform.Find("Frame");
    }
    public void SetIconActive(bool active) => icon.gameObject.SetActive(active);
    public void SetFrameActive(bool active) => frame.gameObject.SetActive(active);
    public void SetIcon(Sprite sprite) => icon.sprite = sprite;


}
