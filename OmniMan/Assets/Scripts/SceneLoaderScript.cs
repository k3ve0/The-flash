using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

public class SceneLoaderScript : MonoBehaviour
{
 public void LoadflashGame()
    {
        SceneManager.LoadScene("FlashGame");

    }
     public void LoadMainMenu()
 {
    SceneManager.LoadScene("MainMenu");
 }

 public void LoadCredits()
 {
   SceneManager.LoadScene("Credits");
 }
    }

