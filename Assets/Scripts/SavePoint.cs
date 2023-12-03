using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.remakePoint = new Vector3(0, 3, 0);
        GameManager.Instance.remakeSceneName = SceneManager.GetActiveScene().name;
        
        
        TextManager.Instance.OnCreatingStatusText(GameManager.Instance.cat.transform.position, "Checkpoint!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
