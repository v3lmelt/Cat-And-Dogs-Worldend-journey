using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogSystem : MonoBehaviour
{
    [Header("UI组件")]
    public Text textLabel;//将Text组件拖拽至此
    public Image faceImage;//这个是实现文本框中可以在摸个位置显示设定的角色，将Image组件拖拽至此

    [Header("文本文件")]
    public TextAsset textFile; //在Unity中，将编写的文本拖拽到这里
    public int index;//显示文本读到第几行
    public float textSpeed;

    [Header("头像")]
    public Sprite player;

    [Header("头像")]
    public Sprite guider;

    public bool cancelTyping;//取消逐字输入
    public bool textFinished;//文本输出完成

    //存储文本
    private readonly List<string> _textList = new List<string>();

    private void Awake()
    {
        GetTextFormFile(textFile);
    }
    private void OnEnable()
    {
        /*textLabel.text = textList[index].ToString();
        index++;*/
        textFinished = true;
        StartCoroutine(SetTextUI());
    }

    // Update is called once per frame
    private void Update()
    {
        //输入键值R进行操作
        if (Input.GetKeyDown(KeyCode.R)&& index == _textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        /*if (Input.GetKeyDown(KeyCode.R) && textFinished)
        {
            *//*textLabel.text = textList[index].ToString();
            index++;*//*
            StartCoroutine(SetTextUI());
        }*/
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (textFinished&& !cancelTyping)
            {
                StartCoroutine(SetTextUI());
            }
            //这个意思就是双击按键R就可以快速实现文本的显示
            else if (!textFinished && !cancelTyping)
            {
                cancelTyping = true;
            }
        }
    }

    private void GetTextFormFile(TextAsset file)
    {
        _textList.Clear();
        index = 0;

        //切割文本Text.text，按行来切割
        var lineDate = file.text.Split('\n');

        foreach (var line in lineDate)
        {
            _textList.Add(line);
        }
    }

    IEnumerator SetTextUI()
    {
        textFinished = false;
        textLabel.text = "";//清空界面显示的文本，以便其他文本的显示
        
        switch (_textList[index].Trim())
        {
            case "A":
                faceImage.sprite = player;
                index++;
                break;
            case "B":
                faceImage.sprite = guider;
                index++;
                break;
            case "C":
                faceImage.sprite = guider;
                index++;
                break;
        }

        /*for (int i = 0; i < textList[index].Length; i++)
        {
            textLabel.text += textList[index][i];
            
            yield return new WaitForSeconds(textSpeed);
        }*/
        int letter = 0;
        while (!cancelTyping && letter < _textList[index].Length-1)
        {
            textLabel.text += _textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);
        }
        textLabel.text = _textList[index];
        cancelTyping = false;
        textFinished = true;
        index++;
    } 
}
