using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public int playerLvl = 1;
    public int levelToBeatGame = 10;
    
    [Header("Player Overlay")]
    public Image healthBar;
    public Image staminaBar;
    
    public PlayerStats playerStats;

    [Header("Experience")] 
    public Slider expBarSlider;
    public TextMeshProUGUI expText;
    
   
    public float maxExp = 100f;
    private float currentExp = 0f;
    
    [Header("Game End")] 
    public GameObject gameEndPanel;
    
    [Header("Game Over")]
    public GameObject restartGamePanel;
    
    private void Start()
    {
        UpdateExpBar(currentExp, maxExp);
        DeactivateGameEndPanel();
    }

    private void OnEnable()
    {
        if (playerStats != null)
        {
            playerStats.OnHealthChanged += UpdateHealthBar;
            playerStats.OnStaminaChanged += UpdateStaminaBar;
            playerStats.OnPlayerDied += ActivateRestartPanel;
        }
    }

    private void OnDisable()
    {
        if (playerStats != null)
        {
            playerStats.OnHealthChanged -= UpdateHealthBar;
            playerStats.OnStaminaChanged -= UpdateStaminaBar;
            playerStats.OnPlayerDied -= ActivateRestartPanel;
        }
    }

    private void UpdateHealthBar(float current, float max)
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = current / max;
        }
    }

    private void UpdateStaminaBar(float current, float max)
    {
        if (staminaBar != null)
        {
            staminaBar.fillAmount = current / max;
        }
    }
    
    public void GainExp(float amount)
    {
        currentExp += amount;

        while (currentExp >= maxExp)
        {
            currentExp -= maxExp;
            playerLvl++;
            maxExp *= 1.2f;

           playerStats.LevelUp();
        }
        
        UpdateExpBar(currentExp, maxExp);
    }

    private void UpdateExpBar(float current, float max)
    {
        if (expBarSlider != null)
        {
            expBarSlider.value = current / max;
        }

        if (expText != null)
        {
            expText.text = $"Lvl {playerLvl}: {currentExp} / {maxExp}";
        }

        if (playerLvl >= levelToBeatGame)
        {
            ActivateGameEndPanel();
        }
    }

    private void ActivateGameEndPanel()
    {
        gameEndPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    
    private void ActivateRestartPanel()
    {
        restartGamePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void DeactivateGameEndPanel()
    {
        Time.timeScale = 1f;
        
        gameEndPanel.SetActive(false);
        restartGamePanel.SetActive(false);
    }
    
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
