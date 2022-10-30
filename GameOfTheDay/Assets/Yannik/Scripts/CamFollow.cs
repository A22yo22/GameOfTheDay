using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform playerPos;

    void Update()
    {
        transform.position = playerPos.position;
    }
}
