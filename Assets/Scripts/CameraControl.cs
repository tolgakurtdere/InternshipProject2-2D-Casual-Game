using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void LateUpdate() //suggested for camera events
    {
        if(player.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
        }
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 initPos = transform.localPosition;
        float tempTime = 0;
        while (tempTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x + initPos.x, y + initPos.y, initPos.z);
            tempTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = initPos;
    }

}
