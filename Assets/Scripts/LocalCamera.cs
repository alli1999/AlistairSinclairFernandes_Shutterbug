using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalCamera : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 1f;

    [SerializeField]
    private float smoothTime = 0.2f;

    private float localRotationY;
    private float localRotationX;

    private Vector3 currentLocalRotation;
    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        //local rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        localRotationY += mouseX;
        localRotationX += -mouseY;

        localRotationX = Mathf.Clamp(localRotationX, -10, 10);
        localRotationY = Mathf.Clamp(localRotationY, -25, 25);

        Vector3 nextLocalRotation = new Vector3(localRotationX, localRotationY);
        currentLocalRotation = Vector3.SmoothDamp(currentLocalRotation, nextLocalRotation, ref velocity, smoothTime);
        //Debug.Log(currentLocalRotation);

        transform.localEulerAngles = currentLocalRotation;

        if (Input.GetKeyDown(KeyCode.Return))
            ResetCamera();
    }

    public void ResetCamera()
    {
        localRotationY = 0;
        localRotationX = 0;
    }
}
