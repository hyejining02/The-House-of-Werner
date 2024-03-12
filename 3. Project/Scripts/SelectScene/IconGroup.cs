using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IconGroup : MonoBehaviour
{
    private TMP_Text Name;
    private Image Focus;
    private RawImage RawImage;
    private int slotIndex;
    private int tableID;

    private Button button;

    public int SlotIndex => slotIndex;
    public int TableID => tableID;
    public void Initialize()
    {
        Name = GetComponentInChildren<TMP_Text>();
        Focus = transform.Find("Focus").GetComponent<Image>();
        RawImage = GetComponentInChildren<RawImage>();
        button = GetComponent<Button>();

        Focus.gameObject.SetActive(false);
    }

    public void SetRenderTexture(RenderTexture renderTexture)
    {
        RawImage.texture = renderTexture;
    }
    public void SetFocusActive(bool active) => Focus.gameObject.SetActive(active);
    public void SetName(string name) => Name.text = name;

    public void SetSlotIndex(int index) => slotIndex = index;
    public void SetTableID(int tableId)=> tableID = tableId;


    public void SetClickEvent( System.Action<IconGroup> action )
    {
        
        button.onClick.AddListener( () => action(this) );
        
    }

}
