using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
   public Slider Slider;
   public Vector3 Offset;

   public void SetHealth(float health, float maxHealth)
   {
      Slider.gameObject.SetActive(health < maxHealth);
      Slider.value = health;
      Slider.maxValue = maxHealth;
      Slider.value -= 0.1f;
   }

   // Update is called once per frame
   void FixedUpdate()
   {
      Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
   }
}
