using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    
    public string RemakeSceneName;
    public Transform RemakePoint;
    public GameObject Cat;
    public GameObject Dog;
    protected override void Awake()
    {
       base.Awake();
        DontDestroyOnLoad(this);
    }
    public void Dead()
    {
        Debug.Log("死了");
        Cat.gameObject.SetActive(false);
        Dog.gameObject.SetActive(false);
   
        StartCoroutine(WaitToRemake());
    }
    //计时几秒后跳转至复活点
    IEnumerator WaitToRemake()
    {
        Debug.Log("进入了携程");
        yield return new WaitForSeconds(3f);
        if (SceneManager.GetActiveScene().name!=RemakeSceneName)
        {
        LoadSceneManager.Instance.LoadScene(RemakeSceneName);
         }
        
        Cat.GetComponent<Damageable>().IsAlive = true;
        Dog.GetComponent<Damageable>().IsAlive = true;

        Cat.gameObject.SetActive(true);
        Dog.gameObject.SetActive(true);
        Cat.transform.position = RemakePoint.position;
        Dog.transform.position= RemakePoint.position;
    }

   
}
