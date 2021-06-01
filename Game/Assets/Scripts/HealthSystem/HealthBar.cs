using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
   public Slider Slider;
   public Vector3 Offset;

   public Color Low;
   public Color High;

   public void SetHealth(float health, float maxHealth)
   {
      Slider.gameObject.SetActive(health < maxHealth);
      Slider.maxValue = maxHealth;
      Slider.value = health;
      
      Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, Slider.normalizedValue);
   }

   // Update is called once per frame
   void FixedUpdate()
   {
      Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
   }
}
