using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float lookAtDistance = 2.5f;

    public float verticalRotationUpperBound = 270.0f;
    public float verticalRotationLowerBound = 90.0f;
    public float rotationSpeedFactor = 5.0f;
    private float verticalRotation = 160.0f;
    private float horizontalRotation = 90.0f;

    public string mouseXInput = "Mouse X";
    public string mouseYInput = "Mouse Y";
    private float mouseX;
    private float mouseY;

    private Vector3 yOffset = new Vector3(0.0f, 1.5f, 0.0f);

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        mouseX = Input.GetAxis(mouseXInput);
        mouseY = Input.GetAxis(mouseYInput);
        SetPosition();
        transform.LookAt(player.position + yOffset);
    }

    private void SetPosition()
    {
        Vector3 newPosition = CalculateNewPosition();
        transform.position = player.position + yOffset + newPosition;
    }

    private Vector3 CalculateNewPosition()
    {
        verticalRotation += rotationSpeedFactor * mouseY;
        horizontalRotation += rotationSpeedFactor * -mouseX;
        LimitVerticalRotation();
        float rotationPhi = verticalRotation * Mathf.Deg2Rad;
        float rotationTheta = horizontalRotation * Mathf.Deg2Rad;
        float pointX = lookAtDistance * Mathf.Cos(rotationPhi) * Mathf.Cos(rotationTheta);
        float pointY = lookAtDistance * Mathf.Sin(rotationPhi);
        float pointZ = lookAtDistance * Mathf.Cos(rotationPhi) * Mathf.Sin(rotationTheta);
        Vector3 newPosition = new Vector3(pointX, pointY, pointZ);
        newPosition = RoundVector3(newPosition, -3);
        return newPosition;
    }

    private Vector3 RoundVector3(Vector3 vector, int digit)
    {
        float shift = Mathf.Pow(10, -digit);
        vector.x = Mathf.Round(vector.x * shift) / shift;
        vector.y = Mathf.Round(vector.y * shift) / shift;
        vector.z = Mathf.Round(vector.z * shift) / shift;

        return vector;
    }

    private void LimitVerticalRotation()
    {
        if (ExceedVerticalRotationUpperBound())
            verticalRotation = verticalRotationUpperBound;
        else if (ExceedVerticalRotationLowerBound())
            verticalRotation = verticalRotationLowerBound;
    }

    private bool ExceedVerticalRotationUpperBound()
    {
        return verticalRotation > verticalRotationUpperBound;
    }

    private bool ExceedVerticalRotationLowerBound()
    {
        return verticalRotation < verticalRotationLowerBound;
    }
}
