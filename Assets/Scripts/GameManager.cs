using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerInput PlayerInputInstance;
    public InputAction Reset;
    public int Score;
    public int Lives;
    public TMP_Text ScoreText;
    public TMP_Text LivesText;


    // Start is called before the first frame update
    void Start()
    {
        //Setting up the input system
        PlayerInputInstance = GetComponent<PlayerInput>();
        PlayerInputInstance.currentActionMap.Enable();

        Reset = PlayerInputInstance.currentActionMap.FindAction("Reset"); //Setting the controls to detect (R)


        //Setting inputs
        Reset.started += Reset_started;
    }

    private void Reset_started(InputAction.CallbackContext obj) //Resets the scene on R input
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScore() //Gives 100 points and displays it
    {
        Score += 100;
        ScoreText.text = "Score:\n" + Score.ToString();
    }

    public void UpdateLives()
    {
        if (Lives == 1) //If lives equal 0 (has to be 1 because that's the final hit), reset the game. Otherwise, lose a life; displays it
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else
        {
            Lives -= 1;
            LivesText.text = "Lives:\n" + Lives.ToString();
        }
    }


    public void OnDestroy()
    {
        //Resetting inputs
        Reset.started -= Reset_started;
    }
}
