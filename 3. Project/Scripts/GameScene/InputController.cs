using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public CharacterBase player;
    public CameraController mainCamera;

    public void Initialize()
    {
        mainCamera = GameObject.FindFirstObjectByType<CameraController>();
        GameObject t = GameObject.FindGameObjectWithTag("Player");
        if ( t != null)
            player = t.GetComponent<CharacterBase>();
        
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (player != null)
            player.FreelookInput(horizontal, vertical);

        if ( Input.GetKeyDown(KeyCode.Tab))
        {

        }
        if ( Input.GetKeyDown(KeyCode.LeftShift) )
        {

        }
    }
}
