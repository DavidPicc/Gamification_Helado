using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IceCreamFlavour
{
    Chocolate,
    Mint,
    Vanilla,
    Lucuma,
    Strawberry
}

public class Flavour : MonoBehaviour
{
    public IceCreamFlavour iceCreamFlavour;
    public Color iceCreamColor;
}
