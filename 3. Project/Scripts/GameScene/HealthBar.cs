using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;

    private void Update()
    {
        //체력바 임시코드
        if (Input.GetKey(KeyCode.H))
            healthBar.value -= 0.1f;

    }
}
