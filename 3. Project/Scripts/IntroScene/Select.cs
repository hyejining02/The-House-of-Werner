using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select : Scene
{
    void Start()
    {
        SceneManager.Instance.EnableDelay(3.2f, SceneType.Game);
        UIManager.Instance.FadeIn(0.5f);
    }
    public override void Enter()
    {
   
    }

    public override void Exit()
    {
        
    }

    public override void Progress(float progress)
    {

    }
}
