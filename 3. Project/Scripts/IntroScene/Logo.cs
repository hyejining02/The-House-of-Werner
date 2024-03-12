using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour
{
    void Start()
    {

        SceneManager.Instance.AddScene<Title>(SceneType.Title);
        SceneManager.Instance.AddScene<Select>(SceneType.Select);
        SceneManager.Instance.AddScene<Game>(SceneType.Game);
        SceneManager.Instance.AddScene<About>(SceneType.About);

        //Resources.Load<GameObject>("Prefabs/LogoName");
        //Resources.Load<GameObject>("UI/UIFade");


        SceneManager.Instance.EnableDelay(3.2f, SceneType.Title);
        UIManager.Instance.FadeOutDelay(2, 1);
        
    }
}
