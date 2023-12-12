using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class CatAndDogInCamera : MonoBehaviour
{
    private Transform _catAndDog;
    [FormerlySerializedAs("Cat")] public Transform cat;
    [FormerlySerializedAs("Dog")] public Transform dog;
    [FormerlySerializedAs("CatBeforeExit")] public Vector3 catBeforeExit;
    [FormerlySerializedAs("DogBeforeExit")] public Vector3 dogBeforeExit;
    private Camera _cm;
    private CinemachineVirtualCamera _cinemachineVirtualCamera;

    private PlayerController _catDamageable;
    private PlayerController _dogDamageable;

    private void Awake()
    {
        _cinemachineVirtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
        gameObject.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = GameObject.Find("Level Border").GetComponent<PolygonCollider2D>();
        
        
    }

    private void Start()
    {
        cat = GameObject.Find("Cat").transform;
        dog = GameObject.Find("Dog").transform;
        _catAndDog = new GameObject("CatAndDog").transform;
        _cm = Camera.main;

        if (cat != null) _catDamageable = cat.GetComponent<PlayerController>();
        if (dog != null) _dogDamageable = cat.GetComponent<PlayerController>();
    }

    
    private void FixedUpdate()

    {
        // 如果猫狗不在相机中，让猫狗回到相机中
        // if (!IsVisibleInCamera())
        // {
        //     cat.position = catBeforeExit;
        //     dog.position = dogBeforeExit;
        //
        //     _catDamageable.CanMove = true;
        //     _dogDamageable.CanMove = true;
        // }
        // else
        // {
        //     _catDamageable.CanMove = false;
        //     _dogDamageable.CanMove = false;
        // }
        // 摄像机看到猫狗后，将猫狗的位置赋值给CatAndDog，然后将CatAndDog作为摄像机的跟随目标
        var catPos = cat.position;
        var dogPos = dog.position;
        
        _catAndDog.position = (catPos + dogPos) / 2;
        _catAndDog.localScale = (cat.localScale + dog.localScale) / 2;
        _catAndDog.rotation = new Quaternion(0,0,0,0);
        
        _cinemachineVirtualCamera.Follow = _catAndDog;
        //储存猫狗出相机前的位置
        // catBeforeExit = catPos;
        // dogBeforeExit = dogPos;
    }
    private bool IsVisibleInCamera()
    {
        
        //猫狗坐标
        Vector3 cPos = cat.position;
        Vector3 dPos = dog.position;
        //猫狗在相机中的坐标
        Vector3 cViewPos = _cm.WorldToViewportPoint(cPos);
        Vector3 dViewPos = _cm.WorldToViewportPoint(dPos);
        //x,y坐标都在0-1之间，说明在相机中
        return cViewPos.x is > 0 and < 1 && cViewPos.y is > 0 and < 1 && dViewPos.x is > 0 and < 1 && dViewPos.y is > 0 and < 1;
    }
}
