using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeartCounter : MonoBehaviour
{

    private int colectedHearts = 0;

    [SerializeField] private ParticleSystem colectionEffect;
    [SerializeField] private TMP_Text heartsUI;
    [SerializeField] private UIManager myUIManager;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Diamond")) return;

        AddAndUpdateHearts();
        Instantiate(colectionEffect, other.transform.position, Quaternion.identity);
        Destroy(other.gameObject);

    }

    private void AddAndUpdateHearts()
    {
        SFXManager.Instance.PlaySound("Heart_colect");
        colectedHearts++;
        heartsUI.text = colectedHearts.ToString()+ "/17";
        myUIManager.CheckAndShowPrize(colectedHearts);
    } 

 
}
