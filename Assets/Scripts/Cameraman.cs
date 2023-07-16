using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameraman : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float distance = 3f;

    [SerializeField]
    private float smoothTime = 0.2f;

    private float rotationY;
    private float rotationX;
    private float rotationZ = 0;

    private Vector3 currentRotation;
    private Vector3 velocity = Vector3.zero;

    private float targetFieldOfView = 45f;

    // Update is called once per frame
    void Update()
    {
        //transform.position = target.position;

        Vector3 moveDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W)) moveDir.x = +1f;
        if (Input.GetKey(KeyCode.S)) moveDir.x = -1f;
        if (Input.GetKey(KeyCode.D)) moveDir.y = -1f;
        if (Input.GetKey(KeyCode.A)) moveDir.y = +1f;

        rotationY += moveDir.y;
        rotationX += moveDir.x;

        //rotationX = Mathf.Clamp(rotationX, -40, 40);

        //different logic
        /*Quaternion qt = Quaternion.Euler(rotationX, rotationY, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, qt, Time.deltaTime * 10);*/

        //working logic
        Vector3 nextRotation = new Vector3(rotationX, rotationY, rotationZ);
        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref velocity, smoothTime);

        transform.localEulerAngles = currentRotation;
        transform.position = target.position - transform.forward * distance;

        if (Input.GetKeyDown(KeyCode.Return))
            ResetCameraman();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.E))
        {
            targetFieldOfView -= 5;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            targetFieldOfView += 5;
        }
        targetFieldOfView = Mathf.Clamp(targetFieldOfView, 20, 70);

        float zoomSpeed = 10f;
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetFieldOfView, Time.deltaTime * zoomSpeed);
    }

    private void ResetCameraman()
    {
        Debug.Log("IN");
        transform.position = new Vector3(0, 1, target.transform.position.z - 5);
        rotationX = 0;
        rotationY = 0;
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }
}