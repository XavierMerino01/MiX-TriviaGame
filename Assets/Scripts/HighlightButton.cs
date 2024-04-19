using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HighlightButton : MonoBehaviour
{

    [SerializeField] private int materialIndex;

    public void ActivateHighlight()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material material = meshRenderer.materials[materialIndex];
        material.EnableKeyword("_EMISSION");
    }

    public void DeactivateHighlight()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material material = meshRenderer.materials[materialIndex];
        material.DisableKeyword("_EMISSION");
    }
}
