using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAnyKeyToJumpToGC : MonoBehaviour
{
    
    void Update()
    {
        if (Input.anyKeyDown)
        {
            gameObject.GetComponent<JumpToOtherScene>().Jump();
        }
    }
}
