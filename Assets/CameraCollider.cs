using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider : MonoBehaviour
{
    public GameObject CameraLeftEdge;
    public GameObject CameraRightEdge;
    
    void Start()
    {
       Vector2 left = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f));
        Vector2 right = Camera.main.ViewportToWorldPoint(new Vector2(1f, 0.5f));
        CameraLeftEdge.transform.position = left;
        CameraRightEdge.transform.position = right;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
