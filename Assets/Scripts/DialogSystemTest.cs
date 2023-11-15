using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystemTest : MonoBehaviour
{
    [Tooltip("字体速度")]
    public float textSpeed;
    
    [Tooltip("文本")]
    public TextAsset dialogText;
    private void Start()
    {
        DialogSystemNew.Instance.InitDialogSystem(dialogText, transform, textSpeed);
    }
}
