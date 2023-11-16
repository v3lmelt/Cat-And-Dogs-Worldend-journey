using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tp : MonoBehaviour
{
    public string Distnation;
    public  bool CatReady;
    public bool DogReady;
    public GameObject Arrow; //有一只动物靠近时显示向右前进的箭头
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Cat")
        {
            CatReady = true;
            Arrow.SetActive(true);
        }
        if(collision.gameObject.name == "Dog")
        {
            DogReady = true;
            Arrow.SetActive(true);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        
            CatReady = false;
       
        if (collision.gameObject.name == "Dog")
        {
            DogReady = false;
        }
    }
    private void Update()
    {
        if (!CatReady && !DogReady)
        {
            Arrow.SetActive(false);
        }
        if (CatReady && DogReady)
        {
            LoadSceneManager.Instance.LoadScene(Distnation);
        }
    }
}
