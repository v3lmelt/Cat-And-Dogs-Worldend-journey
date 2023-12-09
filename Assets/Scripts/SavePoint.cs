using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour
{
    public Vector3 remakePoint;
    void Start()
    {
        GameManager.Instance.remakePoint = remakePoint;
        GameManager.Instance.remakeSceneName = SceneManager.GetActiveScene().name;
        
        
        TextManager.Instance.OnCreatingStatusText(GameManager.Instance.cat.transform.position, "Checkpoint!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
