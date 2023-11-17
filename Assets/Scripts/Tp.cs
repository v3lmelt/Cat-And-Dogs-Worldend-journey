using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Tp : MonoBehaviour
{
    public string destination;
    public bool catReady;
    public bool dogReady;
    public GameObject arrow; //有一只动物靠近时显示向右前进的箭头
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Cat")
        {
            catReady = true;
            arrow.SetActive(true);
        }
        if(collision.gameObject.name == "Dog")
        {
            dogReady = true;
            arrow.SetActive(true);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
            catReady = false;
        if (collision.gameObject.name == "Dog")
        {
            dogReady = false;
        }
    }
    private void Update()
    {
        switch (catReady)
        {
            case false when !dogReady:
                arrow.SetActive(false);
                break;
            case true when dogReady:
                LoadSceneManager.Instance.LoadScene(destination);
                break;
        }
    }
}
