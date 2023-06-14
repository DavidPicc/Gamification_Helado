using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamInside : MonoBehaviour
{
    public List<Flavour> flavours = new List<Flavour>();
    public List<IceCreamFlavour> flavours_ = new List<IceCreamFlavour>();
    public void AddFlavour(Flavour flavour)
    {
        if(!flavours_.Contains(flavour.iceCreamFlavour))
        {
            flavours.Add(flavour);
            flavours_.Add(flavour.iceCreamFlavour);
            GetComponent<Bowl>().space.GetComponent<WorkBase>().ActivateFlavourCircle();
        }

    }
}
