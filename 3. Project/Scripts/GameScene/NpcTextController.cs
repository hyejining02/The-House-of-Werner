using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcTextController : MonoBehaviour
{
    
    void Start()
    {
        if ( Input.GetKeyDown(KeyCode.H))
        {
            gameObject.SetActive(false);
        }
    }

  
}
