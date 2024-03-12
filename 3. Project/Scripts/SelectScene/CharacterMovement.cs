using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캐릭터 그룹을 이동시키기 위한 클래스
public class CharacterMovement : MonoBehaviour
{
    // 프로그램 구동시점의 위치값
    private Vector3 originPosition;

    private List<Vector3> movements = new List<Vector3>();

    private bool update = false;
    public AnimationCurve curve;

    // 이동이 끝났을 때 실행될 함수를 얻어오기 위한 델리게이트
    private System.Action completed;

    // 현재업데이트 중인지 체크하기 위한 속성값
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
                    // 이동이 완료된 이후 호출할 함수가 있다면 실행되도록 처리
                    completed?.Invoke();
                }

                yield return null;
            }
             
    }
}
