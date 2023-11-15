using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject Open;
    public GameObject Close;
    public string Target; 
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetKeyDown(KeyCode.R)&&Open.activeSelf)
        {
            Open.SetActive(false);
            Close.SetActive(true);
            GameObject.Find(Target).GetComponent<Animator>().SetBool("isOpen", true);
        }
        else if(Input.GetKeyDown(KeyCode.R)&&Close.activeSelf)
        {
            Open.SetActive(true);
            Close.SetActive(false);
            GameObject.Find(Target).GetComponent<Animator>().SetBool("isOpen", false);
        }
    }

}
