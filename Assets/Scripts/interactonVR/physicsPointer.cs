using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class physicsPointer : MonoBehaviour
{
    public float defaultLength = 5.0f;
    public GameObject Dot; //this gameobject will collide with the object we aim to select (is trigger)

    public SteamVR_Input_Sources targetSource; //which controller we will use (left or right)
    public SteamVR_Action_Boolean clickAction; // to link the action on the controller to the script

    public Material shader; // to apply a highlighter shader

    private LineRenderer lineRenderer = null;
    private List<Interactable> interactables = new List<Interactable>();
    public Interactable currentInteractable = null; // this interactable will be called in other scripts 
    private FixedJoint joint = null;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        UpdateLine();
        // Handle press
        if (clickAction.GetStateDown(targetSource))
        {
          Debug.Log("clicked");
          Select();

        }


        // Handle release
        if (clickAction.GetStateUp(targetSource))
        {
          Debug.Log("released");
          Release();
        }

    }

    private void UpdateLine()
    {

        float targetLength = defaultLength;

        // Set endPosition
        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        RaycastHit hit = CreateRaycast(targetLength);

        if (hit.collider != null)
        {
            endPosition = hit.point;
        }

        Dot.transform.position = endPosition;

        Debug.Log(transform.position);

        // Set lineRenderer
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endPosition);
    }

    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, defaultLength);

        return hit;
    }

    private void Select()
    {
        currentInteractable = getNearestInteractable();
        // if null
        if (!currentInteractable)
            return;
        // check if selected to release
        if (currentInteractable.activePointer)
            currentInteractable.activePointer.Release();
        // else set active pointer
        currentInteractable.activePointer = this;

        //highlight the selected object
        currentInteractable.gameObject.GetComponent<Renderer>().material = shader;

        // in order to pick up the selected object
        // position
        /*currentInteractable.transform.position = Dot.transform.position;
        //Attach
        Rigidbody targetbody = currentInteractable.GetComponent<Rigidbody>();
        joint.connectedBody = targetbody;*/


    }
    private void Release()
    {
        // if null
        if (!currentInteractable)
            return;
        // reset the highlighter

        // in order to drop the selected gameobject
        //apply the velocity
        /*Rigidbody targetbody = currentInteractable.GetComponent<Rigidbody>();
        //targetbody.velocity = ???.GetVelocity();

        //detach
        joint.connectedBody = null;*/

        // Clear
        currentInteractable.activePointer = null;
        currentInteractable = null;

    }

    // these two functions we think they have to be applied to the Dot gameobject even now it works well while applied to the ray !!!!!!!!
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactabe"))
            return;

        interactables.Add(other.gameObject.GetComponent<Interactable>());

    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactabe"))
            return;

        interactables.Remove(other.gameObject.GetComponent<Interactable>());
    }
    private Interactable getNearestInteractable()
    {
        Interactable nearest = null;
        float minDistance =float.MaxValue;
        float distance = 0.0f;
        foreach (Interactable inter in interactables)
        {
            distance = (inter.transform.position - Dot.transform.position).sqrMagnitude;
            if (distance < minDistance)
            {
                minDistance= distance;
                nearest = inter;
            }
        }
        return nearest;
    }
}
