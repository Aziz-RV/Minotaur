using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enigmeManager : MonoBehaviour
{
    public GameObject table; // pour avoir référence au script énigme Disposition
    public GameObject torch; // pour avoir référence au script énigme TorchInteraction
    public GameObject diamondEye; // pour avoir référence au script énigme MissingEye

    public GameObject doorStatue; // pour pouvoir l'ouvrir
    public GameObject wallSkull; // pour pouvoir le détruire
    public GameObject particles; // pour quand wallSkull explose
    public GameObject finalDoor; // pour sortir quand on a fini toutes les énigmes :

    private bool isTable = false; //enigme de la table
    private bool isTorch = false; //énigme lumières
    private bool isSkull = false; //énigme crâne

    void Update()
    {
        updateEnigmas();
        if (isTorch && doorStatue.activeSelf)
        {
            // énigme de la table réussie on ouvre la porte de la statue
            // on ouvre la porte de la statue
            doorStatue.GetComponent<Animator>().SetTrigger("DoorATrigger");
        }
        if (isTable && wallSkull.activeSelf)
        {
            // énigme des statues réussie, on détruit wallSkull
            wallSkull.SetActive(false);
            particles.SetActive(true);
        }
        if (isTable && isTorch && isSkull){
            // les trois énigmes ont été réussies !
            // on ouvre la porte finale
            finalDoor.GetComponent<Animator>().SetTrigger("DoorATrigger");
        }
    }

    private void updateEnigmas(){
        isTable = table.GetComponent<Disposition>().getGoal();
        isTorch = torch.GetComponent<TorchInteraction>().getGoal();
        isSkull = diamondEye.GetComponent<MissingEye>().getGoal();
    }
}
