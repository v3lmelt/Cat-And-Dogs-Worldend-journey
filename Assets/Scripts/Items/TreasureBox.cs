using System;
using System.Collections;
using System.Collections.Generic;
using Cainos.PixelArtPlatformer_VillageProps;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreasureBox : MonoBehaviour
{
    // 要生成的物品
    public List<GameObject> itemToSpawn;

    // 隔多久后生成物品（生成物品的延时时间）
    [Tooltip("延时多久生成物品")]
    public float delayTime;
    
    [Tooltip("修正物品生成的位置")]
    public Vector3 spawnPositionDelta;
    [Tooltip("是否要对生成的物品添加力，注意使用这个必须所有物品都带有rigidbody2D组件")]
    public bool addForceToSpawnedItem;
    
    [Tooltip("左边界，必须为负值")]
    public float leftBorder = -1.0f;
    [Tooltip("右边界，必须为正值")]
    public float rightBorder = 1.0f;

    [Tooltip("是否为怪物箱, 若为怪物箱的话所有生成的物品应当都为怪物")] 
    public bool isMonsterBox = false;
    
    private Animator _animator;
    
    private float _timer;
    private bool _hasOpened;

    private void Awake()
    {
        GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if(isMonsterBox) _animator.SetTrigger(ChestAnimationStrings.monsterTrigger);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 只能是玩家捡到宝箱
        if (!TagUtil.ComparePlayerTag(other.gameObject) || _hasOpened) return;
        // 捡到宝箱后触发动画
        _animator.SetTrigger(ChestAnimationStrings.openTrigger);
        _hasOpened = true;
        // 触发协程，延迟生成物品
        StartCoroutine(SpawnItemCoroutine());
    }

    private IEnumerator SpawnItemCoroutine()
    {
        while (_timer < delayTime)
        {
            _timer += delayTime;
            yield return null;
        }
        if (itemToSpawn.Count == 0)
        {
            Debug.LogError("Have you forgot to set the itemToSpawn?");
            yield break;
        }
        
        // 不给生成的物品加上力，那么就默认的正常从左到右进行排列
        if (!addForceToSpawnedItem)
        {
            // 排列的左右边界
            var position1 = transform.position;
            
            var left = position1.x + leftBorder;
            var right = position1.x + rightBorder;
            
            var currentX = left;
            foreach (var item in itemToSpawn)
            {
                // 如果只有一个物品，那么就生成在正中央
                if (itemToSpawn.Count == 1)
                {
                    Instantiate(item, transform.position + spawnPositionDelta
                        , Quaternion.identity);
                }
                // 否则从左到右进行排列
                else
                {
                    var position = transform.position;
                    Instantiate(item, new Vector3(
                        currentX, position.y, position.z), Quaternion.identity);
                    currentX += (right - left) / (itemToSpawn.Count - 1);
                }
            }
        }
        else
        {
            foreach (var item in itemToSpawn)
            {
                var position = transform.position;
                var spawnedItem = Instantiate(item, new Vector3(
                    position.x, position.y, position.z), Quaternion.identity);
                
                var xForce = Random.Range(0, 3) * 1f;
                var yForce = Random.Range(1, 4) * 2f;
                spawnedItem.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
            }
        }
    }
}
