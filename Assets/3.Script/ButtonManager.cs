using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
   // private Button[] buttons;
   //
   // private void Start()
   // {
   //     // 맵에 있는 모든 Button 스크립트를 찾음
   //     buttons = FindObjectsOfType<Button>();
   //
   //     // 각 버튼에 대해 이벤트를 등록
   //     foreach (Button button in buttons)
   //     {
   //         button.onButtonPressed.AddListener(() => OpenDoorAndPressButton(button));
   //         button.onButtonReleased.AddListener(() => CloseDoorAndResetButton(button));
   //     }
   // }
   //
   //void OpenDoorAndPressButton(Button button)
   // {
   //     // 문을 위로 이동
   //     Vector3 doorTargetPosition = button.initialDoorPosition + new Vector3(0, button.upDistance, 0);
   //     StartCoroutine(MoveDoor(button.connectedDoor, doorTargetPosition, button.moveSpeed));
   //
   // }
   // void CloseDoorAndResetButton(Button button)
   // {
   //     // 문을 원래 위치로 복원
   //     StartCoroutine(MoveDoor(button.connectedDoor, button.initialDoorPosition, button.moveSpeed));
   // }
   //
   // private IEnumerator MoveDoor(GameObject door, Vector3 targetPosition, float speed)
   // {
   //     while (door.transform.position != targetPosition)
   //     {
   //         door.transform.position = Vector3.MoveTowards(door.transform.position, targetPosition, speed * Time.deltaTime);
   //         yield return null; // 다음 프레임을 기다림
   //     }
   // }
}
