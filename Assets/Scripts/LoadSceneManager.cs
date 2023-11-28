using UnityEngine.SceneManagement;


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
    private void OnEnable()
    {
        
        
    
    }

}