using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogSystemNew : Singleton<DialogSystemNew>
{
    [Header("UI组件")]
    // 将Text组件拖拽至此
    public TMP_Text textLabel;
    // 这个是实现文本框中可以在摸个位置显示设定的角色，将sprite组件拖拽至此
    public SpriteRenderer avatar;
    // 字体速度
    private float  _textSpeed;
    
    private TextAsset _textFile; //在Unity中，将编写的文本拖拽到这里

    private RectTransform _rectTransform;
    private int _index;//显示文本读到第几行
    
    private bool _cancelTyping;//取消逐字输入
    private bool _textFinished;//文本输出完成
    //存储文本
    private readonly List<string> _textList = new List<string>();

    protected override void Awake()
    {
        base.Awake();
        _rectTransform = GetComponent<RectTransform>();
        
        // 初始时不应该是激活状态!
        gameObject.SetActive(false);
    }

    // 初始化对话系统
    public void InitDialogSystem(TextAsset textAsset, Transform npcTransform, float textSpeed)
    {
        gameObject.SetActive(true);
        
        _textFile = textAsset;
        _rectTransform.position = npcTransform.position;
        _textSpeed = textSpeed;
        
        if (_textFile == null)
        {
            Debug.LogError("_textFile is null! Did you forget to initialize the text file?");
            return;
        }
        GetTextFromFile();
        
        _textFinished = true;
        StartCoroutine(SetTextUI());    
    }

    // Update is called once per frame
    private void Update()
    {
        //输入键值R进行操作
        if (Input.GetKeyDown(KeyCode.R)&& _index == _textList.Count)
        {
            gameObject.SetActive(false);
            _index = 0;
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (_textFinished&& !_cancelTyping)
            {
                StartCoroutine(SetTextUI());
            }
            //这个意思就是双击按键R就可以快速实现文本的显示
            else if (!_textFinished && !_cancelTyping)
            {
                _cancelTyping = true;
            }
        }
    }

    private void GetTextFromFile()
    {
        _textList.Clear();
        _index = 0;

        //切割文本Text.text，按行来切割
        var lineDate = _textFile.text.Split('\n');

        foreach (var line in lineDate)
        {
            _textList.Add(line);
        }
    }

    private IEnumerator SetTextUI()
    {
        _textFinished = false;
        textLabel.text = "";//清空界面显示的文本，以便其他文本的显示
        
        // 动态加载头像，并且把头像加载到对话框中
        var avatarToAdd = _textList[_index].Trim();
        
        try
        {
            avatar.sprite = Resources.Load<Sprite>("DialogAvatar/" + avatarToAdd);
            _index++;
        }
        catch (Exception e)
        {
            // 可能会有资源找不到的情况，提示报错信息
            Debug.LogException(e);
        }
        
        int letter = 0;
        while (!_cancelTyping && letter < _textList[_index].Length-1)
        {
            textLabel.text += _textList[_index][letter];
            letter++;
            yield return new WaitForSeconds(_textSpeed);
        }
        
        textLabel.text = _textList[_index];
        _cancelTyping = false;
        _textFinished = true;
        _index++;
    } 
}
