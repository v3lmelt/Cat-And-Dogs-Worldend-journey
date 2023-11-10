using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpToGameplayScene : MonoBehaviour
{
   public void Jump()
    {
        LoadSceneManager.Instance.LoadScene("GameplayScene");
    }
}
