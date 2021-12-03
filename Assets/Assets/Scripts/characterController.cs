using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    //movement
    [SerializeField] private float moveSpeed;
    [SerializeField] private CharacterController controller;
    [SerializeField] private FixedJoystick joystick;

    //bounds
    [SerializeField] private Camera mainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    private void FixedUpdate()
    {
        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        //controller.Move(move * Time.deltaTime * moveSpeed);

        Vector3 move = Vector3.up * joystick.Vertical + Vector3.right * joystick.Horizontal; 
        controller.Move(move * Time.deltaTime * moveSpeed);


        Vector3 viewPos = transform.position;

        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);

        transform.position = viewPos;
    }
}
