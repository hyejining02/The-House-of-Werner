using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class About : Scene
{
    // ���� ���۵� �� ȣ��Ǵ� �Լ�
    public override void Enter()
    {
        UIManager.Instance.FadeIn(0.3f);
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
