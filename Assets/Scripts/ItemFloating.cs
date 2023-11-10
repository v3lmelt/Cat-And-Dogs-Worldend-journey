using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFloating : MonoBehaviour
{
    public float floatLength = 0.8f;
    public float floatRange = 0.25f;
    
    // 获取场景开始时物品所在的坐标，记录下来
    private Vector3 _startingGamePosition;
    private Vector3 _bottomPosition;
    
    private bool _hasHitBottom = false;
    private bool _hasHitUp = true;
    
    // Update is called once per frame

    private Vector3 _moveVelocity;
    private void Start()
    {
        _startingGamePosition = transform.position;
        _bottomPosition = new Vector3(_startingGamePosition.x, _startingGamePosition.y + floatRange,
            _startingGamePosition.z);
    }

    private void Update()
    {
        if (_hasHitUp)
        {
            transform.position =
                Vector3.SmoothDamp(transform.position, _bottomPosition, 
                    ref _moveVelocity, floatLength / 2);

            if (!(Math.Abs(transform.position.y - _bottomPosition.y) < 0.01f)) return;
            _hasHitUp = false;
            _hasHitBottom = true;
        }else if (_hasHitBottom)
        {
            transform.position =
                Vector3.SmoothDamp(transform.position, _startingGamePosition, 
                    ref _moveVelocity, floatLength / 2);

            if (!(Math.Abs(transform.position.y - _startingGamePosition.y) < 0.01f)) return;
            _hasHitUp = true;
            _hasHitBottom = false;
        }
    }
}
