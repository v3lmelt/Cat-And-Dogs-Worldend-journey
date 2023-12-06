using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    public GameObject settingsPrefab;
    private GameObject settingsPanel;
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickSettingsButtonHandler()
    {
        if (!settingsPanel)
        {
            settingsPanel = Instantiate(settingsPrefab,new Vector3(0,0,0), Quaternion.identity);
            settingsPanel.transform.SetParent(parent.GetComponent<Transform>());
            settingsPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
        }
        settingsPanel.SetActive(true);
    }
}
