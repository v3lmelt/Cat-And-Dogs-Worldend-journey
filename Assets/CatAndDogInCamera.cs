using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CatAndDogInCamera : MonoBehaviour
{
    private Transform CatAndDog;
    public Transform Cat;
    public Transform Dog;
    public Vector3 CatBeforeExit;
    public Vector3 DogBeforeExit;
    private Camera cm;
    void Start()
    {
        Cat = GameObject.Find("Cat").transform;
        Dog = GameObject.Find("Dog").transform; 
        CatAndDog = new GameObject("CatAndDog").transform;
        cm = Camera.main;
        gameObject.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = GameObject.Find("Level Border").GetComponent<PolygonCollider2D>();
    }

    
    void Update()

    {
        // 如果猫狗不在相机中，让猫狗回到相机中
        if (!IsVisableInCamera())
        {
            Cat.position = CatBeforeExit;
            Dog.position = DogBeforeExit;
        }
        // 摄像机看到猫狗后，将猫狗的位置赋值给CatAndDog，然后将CatAndDog作为摄像机的跟随目标
        CatAndDog.position = (Cat.position + Dog.position) / 2;
        CatAndDog.localScale = (Cat.localScale + Dog.localScale) / 2;
        CatAndDog.rotation = new Quaternion(0,0,0,0);
        gameObject.GetComponent<CinemachineVirtualCamera>().Follow = CatAndDog;
        //储存猫狗出相机前的位置
        CatBeforeExit = Cat.position;
        DogBeforeExit = Dog.position;
    }
    public bool IsVisableInCamera()
    {
        
        //猫狗坐标
        Vector3 cpos = Cat.position;
        Vector3 dpos = Dog.position;
        //猫狗在相机中的坐标
        Vector3 cviewPos = cm.WorldToViewportPoint(cpos);
        Vector3 dviewPos = cm.WorldToViewportPoint(dpos);
        //x,y坐标都在0-1之间，说明在相机中
        if (cviewPos.x > 0 && cviewPos.x < 1 && cviewPos.y > 0 && cviewPos.y < 1 && dviewPos.x > 0 && dviewPos.x < 1 && dviewPos.y > 0 && dviewPos.y < 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
