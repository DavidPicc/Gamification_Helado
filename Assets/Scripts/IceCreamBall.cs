using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IceCreamBall : MonoBehaviour
{
    private Collider2D[] bowlColliders;
    bool ballPlaced = false;
    private void Start()
    {
        bowlColliders = GameObject.FindObjectsOfType<Bowl>().Select(bowl => bowl.GetComponent<Collider2D>()).ToArray();
    }
    private void Update()
    {
        if(!ballPlaced)
        {
            if (!GetComponent<IngredientMove>().isBeingDragged)
            {
                PlaceBall();
            }
        }
    }

    void PlaceBall()
    {
        bool droppedInBowl = false;

        foreach (Collider2D bowlCollider in bowlColliders)
        {
            if (bowlCollider != null && bowlCollider.bounds.Contains(transform.position))
            {
                if (!bowlCollider.GetComponent<Bowl>().hasLid)
                {
                    // Drop the ball inside the bowl
                    transform.position = bowlCollider.transform.position;
                    transform.SetParent(bowlCollider.transform);
                    droppedInBowl = true;
                    ballPlaced = true;
                    GetComponent<Collider2D>().enabled = false;
                    bowlCollider.GetComponent<IceCreamInside>().AddFlavour(GetComponent<Flavour>());
                    break;
                }
            }
        }

        if (!droppedInBowl)
        {
            // Destroy the ball
            Destroy(gameObject);
        }
    }
}
