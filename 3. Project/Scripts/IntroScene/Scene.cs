using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Scene : MonoBehaviour
{
    // 씬이 시작될 때 호출되는 함수
    public abstract void Enter();

    // 씬이 종료될 때 호출되는 함수
    public abstract void Exit();

    // 씬이 변경되는 과정을 처리하기 위한 함수
    public abstract void Progress(float progress);

}
