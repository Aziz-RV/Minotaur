using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// have to attach a rigid body to the gameobject to which we will attach this script
public class Interactable : MonoBehaviour
{
    // this script will be benefic in case you aim to have two pointers interacting in the scene
    public physicsPointer activePointer = null;
    public GameObject obj = null; // it represents the gameobject referring to the torch to which this Interactable is attached
}
