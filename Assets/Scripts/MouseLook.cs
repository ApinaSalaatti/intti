using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private Transform mainCamera;

    [SerializeField]
    private float sensitivity = 1f;
    [SerializeField]
    private float maxVerticalRotation = 60f;

    private bool inverted = false;
    public bool Inverted { set { inverted = value; } }

    private float currentVerticalRotation = 0f;
    private float currentHorizontalRotation = 0f;

    private Vector3 cameraNormalOffset;
    private bool crouching = false;

    // Use this for initialization
    void Start()
    {
        currentHorizontalRotation = transform.rotation.eulerAngles.y;
        currentVerticalRotation = transform.rotation.eulerAngles.x;

        cameraNormalOffset = mainCamera.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float inv = inverted ? 1f : -1f;
        float horRotation = Input.GetAxis("Mouse X") * sensitivity;
        float verRotation = Input.GetAxis("Mouse Y") * sensitivity * inv;

        currentHorizontalRotation += horRotation;

        currentVerticalRotation += verRotation;
        currentVerticalRotation = Mathf.Clamp(currentVerticalRotation, -maxVerticalRotation, maxVerticalRotation);

        transform.rotation = Quaternion.Euler(0f, currentHorizontalRotation, 0f);
        mainCamera.rotation = Quaternion.Euler(currentVerticalRotation, currentHorizontalRotation, 0f);

        // Crouching!
        //if(Input.GetButtonDown("Crouch"))
        //{
        //    ToggleCrouch();
        //}
    }

    private void ToggleCrouch ()
    {
        crouching = !crouching;
        if(crouching)
        {
            mainCamera.position = transform.position + cameraNormalOffset - new Vector3(0f, 1f, 0f);
        }
        else
        {
            mainCamera.position = transform.position + cameraNormalOffset;
        }
    }
}
