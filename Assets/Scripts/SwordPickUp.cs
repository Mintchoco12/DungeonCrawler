using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPickUp : MonoBehaviour
{
    private Sprite icon;

    private void Start()
    {
        icon = GetComponent<SpriteRenderer>().sprite;
    }

    //Picks up sword and replaces the temporary sprite
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<Equipment>().SetIcon(icon);
            Destroy(gameObject);
        }
    }
}
