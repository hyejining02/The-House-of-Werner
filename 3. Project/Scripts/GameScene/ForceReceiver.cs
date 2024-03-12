using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 힘을 받아서 전달하는 역할을 하는 클래스입니다.
public class ForceReceiver : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 impact;
    [SerializeField]
    private float smoothTime = 0.1f;
    private Vector3 dampingVelocity;

    public Vector3 Impact => impact;

    // Start is called before the first frame update
    public void Initialize()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void AddForce(Vector3 direction, float power)
    {
        impact += direction * power;
    }

    void Update()
    {
        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, smoothTime);
    }
}
