using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadeChange : MonoBehaviour
{
    Animation Shade;

    public void shadeChange()
    {
        Shade = GetComponent<Animation>();
        Shade.Play();
       
    }
}
