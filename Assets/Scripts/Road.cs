using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Road : MonoBehaviour
{
    private Action<Road> _removeAction;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Init(Action<Road> removeAction)
    {
        _removeAction = removeAction;
    }

    private void Update()
    {
        if (Vector3.Distance(new Vector3(0, 0, player.transform.position.z), new Vector3(0, 0, transform.position.z)) > 20 && player.transform.position.z > transform.position.z)
        {
            _removeAction(this);
        }
    }
}
