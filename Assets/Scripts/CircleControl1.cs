using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleControl1 : MonoBehaviour
{
    public int velocity = 100;
    void Update()
    {
        transform.Rotate(0, 0, velocity * Time.deltaTime);
    }
}
