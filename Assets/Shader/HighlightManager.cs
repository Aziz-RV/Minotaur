using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightManager : MonoBehaviour
{
    private GameObject obj1;
    private GameObject obj2;

    public Material sh;
    // Start is called before the first frame update
    void Start()
    {
        obj1=GameObject.Find("Cube");
        obj2=GameObject.Find("Sp");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
          obj1.GetComponent<Renderer> ().material = sh;
          obj2.GetComponent<Renderer> ().material = sh;
        }
    }
}
