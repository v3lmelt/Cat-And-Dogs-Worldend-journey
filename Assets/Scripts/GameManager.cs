using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    
    public string RemakeSceneName;
    public Vector3 RemakePoint;
    
    public GameObject Cat;
    
    public GameObject Dog;

    private void Start()
    {
        DontDestroyOnLoad(this);
        FindCatAndDog();
    }
    public void FindCatAndDog()
    {
        Cat = GameObject.Find("Cat");
        Dog = GameObject.Find("Dog");
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
         yield return new WaitForSeconds(0.001f); //Wait期间会自动调用Updata函数 这里直接写Instance.FindCatAndDog()是找不到猫狗的 
            Cat.transform.position = RemakePoint;
            Dog.transform.position = RemakePoint;
         }
        else
        {
         Cat.GetComponent<Damageable>().IsAlive = true;
        Dog.GetComponent<Damageable>().IsAlive = true;

        Cat.gameObject.SetActive(true);
        Dog.gameObject.SetActive(true);
        Cat.transform.position = RemakePoint;
        Dog.transform.position= RemakePoint;
        }
        
    }

    private void Update()
    {
       FindCatAndDog();    
    }
}
