using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    [SerializeField] private Image iconButton;

    private void Start()
    {
        StartInactive();
    }

    //Disables temporary image 
    private void StartInactive()
    {
        if (iconButton.sprite == null)
        {
            iconButton.gameObject.SetActive(false);
        }
    }

    //Changes temporary sprite to sword and enables it
    public void SetIcon(Sprite iconSprite)
    {
        iconButton.gameObject.SetActive(true);
        iconButton.sprite = iconSprite;
    }
}
