using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PowerupDisplay : MonoBehaviour
{
    public Image speedPowerup;

    public void SpeedDisplay(bool flag)
    {
        speedPowerup.gameObject.SetActive(flag);
    }
}
