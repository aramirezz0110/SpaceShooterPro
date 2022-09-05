using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button newGameButton;
    private void Start()
    {
        newGameButton.onClick.AddListener(LoadGame);
    }
    private void LoadGame()
    {
        SceneManager.LoadScene(SceneNames.GameScene);
    }
}
