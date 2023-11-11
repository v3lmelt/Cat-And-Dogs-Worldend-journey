using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpDoorInGS : MonoBehaviour
{
    public string Distnation;
    public GameObject Icon;
    
    bool interectable;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
        Icon.SetActive(true);
        interectable = true;
        }
        

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
        Icon.SetActive(false);
        interectable = false;
        }
        
    }
    private void Update()
    {
        if (interectable&&Input.GetKeyDown(KeyCode.R))
        {
            LoadSceneManager.Instance.LoadScene(Distnation);
        }
    }
}
