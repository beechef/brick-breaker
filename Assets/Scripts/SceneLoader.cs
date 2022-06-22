using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Effects;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader _instance;
    public static SceneLoader Instance => _instance;

    [SerializeField] private Ball ball;
    [SerializeField] private GameObject dropItemZone;

    private void Awake()
    {
        if (_instance != null) return;
        _instance = this;
    }

    public void LoadLevel(int level)
    {
        ball.HasBallBeenShot = false;
        ball.ResetPosition();

        var dropItemZoneTransform = dropItemZone.transform;
        for (int i = 0; i < dropItemZoneTransform.childCount; i++)
        {
            Destroy(dropItemZoneTransform.GetChild(i).gameObject);
        }

        var path = Path.Combine(Application.dataPath, "Level Data", $"level_{level}.csv");

        CSV csv = CSV.ReadCSV(path);
        GameSession.Instance.StartGameSession(csv.data[0, 0], csv.data[0, 1]);
        BlockLoader.Instance.LoadBlock(csv).Forget();
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