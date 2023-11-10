using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadSceneManager
{
    
    private static LoadSceneManager instance;
    public static LoadSceneManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LoadSceneManager();
                
            }
            return instance;
           
        }
    } 
    public void LoadScene(string sceneName)
    {
        
        SceneManager.LoadScene(sceneName);
    }
    
}