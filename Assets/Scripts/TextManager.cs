using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class TextManager : Singleton<TextManager>
{
    public Canvas canvas;
    public TMP_Text tmpTextPrefab;

    private Camera _camera;
    
    [SerializeField]
    private float playerPosFixedDelta = 3.5f;

    protected override void Awake()
    {
        base.Awake();
        _camera = FindObjectOfType<Camera>();
        // 摄像机应该不为空!
        Assert.AreNotEqual(_camera, null);
    }
    
    // 当玩家触发某种药水效果的时候，调用此方法创建文本
    public void OnCreatingPotionText(Vector3 playerPosition, string textContent)
    {
        // 提供一个修正值，防止文本被玩家挡住
        var fixedPlayerPosition = new Vector3(playerPosition.x, playerPosition.y + playerPosFixedDelta,
            playerPosition.z);
        var textObject = Instantiate(tmpTextPrefab, _camera.WorldToScreenPoint(fixedPlayerPosition), Quaternion.identity,
            canvas.transform);

        textObject.text = textContent;
    }
    
    // TODO, 玩家触发某种状态的时候，调用此方法创建文本
    public void OnCreatingStatusText(Vector3 playerPosition, string textContent)
    {
        var fixedPlayerPosition = new Vector3(playerPosition.x, playerPosition.y + playerPosFixedDelta,
            playerPosition.z);
        var textObject = Instantiate(tmpTextPrefab, _camera.WorldToScreenPoint(fixedPlayerPosition), Quaternion.identity,
            canvas.transform);

        textObject.text = textContent;
    }
}
