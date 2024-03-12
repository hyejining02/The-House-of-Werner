using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentText : MonoBehaviour
{
    private Logo logo;
    void Start()
    {
        Logo logo = this.InstantiateAndCall<Logo>("Logo","Func2", new object[] {10});

        print ( logo );
    
    }

    void Update()
    {
        
    }
}
