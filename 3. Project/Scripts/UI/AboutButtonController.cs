using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AboutButtonController : MonoBehaviour
{
    public Button aboutbutton;

    void Start()
    {
        aboutbutton!.onClick.AddListener(OnAboutButtonClick);
    }

    public void OnAboutButtonClick()
    {
        SceneManager.Instance.EnableDelay(0.7f, SceneType.Title);
    }
}
