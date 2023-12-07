using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SleepPoint : MonoBehaviour
{
    public GameObject SleepStory;

    public GameObject icon;

    private bool _interactable;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!TagUtil.ComparePlayerTag(collision.gameObject)) return;
        icon.SetActive(true);
        _interactable = true;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!TagUtil.ComparePlayerTag(collision.gameObject)) return;
        if (icon == null) return;
        icon.SetActive(false);
        _interactable = false;
    }
    private void Update()
    {
        if (_interactable && Input.GetKeyDown(KeyCode.R))
        {
            
           GameManager.Instance.cat.GetComponent<Damageable>().Health = GameManager.Instance.cat.GetComponent<Damageable>().MaxHealth;
           GameManager.Instance.cat.GetComponent<Damageable>().MP = GameManager.Instance.cat.GetComponent<Damageable>().MaxMP;
           GameManager.Instance.dog.GetComponent<Damageable>().Health = GameManager.Instance.dog.GetComponent<Damageable>().MaxHealth;
           GameManager.Instance.dog.GetComponent<Damageable>().MP = GameManager.Instance.dog.GetComponent<Damageable>().MaxMP;
           SleepStory.GetComponent<PlayableDirector>().Play();

        }
    }
}
