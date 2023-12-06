using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Btn_Setting : MonoBehaviour
{
    public GameObject settingsPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onSettingsHandler()
    {
        settingsPanel.SetActive(true);
    }
}
