using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ĳ���� �׷��� �̵���Ű�� ���� Ŭ����
public class CharacterMovement : MonoBehaviour
{
    // ���α׷� ���������� ��ġ��
    private Vector3 originPosition;

    private List<Vector3> movements = new List<Vector3>();

    private bool update = false;
    public AnimationCurve curve;

    // �̵��� ������ �� ����� �Լ��� ������ ���� ��������Ʈ
    private System.Action completed;

    // ���������Ʈ ������ üũ�ϱ� ���� �Ӽ���
    public bool IsUpdate => update;

    public void SetCompleted( System.Action completed)
    {
        this.completed = completed; 
    }
    public void Initialize(int characterNumber, float distance)
    {
        originPosition = transform.position;
        Vector3 position = originPosition;

        for ( int i = 0; i < characterNumber; ++i)
        {
            movements.Add(position);
            //print(position);
            position += new Vector3(0, 0, -distance);
        }
    }

    public void Move(int current, int next, float speed, bool graph = false)
    {
        if (update)
            return;

        StartCoroutine(IEMove(current, next, speed, graph));
    }

    private IEnumerator IEMove ( int current, int next, float speed, bool graph)
    {
        update = true;
        float elapsed = 0;
             
            while (update)
            {
                elapsed += Time.deltaTime / speed;
                elapsed = Mathf.Clamp01(elapsed);
                float graphValue = curve.Evaluate(elapsed);
                if (!graph)
                    graphValue = elapsed;
                transform.position = Vector3.Lerp(movements[current], movements[next], graphValue);

                if (elapsed >= 1.0f)
                {
                    update = false;
                    // �̵��� �Ϸ�� ���� ȣ���� �Լ��� �ִٸ� ����ǵ��� ó��
                    completed?.Invoke();
                }

                yield return null;
            }
             
    }
}
