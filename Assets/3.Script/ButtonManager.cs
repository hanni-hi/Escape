using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
   // private Button[] buttons;
   //
   // private void Start()
   // {
   //     // �ʿ� �ִ� ��� Button ��ũ��Ʈ�� ã��
   //     buttons = FindObjectsOfType<Button>();
   //
   //     // �� ��ư�� ���� �̺�Ʈ�� ���
   //     foreach (Button button in buttons)
   //     {
   //         button.onButtonPressed.AddListener(() => OpenDoorAndPressButton(button));
   //         button.onButtonReleased.AddListener(() => CloseDoorAndResetButton(button));
   //     }
   // }
   //
   //void OpenDoorAndPressButton(Button button)
   // {
   //     // ���� ���� �̵�
   //     Vector3 doorTargetPosition = button.initialDoorPosition + new Vector3(0, button.upDistance, 0);
   //     StartCoroutine(MoveDoor(button.connectedDoor, doorTargetPosition, button.moveSpeed));
   //
   // }
   // void CloseDoorAndResetButton(Button button)
   // {
   //     // ���� ���� ��ġ�� ����
   //     StartCoroutine(MoveDoor(button.connectedDoor, button.initialDoorPosition, button.moveSpeed));
   // }
   //
   // private IEnumerator MoveDoor(GameObject door, Vector3 targetPosition, float speed)
   // {
   //     while (door.transform.position != targetPosition)
   //     {
   //         door.transform.position = Vector3.MoveTowards(door.transform.position, targetPosition, speed * Time.deltaTime);
   //         yield return null; // ���� �������� ��ٸ�
   //     }
   // }
}
