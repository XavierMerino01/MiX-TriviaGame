using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureObject : MonoBehaviour
{
    public enum TypeOfObject
    {
        None,
        Book,
        Pillow,
        Picture
    }
    public TypeOfObject typeOfObject;

    public int indexOfObject;
}
