using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonController : MonoBehaviour
{
    public Button startbutton;
    public Button exitbutton;
    public Button aboutbutton;

    void Start()
    {
        startbutton!.onClick.AddListener(OnStartButtonClick);
        exitbutton!.onClick.AddListener(OnExitButtonClick);
        aboutbutton!.onClick.AddListener(OnAboutButtonClick);
    }

    public void OnStartButtonClick()
    {
        SceneManager.Instance.EnableDelay(0.7f,SceneType.Select);
    }

    public void OnExitButtonClick()
    {
        SceneManager.Instance.EnableDelay(0.7f, SceneType.Logo);
    }
    public void OnAboutButtonClick()
    {
        SceneManager.Instance.EnableDelay(0.7f, SceneType.About);
    }


}
