using System;
using System.Collections;
using System.Collections.Generic;
using UI.LevelMap;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private readonly string GAME_OVER_SCENE_NAME = "Scenes/GameOver";
    private int MapCount => MapManager.Instance.MapCount;
    
    // UI elements
    [SerializeField] int blocksCounter;

    // state
    private SceneLoader _sceneLoader;
    
    private void Start()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void IncrementBlocksCounter()
    {
        blocksCounter++;
    }
    
    public void DecrementBlocksCounter()
    {
        blocksCounter--;

        if (blocksCounter <= 0)
        {
            var gameSession = GameSession.Instance;
            
            // check for game over
            if (gameSession.GameLevel >= MapCount)
            {
                _sceneLoader.LoadSceneByName(GAME_OVER_SCENE_NAME);
            }

            // increases game level
            MapManager.Instance.SetVictory(gameSession.GameLevel);
            // gameSession.GameLevel++;
            gameSession.StartGameSession();
            
            _sceneLoader.LoadNextScene();
        }
    }
    
}
