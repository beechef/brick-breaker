using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets.Pooling;
using Utilities;

public class BlockLoader : MonoBehaviour
{
    private static BlockLoader _instance;
    public static BlockLoader Instance => _instance;

    private void Awake()
    {
        if (_instance != null) return;
        _instance = this;
        DontDestroyOnLoad(this);
    }

    [SerializeField] private AddressableGameObjectSpawner spawner;

    private async void Start()
    {
        await spawner.InitializeAsync();
        SceneLoader.Instance.LoadLevel(MapManager.Instance.CurrentLevel);
        
    }

    public async UniTask LoadBlock(CSV data)
    {
        for (int i = 1; i < CSV.MAXRow; i++)
        {
            var row = data.GetRow(i);
            for (int j = 0; j < row.Length; j++)
            {
                string blockName;
                switch (row[j])
                {
                    case -2:
                    {
                        blockName = "TransparentBlock";
                        break;
                    }
                    case -1:
                    {
                        blockName = "UnbreakableBlock";
                        break;
                    }
                    case 1:
                    {
                        blockName = "BlockHit1";
                        break;
                    }
                    case 2:
                    {
                        blockName = "BlockHit2";
                        break;
                    }
                    case 3:
                    {
                        blockName = "BlockHit6";
                        break;
                    }
                    default:
                    {
                        blockName = null;
                        break;
                    }
                }

                if (blockName == null) continue;
                var block = await spawner.GetAsync(blockName);

                block.transform.localPosition = new Vector2(j, i);
            }
        }
    }

    public void Return(GameObject go)
    {
        spawner.Return(go);
    }
}