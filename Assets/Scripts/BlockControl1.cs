using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControl1 : MonoBehaviour
{
    public int velocity = 1;
    private bool goRight = true;
    private bool goLeft = true;
    public bool isLeft;
    void Update()
    {
        if (isLeft)
        {
            if (transform.position.x >= -1.5f)
            {
                goRight = false;
            }
            if (transform.position.x <= -2)
            {
                goRight = true;
            }
            if (goRight) transform.position += new Vector3(velocity * Time.deltaTime, 0, 0);
            if (!goRight) transform.position += new Vector3(-velocity * Time.deltaTime, 0, 0);
        }
        else if (!isLeft)
        {
            if (transform.position.x <= 1.5f)
            {
                goLeft = false;
            }
            if (transform.position.x >= 2)
            {
                goLeft = true;
            }
            if (goLeft) transform.position += new Vector3(-velocity * Time.deltaTime, 0, 0);
            if (!goLeft) transform.position += new Vector3(velocity * Time.deltaTime, 0, 0);
        }
    }
}
