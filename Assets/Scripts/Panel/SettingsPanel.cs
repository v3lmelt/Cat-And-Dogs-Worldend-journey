using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsPanel : MonoBehaviour
{
    public AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onCloseHandler()
    {
        gameObject.SetActive(false);
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("BGMVolume",value);
    }
}
