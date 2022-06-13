using System;
using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
   private static SceneLoader _instance;
   public static SceneLoader Instance => _instance;

   private void Awake()
   {
      if (_instance != null) return;
      _instance = this;
   }

   // loads next scene based on the scene ordering defined on Unity > build settings
   public void LoadNextScene()
   {
      int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
      EffectManager.Instance.Clear();
      SceneManager.LoadScene(currentSceneIndex + 1);
      
   }

   // loads scene by its name
   public void LoadSceneByName(string sceneName)
   {
      SceneManager.LoadScene(sceneName: sceneName);
   }

   // always the 0 indexed scene
   public void LoadStartScene()
   {
      // FindObjectOfType<GameState>().ResetState();
      SceneManager.LoadScene(0);
   }

   public void Quit()
   {
      Application.Quit();
   }

   /**
    * Hides the mouse cursor.
    */
   public void Start()
   {
      Cursor.visible = false;
   }
}
