using System.Collections;
using TMPro;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    
    public string remakeSceneName;
    public Vector3 remakePoint;

    
    public GameObject cat;
    public GameObject dog;
    public GameObject Canvas_1;//血条金钱ui
    public GameObject CoinPrefab;
    public int CoinNum;
    private void Start()
    {
        DontDestroyOnLoad(this);
        FindCatAndDog();
    }

    private void OnEnable()
    {
        // 订阅事件, 在场景加载的时候尝试去找猫和狗部件
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    // 
    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        FindCatAndDog();
        GetMoney(0);
        LoadSceneManager.Instance.CheckObjectPlayerPrefs();

        Debug.Log("On Scene Loaded: " + scene.name);
        if(PlayerStatUtil.SceneExcludeFromGettingComponents.Contains(scene.name)) return;
        FindCatAndDog();
        PlayerStatUtil.GetComponents();
        if(PlayerStatUtil.SceneExcludeFromStatRestore.Contains(scene.name)) return;
        PlayerStatUtil.RestorePlayerStats();
    }

    private static void OnSceneUnloaded(Scene current)
    {
        Debug.Log("On Scene unloaded: " + current.name);
        
        if(current.name == null || PlayerStatUtil.SceneExcludeFromStatRecord.Contains(current.name)) return;
        PlayerStatUtil.RecordPlayerStats();
    }
    
    public void FindCatAndDog()
    {
        cat = GameObject.Find("Cat");
        dog = GameObject.Find("Dog");
        Canvas_1 = GameObject.Find("Canvas_1");

    }
    public void Dead()
    {
        Debug.Log("死了");
        cat.gameObject.SetActive(false);
        dog.gameObject.SetActive(false);
       
        cat.GetComponent<Damageable>().Health = cat.GetComponent<Damageable>().MaxHealth;
        dog.GetComponent<Damageable>().Health = dog.GetComponent<Damageable>().MaxHealth;
   
        StartCoroutine(WaitToRemake());
    }
    //计时几秒后跳转至复活点
    IEnumerator WaitToRemake()
    {
        // Debug.Log("进入了携程");
        // yield return new WaitForSeconds(3f); 
        if (SceneManager.GetActiveScene().name!=remakeSceneName)
        {
            LoadSceneManager.Instance.LoadScene(remakeSceneName);
                yield return new WaitForSeconds(0.001f); //Wait期间会自动调用Update函数 这里直接写Instance.FindCatAndDog()是找不到猫狗的 
                cat.transform.position = remakePoint;
                dog.transform.position = remakePoint;
            
        }
        else
        {
            
            cat.GetComponent<Damageable>().IsAlive = true;
            dog.GetComponent<Damageable>().IsAlive = true;

            cat.gameObject.SetActive(true);
            dog.gameObject.SetActive(true);
            cat.transform.position = remakePoint;
            dog.transform.position = remakePoint;

        }
        yield break;
    }
    public void GetMoney(int num)
    {
        if(Canvas_1 ==null)
        {
            return;
        }
        Canvas_1.transform.Find("Coin").transform.Find("CoinNum").GetComponent<TextMeshProUGUI>().text = (CoinNum+=num).ToString();

    }
}
