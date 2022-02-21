using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    private PlayerHealth playerHealth;

    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private GameObject heartsParent;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private List<Image> heartImages = new List<Image>();

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth == null)
        {
            Debug.LogError("Can't find Playerhealth in scene, Object might be inactive");
            return;
        }

        for (int i = 0; i < playerHealth.GetMaxHealth(); i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartsParent.transform);
            heartImages.Add(heart.GetComponent<Image>());
        }
    }

    private void Update()
    {
        HeartsUpdate();
    }

    public void HeartsUpdate()
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            if (i + 0.5f == playerHealth.GetCurrentHealth())
            {
                heartImages[i].sprite = halfHeart;
            }
            else if (i < playerHealth.GetCurrentHealth())
            {
                heartImages[i].sprite = fullHeart;
            }   
            else
            {
                heartImages[i].sprite = emptyHeart;
            }
        }
    }
}
