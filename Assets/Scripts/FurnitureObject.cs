using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureObject : Interactable
{
    public FurnitureObject() : base(Type.PickUp)
    {

    }

    public enum TypeOfObject
    {
        None,
        Table,
        Sofa,
        Picture
    }
    public TypeOfObject typeOfFurniture;
}
