using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] private float zoomSpeed;
    [SerializeField] private GameObject cameraAnchor;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float distance;
    private Vector3 anchor;

    void Start()
    {
        anchor = cameraAnchor.transform.position;
        transform.LookAt(anchor);
    }

    void Update()
    {
        if (Input.GetMouseButton (1))
        {
            transform.RotateAround(anchor, new Vector3(0, 1, 0), Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime);
        }

        distance = Vector3.Distance(transform.position, anchor);
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (distance > 1.5f)
                transform.position += transform.forward * zoomSpeed * Time.deltaTime;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (distance < 10)
                transform.position -= transform.forward * zoomSpeed * Time.deltaTime;
        }
    }
}
