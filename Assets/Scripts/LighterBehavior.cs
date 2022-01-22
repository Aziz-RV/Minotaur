using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterBehavior : MonoBehaviour
{
    public GameObject Flamme;
    bool flammeAllumee; // false by default

    private Transform _lighterTransform;
    private GameObject _instance;
	
    // Start is called before the first frame update
	void Start() {
		_lighterTransform = transform.Find("Lighter");
	}
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (!flammeAllumee)
            {
                Vector3 hauteurFlamme = new Vector3(0, 0.18f, 0);
                Quaternion rotationFlamme = new Quaternion(-0.5f, 0, 0, 0);
                _instance = Instantiate(Flamme, _lighterTransform.position + hauteurFlamme, _lighterTransform.rotation * rotationFlamme, transform);
                flammeAllumee = true;
            }

            else
            {
                Destroy(_instance);
                flammeAllumee = false;
            }
        }
    }
}
