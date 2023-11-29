using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class LoadSceneManager:Singleton<LoadSceneManager>
{
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    public void LoadScene(string sceneName)
    {
        
        SceneManager.LoadScene(sceneName);
        

    }
    public void LoadScene(string sceneName,Vector3 TpPoint)
    {
        if (TpPoint == new Vector3(0,0,0))
        {
            LoadScene(sceneName);
        }
        else
        {
        SceneManager.LoadScene(sceneName);
        StartCoroutine(WaitToTp(TpPoint));
        }
        
    }
    IEnumerator WaitToTp(Vector3 TpPoint)
    {
        yield return new WaitForSeconds(0.001f);
        GameManager.Instance.FindCatAndDog();
        GameManager.Instance.cat.transform.position = TpPoint;
        GameManager.Instance.dog.transform.position = TpPoint;
    }
    private void OnEnable()
    {
        
        
    
    }

}