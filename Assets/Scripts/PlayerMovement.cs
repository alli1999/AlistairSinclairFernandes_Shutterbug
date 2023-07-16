using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float curSpeed = 10;

    private Rigidbody rb;

    private int curLane = 1;

    private float moveValX = 0;

    private Vector3 currentPosition;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && curLane > 0)
        {
            curLane--;
            moveValX -= 3;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && curLane < 2)
        {
            curLane++;
            moveValX += 3;
        }

        float moveVal = Mathf.Lerp(transform.position.x, moveValX, Time.deltaTime * 5);
        transform.position = new Vector3(moveVal, transform.position.y, transform.position.z);

        rb.velocity = new Vector3(0, 0, curSpeed);
    }
}
