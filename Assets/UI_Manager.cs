using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Image healthBar;
    public Image staminaBar;
    public Image manaBar;
    
    private Slider expBarSlider;
    public PlayerStats playerStats;

    private void Update()
    {
        if (playerStats == null) return;
        healthBar.fillAmount = playerStats.GetHealthRatio();
    }
}
