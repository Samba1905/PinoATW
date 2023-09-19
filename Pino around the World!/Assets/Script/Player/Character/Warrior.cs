using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    public bool isShielding;
    public static bool vulnerable;

    private void Update()
    {
        if(Input.GetButton("Attack2")) isShielding = true;
        else isShielding = false;
        if (isShielding) vulnerable = false;
        else vulnerable = true;
    }
}
