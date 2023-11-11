using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpToBackgroundStory : MonoBehaviour
{
    public void Jump()
    {
      LoadSceneManager.Instance.LoadScene("Background Story");
        
    }
}
