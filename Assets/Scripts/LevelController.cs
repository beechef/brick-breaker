using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void SetBlockCounter(int blocksCounter)
    {
        this.blocksCounter = blocksCounter;
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
                SceneManager.LoadScene(GAME_OVER_SCENE_NAME);
            }

            // increases game level
            MapManager.Instance.SetVictory(gameSession.GameLevel);
            gameSession.GameSpeed = 0;
               
            _sceneLoader.LoadLevel(MapManager.Instance.CurrentLevel);
        }
    }
    
}
