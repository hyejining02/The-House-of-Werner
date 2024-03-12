using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 입력이 발생되었을 때 핸들링되는 역할만 수행

public class TabButton : UIBase
{
    private Transform normal;
    private Transform highlight;
    private Transform focus;
    private Button button;

    [SerializeField]
    private Category category;
    
    public Category Category  => category;
    public Vector3 FocusPos => focus.position;
     
    public override void Initialize()
    {
        normal = transform.Find("Normal");
        highlight = transform.Find("Highlight");
        focus = transform.Find("Focus");
        print(focus.position);
        button = GetComponent<Button>();
    }

    public void SetFocus( bool state ) => highlight.gameObject.SetActive( state );
    public void SetListener( System.Action<TabButton> action) => button.onClick.AddListener( () => { action(this); });

}
