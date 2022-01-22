using UnityEngine;
using System.Collections;

//script utilisé pour que l'orientation de la caméra suive le déplacement de la souris. Ce déplacement est désactivable en appuyant sur la touche R.

public class CameraController : MonoBehaviour
{
  private bool cameraRot;
  private float sensitivity;
  void Start()
  {
    cameraRot=true;
    Cursor.lockState = CursorLockMode.Locked;
    sensitivity = 2.0f;
  }

  void Update(){

    //Rotate Camera with Cursor
    Vector2 posCursor = new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));

    if (Input.GetKeyDown(KeyCode.Escape)){
      cameraRot = !cameraRot;
      if (cameraRot){
        Cursor.lockState = CursorLockMode.Locked;
      } else {
        Cursor.lockState = CursorLockMode.None;
      }
    }
    if (cameraRot){
      // si on veux désactiver la rotation, il suffit de désactiver cameraRot
      transform.Rotate(sensitivity*-posCursor.y,0.0f,0.0f);
      transform.parent.Rotate(0.0f,sensitivity*posCursor.x,0.0f);
    }
  }
}
