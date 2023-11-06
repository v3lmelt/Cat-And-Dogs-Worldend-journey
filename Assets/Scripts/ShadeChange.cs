using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadeChange : MonoBehaviour
{
    Animation _shade;

    public void shadeChange()
    {
        _shade = GetComponent<Animation>();
        _shade.Play();
    }
}
