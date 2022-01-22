using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limit : MonoBehaviour
{
    public AudioClip openingSound;
    public GameObject missingEye;
    public HingeJoint joint;
    JointLimits limits;
    Vector3 eyeSocket;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        joint = GetComponent<HingeJoint>();
        limits = joint.limits;
        eyeSocket.x = 2;
        eyeSocket.y = (float)0.3;
        eyeSocket.z = (float)4.0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(eyeSocket, missingEye.transform.position) < 1 && limits.max == 0)
        {
            source.PlayOneShot(openingSound, (float)10);
            limits.max = 45;
            joint.limits = limits;

        }
    }
}
