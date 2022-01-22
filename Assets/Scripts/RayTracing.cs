using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTracing : MonoBehaviour
{
    [SerializeField] private GameObject pointer;
    static LayerMask mask;



    public static GameObject GetObject(string tag){
        //Retourne l'objet avec le tag correspondant lors du click gauche
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        mask = LayerMask.GetMask("Interactable"); // pour detecter que les objets dans le layer "interactable"
        if(Physics.Raycast(ray, out hit, 100.0f ,mask)){
            if (hit.collider.gameObject.tag == tag)
            {
                return hit.transform.gameObject;
            }
            else
            {
                return null;
            }
        }
        return null;
    }



    public static Vector3 GetHitPosition(string tag){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == tag)
            {
                return hit.point;
            }
        }
        return Vector3.zero;
    }

    private Vector3 GetPointerPosition(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return new Vector3(0,-20,0); //Position où on ne peut pas le voir
    }


    void Update(){
        //Affichage du pointer
        pointer.transform.position = GetPointerPosition();
    }


    void OnDrawGizmos()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(transform.position, direction);
    }
}
