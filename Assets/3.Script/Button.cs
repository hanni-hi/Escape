using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Button : MonoBehaviour
{
    public Action  onButtonPressed;
    public Action onButtonReleased;

    private bool isPushed = false;
    public float moveSpeed = 3f;
    public float downDistance = 1f;
    public float upDistance = 3.5f;

    public GameObject connectedDoor;
    public Transform buttonTop;

    [HideInInspector] public Vector3 initialButtonPosition;
    [HideInInspector] public Vector3 initialDoorPosition;

    private void Start()
    {
        initialButtonPosition = buttonTop.position;
        initialDoorPosition = connectedDoor.transform.position;
    }

    private void Update()
    {
        if (isPushed)
        {
            Debug.Log("버튼눌렸다!!");

            //문을 윗쪽으로
            Vector3 doorTargetPosition = initialDoorPosition + new Vector3(0, upDistance, 0);
            connectedDoor.transform.position = Vector3.MoveTowards(connectedDoor.transform.position, doorTargetPosition, moveSpeed * Time.deltaTime);

            //버튼을 아래로 
            Vector3 buttonTargetPosition = initialButtonPosition + new Vector3(0, -downDistance, 0);
            buttonTop.position = Vector3.MoveTowards(buttonTop.position, buttonTargetPosition, moveSpeed * Time.deltaTime);

        }
        else
        {
            // 문과 버튼을 원래 위치로 천천히 복원
            connectedDoor.transform.position = Vector3.MoveTowards(connectedDoor.transform.position, initialDoorPosition, moveSpeed * Time.deltaTime);
            buttonTop.position = Vector3.MoveTowards(buttonTop.position, initialButtonPosition, moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Something entered the trigger zone");
        if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ducks")) && gameObject.CompareTag("Button"))
        {
            isPushed = true;
            onButtonPressed?.Invoke();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Object")) && gameObject.CompareTag("Button"))
        {
            isPushed = false;
            onButtonReleased?.Invoke();
        }
    }
}






