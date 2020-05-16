using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BecomePlayerChild : MonoBehaviour
{
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
            return;

        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;
    }
}
