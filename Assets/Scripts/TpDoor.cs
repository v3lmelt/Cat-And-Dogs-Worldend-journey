using UnityEngine;
public class TpDoor : MonoBehaviour
{
    public string destination; 
    public GameObject icon;
    
    private bool _interactable;
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
            LoadSceneManager.Instance.LoadScene(destination);
        }
    }
}
