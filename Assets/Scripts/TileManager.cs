using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TileManager : MonoBehaviour
{
    public int tile_count = 1;
    public GameObject tile_prefab;
    Vector3 camLocation;
    public float orthoSize;
    public GameObject timer;
    int level;
    int activeTile;
    private bool completed = false;

    private void Awake()
    {
        if (GameObject.FindWithTag("GameManager") != null)
        {
            level = GameObject.FindWithTag("GameManager").GetComponent<MainMenu>().selectedGridSize;
            tile_count = level;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        GenerateTitles();

        camLocation = new Vector3((tile_count-1) * 0.525f, (tile_count-1) * -0.525f, -1.0f);

        Camera.main.transform.position = camLocation;

        orthoSize = (tile_count * (gameObject.GetComponentInChildren<SpriteRenderer>().bounds.size.x + 0.12f)) * Screen.height / Screen.width * 0.5f;

        Camera.main.orthographicSize = orthoSize;

        //Debug.Log("Child Count: " + transform.childCount.ToString());

        //If the game is a 1x1, make sure not to change the colour of the tile and automatically win the game
        if (transform.childCount != 1)
        {
            //Pre-Click on n number of titles when the game starts
            //Where n is the number of tiles in the scene
            for (int i = 0; i < transform.childCount; i++)
            {
                //Random number between 0 and the occurrences of tiles in the game minus 1 (used for the index of a random tile)
                activeTile = Random.Range(0, transform.childCount);

                //Click on the random tile and change the colour of the surrounding tiles, including itself
                transform.GetChild(activeTile).GetComponent<Tile>().ClickedTitle();
            }
        }
    }

    private void GenerateTitles ()
    {
        for (int i = 0; i < tile_count; i++)
        {
            for (int j = 0; j < tile_count; j++)
            {
                GameObject tile = Instantiate(tile_prefab, transform);

                float posX = i * 1.05f;
                float posY = j * -1.05f;

                tile.transform.position = new Vector2(posX, posY);
                SetTileColor(tile);
                
            }
        }
    }

    private void SetTileColor (GameObject tile)
    {
        tile.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    public Text text;
    public void CompletionCheck()
    {
        bool complete = true;
        foreach (Transform tile in transform)
        {
            if (tile.GetComponent<SpriteRenderer>().color == Color.yellow)
            {
                complete = false;
            }
        }
        if (complete == true && timer.GetComponent<Timer>().timeRemaining > 0)
        {
            completed = true;
            //If the user completes a higher level, save itx
            if (level > SaveSystem.LoadPlayer().level)
            {
                SavePlayer();
            }
            text.text = "You Won!";
            //Pauses the timer when the user wins the game
            timer.GetComponent<Timer>().StopTimer(true);

        } else if (completed == false && timer.GetComponent<Timer>().timeRemaining > 0)
        {
            text.text = "";
        }
    }

    //Change the scene from the game to the menu
    public void EndGame()
    {
        Destroy(GameObject.FindWithTag("GameManager"));
        //SceneManager.LoadScene("Menu");
        GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>().LoadPreviousLevel();
    }

    //Resets the positon and orthographic size of the camera
    public void ResetZoom()
    {
        Camera.main.transform.position = camLocation;
        Camera.main.orthographicSize = orthoSize;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(level);
    }
}
