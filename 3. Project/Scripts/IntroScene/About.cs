using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class About : Scene
{
    // 씬이 시작될 때 호출되는 함수
    public override void Enter()
    {
        UIManager.Instance.FadeIn(0.3f);
    }

    // 씬이 종료될 때 호출되는 함수
    public override void Exit()
    {

    }

    // 씬이 변경되는 과정을 처리하기 위한 함수
    public override void Progress(float progress)
    {

    }
}
