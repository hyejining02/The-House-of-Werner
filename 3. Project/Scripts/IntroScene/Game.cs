using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Scene
{
    // ���� ���۵� �� ȣ��Ǵ� �Լ�
    public override void Enter()
    {
        UIManager.Instance.FadeIn(0.5f);
    }

    // ���� ����� �� ȣ��Ǵ� �Լ�
    public override void Exit()
    {

    }

    // ���� ����Ǵ� ������ ó���ϱ� ���� �Լ�
    public override void Progress(float progress)
    {

    }
}
