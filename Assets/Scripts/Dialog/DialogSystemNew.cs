using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

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
    
    [FormerlySerializedAs("_index")] [SerializeField]
    private int index;//显示文本读到第几行
        
    private bool _cancelTyping;//取消逐字输入
    private bool _textFinished;//文本输出完成
    //存储文本
    private readonly List<string> _textList = new List<string>();


    [SerializeField] private bool hasFinishedDialog;
    public bool HasFinishedDialog => hasFinishedDialog;

    protected override void Awake()
    {
        base.Awake();
        hasFinishedDialog = true;
        _rectTransform = GetComponent<RectTransform>();
        
        // 摆在玩家看不见的位置!
        _rectTransform.position = new Vector3(-500, -500, 0);
    }

    // 初始化对话系统
    public void InitDialogSystem(TextAsset textAsset, Vector3 npcPosition, float textSpeed)
    {
        hasFinishedDialog = false;
        gameObject.SetActive(true);
        
        _textFile = textAsset;
        _rectTransform.position = npcPosition;
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
        if (Input.GetKeyDown(KeyCode.R) && index == _textList.Count)
        {
            ResetDialogSystem();
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
        index = 0;

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
        var avatarToAdd = _textList[index].Trim();
        
        try
        {
            avatar.sprite = Resources.Load<Sprite>("DialogAvatar/" + avatarToAdd);
            index++;
        }
        catch (Exception e)
        {
            // 可能会有资源找不到的情况，提示报错信息
            Debug.LogException(e);
        }
        
        int letter = 0;
        while (!_cancelTyping && letter < _textList[index].Length-1)
        {
            textLabel.text += _textList[index][letter];
            letter++;
            yield return new WaitForSeconds(_textSpeed);
        }
        
        textLabel.text = _textList[index];
        _cancelTyping = false;
        _textFinished = true;
        index++;
    }

    public void ResetDialogSystem()
    {
        index = 0;
        
        gameObject.SetActive(false);
        hasFinishedDialog = true;
        _rectTransform.position = new Vector3(-500, -500, 0);
    }
}
