using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Title : Scene
{
    void Start()
    {
        SceneManager.Instance.EnableDelay(3.2f, SceneType.Select);
        UIManager.Instance.FadeIn(1);
    }

    public override void Enter()
    {
        UIManager.Instance.FadeIn(0.3f);
    }

    public override void Exit()
    {

    }

    public override void Progress(float progress)
    {

    }


}
