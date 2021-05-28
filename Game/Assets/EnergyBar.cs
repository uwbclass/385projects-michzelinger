using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider bar;
    // Start is called before the first frame update
    public void UpdateEnergy(float value)
    {
        bar.value = value;
    }

    public void UpdateMaxEnergy(float maxValue)
    {
        bar.maxValue = maxValue;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
