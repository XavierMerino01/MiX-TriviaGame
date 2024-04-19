using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum Type
    {
        None,
        PickUp,
        Button
    }
    public Type objectType;
}
