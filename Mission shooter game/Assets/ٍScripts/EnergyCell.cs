using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyCell : MonoBehaviour
{
    [Header("EnergyCell")]
    public GameObject energyCell_batary;

    public Text energyText;

    public static EnergyCell occurrence;

    private int activeSpikeCount = 0;  // Aktif spike sayısını tutacak sayaç

    private void Awake()
    {
        occurrence = this;
    }

    void Start()
    {
        CheckActiveSpikes();
    }

    void Update()
    {
        CheckActiveSpikes();
    }

    private void CheckActiveSpikes()
    {
        if (!energyCell_batary.activeSelf)
            UpdateAmmoText(activeSpikeCount);

    }

    public void UpdateAmmoText(int count)
    {
        energyText.text = "Energy cell taken";
    }
}

