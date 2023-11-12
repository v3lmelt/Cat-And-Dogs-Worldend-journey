using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpToOtherScene : MonoBehaviour
{
    public string Dinstination;
    public void Jump()
    {
      LoadSceneManager.Instance.LoadScene(Dinstination);
        
    }
}
