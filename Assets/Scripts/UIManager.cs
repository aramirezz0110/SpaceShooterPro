using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private GameObject gameOverMessages;
    [SerializeField] private Image livesDisplayImage;
    [SerializeField] private Sprite[] liveSprites;
    void Start()
    {
        scoreText.text = "Score: 0";
        gameOverMessages.SetActive(false);
    }
    public void UpdateScore(int value)
    {
        scoreText.text = "Score: " + value;
    }
    public void UpdateLives(int value)
    {
        if (value < liveSprites.Length && value>=0)
            livesDisplayImage.sprite = liveSprites[value];

        if (value == 0)
        {
            gameOverMessages.SetActive(true);
            StartCoroutine(GameOverFlickerRoutine());
        }
            
    }
    private IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(.5f);
            gameOverText.text = "";
            yield return new WaitForSeconds(.5f);
        }
    }
}
