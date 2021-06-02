using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PowerupDisplay : MonoBehaviour
{
    public Image speedPowerup;
    public Image shieldPowerup;
    public Image bombPowerup;
    [SerializeField] TextMeshProUGUI m_bombText;

    public void SpeedDisplay(bool flag)
    {
        speedPowerup.gameObject.SetActive(flag);
    }

    public void ShieldDisplay(bool flag)
    {
        shieldPowerup.gameObject.SetActive(flag);
    }

    public void BombDisplay(int bombNumber)
    {
        bombPowerup.gameObject.SetActive(true);
        m_bombText.text = "X " + bombNumber;
    }

    public void UpdateBombNumber(int bombNumber)
    {
        m_bombText.text = "X " + bombNumber;
    }

    public void BombHide()
    {
        bombPowerup.gameObject.SetActive(false);
    }
}
