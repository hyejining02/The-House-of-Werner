using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;

    private void Update()
    {
        //ü�¹� �ӽ��ڵ�
        if (Input.GetKey(KeyCode.H))
            healthBar.value -= 0.1f;

    }
}
