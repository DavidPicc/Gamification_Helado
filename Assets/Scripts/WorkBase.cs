using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorkBase : MonoBehaviour
{
    public bool occupied = false;
    public GameObject bowlShadow;
    public Transform[] flavourCircles;

    void Start()
    {
        //flavourCircles = GetComponentsInChildren<Transform>();
        //for (int i = 1; i < flavourCircles.Length; i++)
        //{
        //    flavourCircles[i].gameObject.SetActive(false);
        //}

        Transform[] children = GetComponentsInChildren<Transform>();

        // Iterate through the child objects
        foreach (Transform child in children)
        {
            // Check if the child has the desired tag
            if (child.gameObject.CompareTag("FlavourDot"))
            {
                // Add the child to the array
                AddChildToArray(child);
            }
        }
        DeactivateFlavourCircle();
        bowlShadow.SetActive(true);
    }

    public void ActivateFlavourCircle()
    {
        int flavourQty = GetComponentInChildren<IceCreamInside>().flavours.Count;
        for (int i = 0; i < flavourQty; i++)
        {
            flavourCircles[i].gameObject.SetActive(true);
            flavourCircles[i].GetComponent<SpriteRenderer>().color = GetComponentInChildren<IceCreamInside>().flavours[i].iceCreamColor;
        }
    }

    public void DeactivateFlavourCircle()
    {
        //int flavourQty = GetComponentInChildren<IceCreamInside>().flavours.Count;
        for (int i = 0; i < flavourCircles.Length; i++)
        {
            flavourCircles[i].gameObject.SetActive(false);
        }
        bowlShadow.SetActive(true);
    }

    private void AddChildToArray(Transform child)
    {
        // Resize the array to accommodate the new child
        System.Array.Resize(ref flavourCircles, flavourCircles.Length + 1);

        // Add the child to the last index of the array
        flavourCircles[flavourCircles.Length - 1] = child;
    }
}
