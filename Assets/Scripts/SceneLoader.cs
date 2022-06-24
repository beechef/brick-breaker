using Cysharp.Threading.Tasks;
using Effects;
using UnityEngine;
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

        EffectManager.Instance.Clear();

        var path = $"Level/level_{level}";

        CSV csv = CSV.ReadCSV(path);
        GameSession.Instance.StartGameSession(csv.data[0, 0], csv.data[0, 1]);
        BlockLoader.Instance.LoadBlock(csv).Forget();
    }

    /**
    * Hides the mouse cursor.
    */
    public void Start()
    {
        Cursor.visible = false;
    }
}