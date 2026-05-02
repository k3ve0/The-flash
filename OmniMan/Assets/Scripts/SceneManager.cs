using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

public class SceneManagerUnity : MonoBehaviour
{
 public void LoadflashGame()
    {
        SceneManager.LoadScene("FlashGame");

    }
     public void LoadMainMenu()
 {
    SceneManager.LoadScene("MainMenu");
 }
    }

