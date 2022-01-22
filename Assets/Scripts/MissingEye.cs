using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ce script est attaché à l'oeil en diamant
public class MissingEye : MonoBehaviour
{

    private bool magnet = false;
    private bool goal = false;
    public bool getGoal(){return goal;}  //Permet de gérer la réussite de l'énigme

    private GameObject skull; //parent
    private GameObject jaw; //pour lancer l'animation
    private HingeJoint jawJoint;
    private JointLimits jawLimits;
    public AudioClip openingSound;
    private AudioSource source;

    private Vector3 eyeSocket = new Vector3(2f, 2f, 1.4f);  //Position souhaitée de l'oeil (repérée dans la scène, à modifier potentiellement)
    private float threshold = 1f;
    private float armLength = 2f; // correspond a la distance a laquelle on manipule le diamant

    void Start(){
        skull = transform.parent.gameObject;
        jaw = getChildGameObject(skull.transform, "BullJaw").gameObject;

        source = jaw.GetComponent<AudioSource>();
        jawJoint = jaw.GetComponent<HingeJoint>();
        jawLimits = jawJoint.limits;
    }

    void Update()
    {
        if (!goal){
            if (magnet)
            {
                transform.SetParent(skull.transform);
                transform.localRotation = Quaternion.Euler(45, 0, 0);
                transform.localPosition = eyeSocket;
                GetComponent<Rigidbody>().useGravity = false;

                source.PlayOneShot(openingSound, (float)10); // on joue un son
                jawLimits.max = 45; //ouverture de la bouche
                jawJoint.limits = jawLimits;

                goal = true;    //Permet de gérer la réussite de l'énigme
            }

            if (Vector3.Distance(eyeSocket,transform.position)<threshold)
            {
                magnet = true;
            }

            MoveEye();
        }
    }

    void MoveEye(){
        if (Input.GetMouseButtonDown(0) && RayTracing.GetObject(gameObject.tag) == gameObject){
            // si le diamant est sélectionné on l'attache a la caméra.
            GetComponent<Rigidbody>().useGravity = false;
            transform.SetParent(Camera.main.transform);
        }
        if (Input.GetMouseButtonUp(0)){
            transform.SetParent(skull.transform); // il retrouve son père d'origine
            GetComponent<Rigidbody>().useGravity = true;
        }

    }

    static private Transform getChildGameObject(Transform tr, string withName)
    {
        //Author: Isaac Dart, June-13.
        Transform[] ts = tr.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t;
        return null;
    }
}
