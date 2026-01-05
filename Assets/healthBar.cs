using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float lerpSpeed = 5f;

    public void SetMaxHealth(float maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        easeHealthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        easeHealthSlider.value = maxHealth;
    }

    public void SetHealth(float health)
    {
        healthSlider.value = health;
    }

    void Update()
    {
        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(
                easeHealthSlider.value,
                healthSlider.value,
                Time.deltaTime * lerpSpeed
            );
        }
    }
}
