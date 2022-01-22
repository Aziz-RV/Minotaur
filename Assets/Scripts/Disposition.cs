using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Indique si les objets enfants (les nb premiers) sont bien placés par rapport à l'objet qui porte ce script

public class Disposition : MonoBehaviour
{
    private List<Vector3> objectifs = new List<Vector3>();   //Positions à atteindre
    private float tolerance;  //Marge d'erreur pour le placement des objets
    public int nItemConsidered; //Pour debugger, on ne peut placer que n objets pour résoudre l'énigme
    private List<Transform> items = new List<Transform>();  //Liste des transforms des enfants de la table
    private List<bool> accomplishments = new List<bool>();  //Indique, pour chaque item, si l'objectif est atteint
    private List<bool> itemSelected = new List<bool>(); //Indique quel item est sélectionné
    [SerializeField]
    private bool goal;  //Réussite de l'énigme
    public bool getGoal(){return goal;}

    // Start is called before the first frame update

    void Start()
    {
        nItemConsidered = Mathf.Min(3, transform.childCount); // on pourra passer à 4 quand le probleme du crane qui s'enfonce a moitiée dans le sol sera réglé.
        for (int i = 0; i < nItemConsidered; i++)
        {
            //Remplissage de la liste des enfants de la table
            // 1.Vase 2.Sword 3.Discobole 4.Skull
            items.Add(transform.GetChild(i));
            accomplishments.Add(false);
            itemSelected.Add(false);
        }
        objectifs.Add(new Vector3(0.3f, 0.7f, -0.2f)); //vase
        objectifs.Add(new Vector3(-0.1f, 0.7f, 0f)); //sword
        objectifs.Add(new Vector3(0.1f, 0.7f, 0.2f)); //discobole
        objectifs.Add(new Vector3(-0.4f, 0.7f, 0.1f)); //skull
        goal = false;
        tolerance = 0.08f*nItemConsidered;
    }

    // Update is called once per frame
    void Update()
    {
        if (!goal){
            bool goalStatus = true;
            for (int i = 0; i < nItemConsidered; i++)
            {
                //On vérifie, pour chaque objet, s'il est proche de la position visée à la tolérance près
                accomplishments[i] = (Vector3.Distance(items[i].localPosition,objectifs[i]) < tolerance);
                if (itemSelected[i]) {Debug.Log(items[i].localPosition + " : " + (items[i].localPosition - objectifs[i]).magnitude);}
                if (!accomplishments[i])
                {
                    goalStatus = false;
                }
            }
            goal = goalStatus;

            MoveObjectsOnTable();

            if (goal){
                Debug.Log("énigme table réussie !");
            }
        }
    }


    void MoveObjectsOnTable(){
        if(Input.GetMouseButton(0)){
            bool itemAlreadySelected = false;
            //On détermine si un objet est déjà sélectionné ou non
            for(int i = 0; i < nItemConsidered; i++){
                itemAlreadySelected = itemAlreadySelected || itemSelected[i];
            }


            for(int i = 0; i < nItemConsidered; i++){
                //Si le raycast renvoie un objet et qu'aucun autre n'est sélectionné alors on sélectionne cet item
                if (RayTracing.GetObject(items[i].gameObject.tag) != null && !itemAlreadySelected)
                {
                    itemSelected[i] = true;
                }
            }
        }else{
            //Lorsque la souris est relachée on déselectionne tous les objets
            for (int i = 0; i < nItemConsidered; i++)
            {
                itemSelected[i] = false;
            }
        }


        Vector3 newPosition = RayTracing.GetHitPosition("table");

        if((newPosition-Vector3.zero).magnitude > 0){
            //On est bien sur la table
            for (int i = 0; i < nItemConsidered; i++)
            {
                if (itemSelected[i])
                {
                    items[i].position = newPosition;
                }
            }
        }
    }
}
