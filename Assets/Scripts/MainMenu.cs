using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Slider mainSlider;
    public int maxGridSize;
    public Text gridSizeText;
    public int selectedGridSize;
    public Text highscore;

    void Awake()
    {
        //Call LoadPlayer as soon as possible to populate the highscore text
        LoadPlayer();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Maximum size of the grid reduced by 1 since because of adding 1 to the grid size (*)
        maxGridSize -= 1;
    }

    // Update is called once per frame
    void Update()
    {
        selectedGridSize = Mathf.RoundToInt((mainSlider.value * maxGridSize) + 1);
        //Add 1 to the grid size so that the minimum value is 1 (*)
        gridSizeText.text = "Grid Size: " + selectedGridSize.ToString();
    }

    //Change the scene from the menu to the game
    public void StartGame()
    {
        //SceneManager.LoadScene("Game");
        GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>().LoadNextLevel();
    }

    //Load player data from the binary file
    public void LoadPlayer()
    {      
        PlayerData data = SaveSystem.LoadPlayer();
        highscore.text = "Highscore: " + data.level;
    }

    //Saves intial values to the binary file and reloads the highscore text
    public void ResetPlayer()
    {
        SaveSystem.SavePlayer(0);
        LoadPlayer();
    }
}
