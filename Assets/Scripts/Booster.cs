using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    private float onTime = 0;
    private bool isON = true;
    void Update()
    {
        onTime += Time.deltaTime;
        if(onTime >= 0.5f)
        {
            onTime = 0;
            if (isON)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;
                isON = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = true;
                GetComponent<BoxCollider2D>().enabled = true;
                isON = true;
            }
        }
    }

}
