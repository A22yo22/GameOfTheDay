using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    private GameObject Win_UI;
    private GameObject Player_Cam;
    private GameObject Win_Cam;


    private void Start()
    {
        Win_UI = GameObject.FindGameObjectWithTag("Win_UI");
        Player_Cam = GameObject.FindGameObjectWithTag("Player_Cam");
        Win_Cam = GameObject.FindGameObjectWithTag("Win_Cam");

        Win_UI.SetActive(false);
        Win_Cam.SetActive(false);
        Player_Cam.SetActive(true);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Win") // Query if player has won the game
        {
            Player_Cam.SetActive(false);
            Win_Cam.SetActive(true);
            Win_UI.SetActive(true);
        }
    }

    public void Win_Close(int Next_Scene) //Switch scene method for Button
    {
        SceneManager.LoadScene(Next_Scene);
    }
}