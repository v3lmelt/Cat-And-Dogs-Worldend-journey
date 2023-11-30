using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Goods : MonoBehaviour
{
    public GameObject goods;
    public string price;
    public GameObject icon;
    bool _interactable;
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = goods.GetComponent<SpriteRenderer>().sprite;
        gameObject.transform.Find("Canvas").Find("Price").GetComponent<TextMeshProUGUI>().text = price;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Util.ComparePlayerTag(collision.gameObject)) return;
        icon.SetActive(true);
        _interactable = true;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!Util.ComparePlayerTag(collision.gameObject)) return;
        icon.SetActive(false);
        _interactable = false;
    }
    private void Update()
    {
        if (_interactable && Input.GetKeyDown(KeyCode.R))
        {
            if (GameManager.Instance.CoinNum < int.Parse(price))
            {
                TextManager.Instance.OnCreatingStatusText(gameObject.transform.position, "No money!");
                return;
            }
            Instantiate(goods, gameObject.transform.position, Quaternion.identity);
            GameManager.Instance.GetMoney(-int.Parse(price));
            gameObject.SetActive(false);
        }
    }
}
