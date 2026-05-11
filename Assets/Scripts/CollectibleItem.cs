using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public GameObject collectEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (collectEffect != null)
            {
                Instantiate(collectEffect, transform.position, Quaternion.identity);
            }
            GameManager.instance.AddScore();
            Destroy(gameObject);
        }
    }
}