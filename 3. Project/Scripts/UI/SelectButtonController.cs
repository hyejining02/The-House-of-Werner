using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButtonController : MonoBehaviour
{
    public Button selectbutton;

    void Start()
    {
        selectbutton!.onClick.AddListener(OnSelectButtonClick);
    }

    public void OnSelectButtonClick()
    {
        SceneManager.Instance.EnableDelay(1.8f, SceneType.Game);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
