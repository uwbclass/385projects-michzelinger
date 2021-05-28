using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider bar;
    // Start is called before the first frame update
    private void UpdateEnergy()
    {
        bar.value = HeroBehavior.instance.energy;
    }

    private void UpdateMaxEnergy()
    {
        bar.maxValue = HeroBehavior.MaxEnergy;
    }
    void Start()
    {
        UpdateMaxEnergy();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnergy();
    }
}
