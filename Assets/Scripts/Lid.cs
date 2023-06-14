using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Lid : MonoBehaviour
{
    private Collider2D[] workSpaceColliders;
    bool placedLid = false;
    private void Start()
    {
        workSpaceColliders = GameObject.FindObjectsOfType<Bowl>().Select(bowl => bowl.GetComponent<Collider2D>()).ToArray();
    }
    private void Update()
    {
        if (!placedLid)
        {
            if (!GetComponent<IngredientMove>().isBeingDragged)
            {
                PlaceLid();
            }
        }
    }

    void PlaceLid()
    {
        bool droppedInWorkSpace = false;

        foreach (Collider2D workCollider in workSpaceColliders)
        {
            if (workCollider != null && workCollider.bounds.Contains(transform.position))
            {
                // Drop the ball inside the bowl
                transform.position = workCollider.transform.position;
                transform.SetParent(workCollider.transform);
                droppedInWorkSpace = true;
                placedLid = true;
                GetComponent<Collider2D>().enabled = false;
                workCollider.GetComponent<Bowl>().hasLid = true;
                break;
            }
        }

        if (!droppedInWorkSpace)
        {
            // Destroy the ball
            Destroy(gameObject);
        }
    }
}
