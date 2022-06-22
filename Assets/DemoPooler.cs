using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets.Pooling;

public class DemoPooler : MonoBehaviour
{
    public AddressableGameObjectSpawner spawner;

    private async void Start()
    {
        await spawner.InitializeAsync();
        CreateObject().Forget();
    }

    private async UniTask CreateObject()
    {
        for (int i = 0; i < 25; i++)
        {
            var obj = await spawner.GetAsync("UnbreakableBlock");

            obj.transform.position = new Vector2(i, i);
        }
    }
}