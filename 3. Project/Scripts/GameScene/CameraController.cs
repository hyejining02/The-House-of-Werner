using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum CameraState
    {
        Freelook,
        Targeting,
        Blending,
    }

    public Transform target;

    private Camera gameCamera;

    private Vector3 offset;

    private Vector3 forward;

    public float defaultDistance = 0;
    public float currentDistance = 0;

    // 마우스를 수평과 수직으로 이동한 누적값.
    private float CurrentVertical = 0;
    private float CurrentHorizontal = 0;

    public float minVerticalAngle = -90;
    public float maxVerticalAngle = 90;

    public float minDistance = 0;
    public float maxDistance = 10;

    [SerializeField]
    private float zoomDistance = 0;

    public float zoomSpeed = 10;
    public float sphereRadius = 0.5f;

    public LayerMask objectLayer;

    public ITarget enemy;

    public float cameraDamping = 3;

    public Quaternion startRotation;
    public Quaternion targetRotation;

    public float blendTime = 0.2f;
    public float elapsed = 0;

    // 카메라의 상태 변수
    public CameraState current;
    public CameraState next;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        gameCamera = GetComponent<Camera>();

        offset = target.position - transform.position;

        currentDistance = Vector3.Distance(target.position - offset, transform.position);

        Quaternion rotation = Quaternion.FromToRotation(transform.forward, Vector3.forward);

        offset = rotation * offset;

        forward = transform.forward;
        transform.position = CalculatePositionOfCamera(transform.rotation);

    }

    Vector3 CalculatePositionOfCamera( Quaternion rotation )
    {
        Vector3 lookAt = rotation * Vector3.forward;

        Vector3 f = transform.TransformDirection(offset);

        return (target.position - f) - lookAt * currentDistance;
    }

    public Quaternion CalculateRotationMovedByTheXAxis()
    {
        CurrentHorizontal += Input.GetAxis("Mouse X");
        return Quaternion.Euler(0, CurrentHorizontal, 0);
    }

    public Quaternion CalculateRotationMovedByTheYAxis()
    {
        float rotation = CurrentVertical + Input.GetAxis("Mouse Y");
        CurrentVertical = Mathf.Clamp(rotation, minVerticalAngle, maxVerticalAngle);

        return Quaternion.Euler(CurrentVertical, 0, 0);
    }

    public Quaternion RotationValueOfTheCamera()
    {
        Quaternion xRotation = CalculateRotationMovedByTheXAxis();
        Quaternion yRotation = CalculateRotationMovedByTheYAxis();
        Vector3 looAt = xRotation * forward;
        Quaternion lookRotation = Quaternion.LookRotation(looAt) * yRotation;
        return lookRotation;
    }

    public void Freelook()
    {
        transform.rotation = RotationValueOfTheCamera();

        zoomDistance = Mathf.Clamp(zoomDistance - Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, minDistance, maxDistance);

        currentDistance = Mathf.Lerp(currentDistance, zoomDistance, zoomSpeed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, target.position);


        RaycastHit[] hits = Physics.SphereCastAll(target.position, sphereRadius, transform.rotation * -Vector3.forward, zoomDistance, objectLayer);

        for (int i = 0; i < hits.Length; ++i)
        {
            if ( currentDistance > hits[i].distance)
            {
                currentDistance = hits[i].distance;
                //zoomDistance = currentDistance;
            }
        }

        transform.position = CalculatePositionOfCamera(transform.rotation);

        startRotation = transform.rotation;
    }

    public void Targeting()
    {
        if (enemy == null)
            return;

        Vector3 direction = (enemy.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, cameraDamping * Time.deltaTime);

        transform.position = CalculatePositionOfCamera(transform.rotation);

        startRotation = lookRotation;
    }

    public void SetBlendMode( CameraState state )
    {
        current = CameraState.Blending;

        next = state;

        if ( state == CameraState.Targeting )
        {
            Vector3 direction = (enemy.transform.position - transform.position).normalized;
            targetRotation = Quaternion.LookRotation(direction);
        }
        else if( state == CameraState.Freelook )
        {
            targetRotation = RotationValueOfTheCamera();
        }

    }

    public void Blending()
    {
        elapsed += Time.deltaTime / blendTime;

        transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsed);

        Vector3 targetPosition = CalculatePositionOfCamera(transform.rotation);

        transform.position = Vector3.Lerp(transform.position, targetPosition, elapsed);

        if ( elapsed >= 1.0f)
        {
            elapsed = 0;
            current = next;
        }

    }

    void LateUpdate()
    {
        switch ( current )
        {
            case CameraState.Freelook:
                Freelook();
                break;
            case CameraState.Targeting:
                Targeting();
                break;
            case CameraState.Blending:
                Blending();
                break;

        }
            
    }
}
