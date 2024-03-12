using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Scene : MonoBehaviour
{
    // ���� ���۵� �� ȣ��Ǵ� �Լ�
    public abstract void Enter();

    // ���� ����� �� ȣ��Ǵ� �Լ�
    public abstract void Exit();

    // ���� ����Ǵ� ������ ó���ϱ� ���� �Լ�
    public abstract void Progress(float progress);

}
