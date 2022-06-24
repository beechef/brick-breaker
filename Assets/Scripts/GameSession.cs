using TMPro;
using UI.LevelMap;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private static readonly int MAXLives = 5;
    // config
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI gameLevelText;
    [SerializeField] private TextMeshProUGUI playerLivesText;

    // state
    private static GameSession _instance;
    public static GameSession Instance => _instance;

    public int GameLevel => MapManager.Instance.CurrentLevel;
    public int PlayerScore { get; set; }
    public int PlayerLives { get; set; }
    public int PlayerMaxLives { get; private set; }
    public float GameSpeed { get; set; }

    /**
     * Singleton implementation.
     */
    private void Awake()
    {
        // this is not the first instance so destroy it!
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // first instance should be kept and do NOT destroy it on load
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /**
     * Before first frame.
     */
    void Start()
    {
        playerScoreText.text = PlayerScore.ToString();
        gameLevelText.text = GameLevel.ToString();
        playerLivesText.text = PlayerLives.ToString();
    }

    /**
     * Update per-frame.
     */
    void Update()
    {
        Time.timeScale = GameSpeed;

        // UI updates
        playerScoreText.text = PlayerScore.ToString();
        gameLevelText.text = GameLevel.ToString();
        playerLivesText.text = PlayerLives.ToString();
    }

    /**
     * Updates player score with given points and also updates the UI score. The total points that are
     * calculated is based on the basis value (this.PointsPerBlock).
     */
    public void AddToPlayerScore(int blockMaxHits)
    {
        PlayerScore += blockMaxHits;
        playerScoreText.text = PlayerScore.ToString();
    }
    
    public void StartGameSession(int playerLives, float gameSpeed)
    {
        PlayerMaxLives = MAXLives;
        PlayerScore = 0;
        PlayerLives = playerLives;
        GameSpeed = gameSpeed;
    }
}