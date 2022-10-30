// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameStatusText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_Text subText;

    public void GetReady(int waveNumber)
    {
        SetText("Get ready!", "Wave " + waveNumber);
    }

    public void WaveDefeated(int waveNumber)
    {
        SetText("Wave " + waveNumber + " cleared");
    }
    
    public void FinalBoss()
    {
        SetText("THIS IS THE FINAL BOSS", "Hope you are prepared");
    }

    public void Win()
    {
        SetText("You won!","Those slimes were innocent and you slaughtered them");
        
        Invoke("BacktoMenu", 5.0f);
    }
    
    public void Lose()
    {
        SetText("You died!","L + Bozo + Ratio + Cancelled + You suck doggy doo doo");

        Invoke("BacktoMenu", 5.0f);
    }

    public void AttackSpeedUp()
    {
        SetText("Attack Speed Up!");
        Invoke("Clear", 2.0f);

    }

    public void SpeedUp()
    {
        SetText("Speed Up!");
        Invoke("Clear", 2.0f);

    }

    public void DamageUp()
    {
        SetText("Damage Up!");
        Invoke("Clear", 2.0f);

    }

    public void Clear()
    {
        SetText("");
    }

    private void SetText(string text, string subText = "")
    {
        this.text.SetText(text);
        this.subText.SetText(subText);
    }
    private void BacktoMenu () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex * 0);
    }
}
