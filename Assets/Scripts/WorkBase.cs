using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorkBase : MonoBehaviour
{
    public bool occupied = false;
    public Transform[] flavourCircles;

    void Start()
    {
        flavourCircles = GetComponentsInChildren<Transform>();
        for (int i = 1; i < flavourCircles.Length; i++)
        {
            flavourCircles[i].gameObject.SetActive(false);
        }
    }

    public void ActivateFlavourCircle()
    {
        int flavourQty = GetComponentInChildren<IceCreamInside>().flavours.Count;
        for (int i = 1; i <= flavourQty; i++)
        {
            flavourCircles[i].gameObject.SetActive(true);
            flavourCircles[i].GetComponent<SpriteRenderer>().color = GetComponentInChildren<IceCreamInside>().flavours[i-1].iceCreamColor;
        }
    }

    public void DeactivateFlavourCircle()
    {
        int flavourQty = GetComponentInChildren<IceCreamInside>().flavours.Count;
        for (int i = 1; i < flavourCircles.Length; i++)
        {
            flavourCircles[i].gameObject.SetActive(false);
        }
    }
}
