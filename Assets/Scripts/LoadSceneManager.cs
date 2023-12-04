using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LoadSceneManager:Singleton<LoadSceneManager>
{
    public List<String> ObjectsToControl;
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
    public void ActivateObject(GameObject obj)
    {
        // 当物体被激活时，将其状态保存到PlayerPrefs,之后就不会再激活了
        PlayerPrefs.SetInt(obj.name, 1);
        
    }
    public void CheckObjectPlayerPrefs()
    {
        for (int i = ObjectsToControl.Count - 1; i >= 0; i--)
        {
            String ObjName = ObjectsToControl[i];
            // 检查PlayerPrefs，看看物体是否应该被禁用
            if (ObjName == null)
            {
                ObjectsToControl.RemoveAt(i);
                continue;
            }

                GameObject Obj;
                Obj = GameObject.Find(ObjName);
            if(Obj!=null)
            {
                if (PlayerPrefs.GetInt(ObjName) == 1)
                {
                Obj.SetActive(false);
                Debug.Log("禁用了" + ObjName);
                }
            ActivateObject(Obj);
            }
            
        }
    }
    void OnDisable()
    {
        // 当场返回编辑模式时，清除PlayerPrefs
        if (SceneManager.GetActiveScene().name != SceneManager.GetSceneByBuildIndex(0).name)
        {
            foreach (String obj in ObjectsToControl)
            {
                PlayerPrefs.DeleteKey(obj);
            }
        }
    }
}