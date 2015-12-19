using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Purpose: demonstrate script interface to interact with terrain trees

// Steps: Attach this to main(parent/top) Player gameObject, or adjust myTransform to your hierarchy,
// define Inspector values for harvestTreeDistance, respawnTimer
// Setup a prefab tree with CAPSULE collider, how to at bottom of
// http://docs.unity3d.com/Documentation/Components/terrain-Trees.html
// paint terrain tree 

// Assign the non-collider prefab version of the tree to felledTree
// press Play, left click on terrain tree

// Harvested terrain tree info is passed to a manager object QM_ResourceManager for respawn management,
// you'll need that too or you could comment out any functionality related to manager 

// Note: this is not a demo of modifying terrainData permanently - there's enough risk involved with that
// such that you shouldn't try it unless you know what you are doing.


public class raycaster : MonoBehaviour
{

    // Player, Range
    public int harvestTreeDistance;        // Set [Inspector] min. distance from Player to Tree for your scale?
    public bool rotatePlayer = true;    // Should we rotate the player to face the Tree? 
    private Transform myTransform;        // Player transform for cache

    // Terrains, Hit
    private Terrain terrain;            // Derived from hit...GetComponent<Terrain>
    private RaycastHit hit;                // For hit. methods
    private string lastTerrain;            // To avoid reassignment/GetComponent on every Terrain click

    // Tree, GameManager
    public GameObject felledTree;    
    public float respawnTimer;            // Duration of terrain tree respawn timer

    void Start()
    {

        if (harvestTreeDistance <= 0)
        {
            Debug.Log("harvestTreeDistance unset in Inspector, using value: 6");
            harvestTreeDistance = 6;
        }

        myTransform = transform;
        lastTerrain = null;

    }


    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 30f))
            {
                if (CheckProximity())
                {
                    if (hit.collider.gameObject.tag == "Tree")
                    {
                        if (CheckProximity())
                        {
                            HarvestWood();
                        }
                    }
                    else if (hit.collider.gameObject.tag == "Rock")
                    {
                        if (CheckProximity())
                        {
                            HarvestRock();
                        }
                    }
                }
            }
        }
    }

    private bool CheckProximity()
    {
        bool inRange = true;
        float clickDist = Vector3.Distance(myTransform.position, hit.point);

        if (clickDist > harvestTreeDistance)
        {
            Debug.Log("Out of Range");
            inRange = false;
        }

        return inRange;
    }

    private void HarvestRock()
    {
        Debug.Log("Harvested rock.");
    }

    private void HarvestWood()
    {
        Debug.Log("Harvested wood.");
    }
}