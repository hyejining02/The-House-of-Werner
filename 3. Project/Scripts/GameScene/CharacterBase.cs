using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public enum State
{
    Freelook,
    Targeting,
    Attack,
    Patrol,
    Chase,
    Doudt,
    Wait,
}

public class ITarget : MonoBehaviour
{

}

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(ForceReceiver))]
//[RequireComponent(typeof(NavMeshAgent))]

public class CharacterBase : ITarget
{
    public CharacterController controller;
    public Animator animator;
    public ForceReceiver forceReceiver;

    public readonly string forwardAnimation = "Forward";
    public readonly string rightAnimation = "Right";

    public readonly string freelookBlendTree = "Freelook";
    public readonly string targetingBlendTree = "Targeting";
    public readonly string dodgeBlendTree = "Dodge";

    public float fixedTransitionDuration = 0.1f;

    public Vector3 gravity = new Vector3(0, -9.81f, 0);

    public State current = State.Freelook;
    public State prev;

    public float freelookspeed = 5;
    public float targetingspeed = 4;

    public float freelookAniDamping = 0.1f;
    public float targetingAniDamping = 0.1f;

    public float rotateDamping = 10;

    public bool npcArea = false;
    private GameObject currentUI;

    public NPCLookAt npc;


    public void FootR()
    {

    }

    public void FootL()
    {

    }

    public void Initialize()
    {
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        forceReceiver = GetComponent<ForceReceiver>();
    }

    public void Move(Vector3 direction, float speed)
    {
        // 주어진 방향으로 이동 및 중력처리
        controller?.Move(direction * speed * Time.deltaTime + gravity * Time.deltaTime);
    }

    private Vector3 RemoveYAxis( Vector3 vector )
    {
        vector.y = 0;
        return vector;
    }

    public Vector3 CameraDirection ( float x, float z )
    {
        Camera camera = Camera.main;
        if (camera == null) return Vector3.zero;

        Vector3 forward = RemoveYAxis(camera.transform.forward);
        Vector3 right = RemoveYAxis(camera.transform.right);

        return (forward * z + right * x).normalized;
    }

    public void LookingInTheDirectionTiMove( Vector3 direction)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotateDamping * Time.deltaTime);
    }

    public void SetFloat ( string name, float value, float damping, float deltaTime )
    {
        animator?.SetFloat(name, value, damping, deltaTime);
    }

    public void FreelookInput ( float x, float z)
    {
        Vector3 direction = CameraDirection(x, z);
        Move(direction, freelookspeed);
        float animationValue = 0;
        if ( x != 0 || z != 0)
        {
            LookingInTheDirectionTiMove (direction);
            animationValue = 1;
        }
        SetFloat(forwardAnimation, animationValue, freelookAniDamping, Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if( other.CompareTag("Npc") )
        {
            npcArea = true;
            npc = other.GetComponent<NPCLookAt>();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Npc"))
        {
            npcArea = false;
            npc = null;
        }
    }

    void OpenUI()
    {
        switch (npc.npcType)
        {
            case NPCType.Shop:
                break;

            case NPCType.Quest:
                GameObject npcText = Resources.Load<GameObject>("UI/NpcText01");
                //UIManager.Instance.Add<UIBase>(npcText,false);
                currentUI = Instantiate(npcText);
                break;
        }
    }

    void CloseUI()
    {
        if (currentUI != null)
        {
            Destroy(currentUI);
            currentUI = null;
        }
    }

    void Update()
    {
        if( npcArea )
        {
            if( Input.GetKeyDown(KeyCode.G) )
            {
                OpenUI();
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                CloseUI();
            }
        }

    }
}
