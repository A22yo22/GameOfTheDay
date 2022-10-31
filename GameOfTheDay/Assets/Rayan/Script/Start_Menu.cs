using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Menu : MonoBehaviour
{
    public GameObject Level_selection; //UI for Level seletion

    int Level_Num_Public; // Public int for Level_Number

    public void Start_Level() 
    {
        SceneManager.LoadScene(Level_Num_Public);
    }

    public void Level()
    {
        Level_selection.SetActive(true); // Button Method for Switch to Level selevtion UI
    }
    public void Level_Select(int Level_Num) // Button Method for Level Images
    {
        Level_Num_Public = Level_Num;
        Level_selection.SetActive(false);
    }
}
