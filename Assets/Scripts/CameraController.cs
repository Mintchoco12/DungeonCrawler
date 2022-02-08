using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private Coroutine currrentCoroutine;

    [SerializeField]
    private Vector2 viewportSize;

    void Update()
    {
        if (gameObject.GetComponent<Camera>().WorldToViewportPoint(player.transform.position).y >= 0.75f)
        {
            if (currrentCoroutine == null)
                currrentCoroutine = StartCoroutine(MoveCam(Vector3.up));
        }
        if (gameObject.GetComponent<Camera>().WorldToViewportPoint(player.transform.position).y <= 0)
        {
            if (currrentCoroutine == null)
                currrentCoroutine = StartCoroutine(MoveCam(Vector3.down));
        }
        if (gameObject.GetComponent<Camera>().WorldToViewportPoint(player.transform.position).x <= 0)
        {
            if (currrentCoroutine == null)
                currrentCoroutine = StartCoroutine(MoveCam(Vector3.left));
        }
        if (gameObject.GetComponent<Camera>().WorldToViewportPoint(player.transform.position).x >= 1)
        {
            if (currrentCoroutine == null)
                currrentCoroutine = StartCoroutine(MoveCam(Vector3.right));
        }
    }

    IEnumerator MoveCam(Vector3 direction)
    {
        // geen playermovement
        // player.GetComponent<PlayerMovenemt>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;        
        // wacht 0.2 seconden
        yield return new WaitForSeconds(0.2f);

        // beweeg over 1 seconde
        float elapsedTime = 0;
        float overTime = 1;
        Vector3 end = transform.position + new Vector3(direction.x * viewportSize.x, direction.y * viewportSize.y, 0);
        while (elapsedTime < overTime)
        {
            transform.position = Vector3.Lerp(transform.position, end, elapsedTime / overTime);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = end;

        // wacht 0.2 seconden
        yield return new WaitForSeconds(0.2f);

        // playermovement aan
        player.GetComponent<PlayerMovement>().enabled = true;

        currrentCoroutine = null;
    }
}