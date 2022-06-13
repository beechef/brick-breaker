using UnityEngine;

public class DemoCollider : MonoBehaviour
{
    private Vector3[] cubePositions =
    {
        new() {x = 0, y = 0, z = 0},
        new() {x = 1, y = 0, z = 0},
        new() {x = 2, y = 0, z = 0}
    };

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hitInfo);
            if (hitInfo.collider != null)
            {
                Debug.Log(hitInfo.point);
                // foreach (var cubePosition in cubePositions)
                // {
                //     float distance = Vector3.Distance(hitInfo.point, cubePosition);
                //     Debug.Log(distance);
                //     if (distance >= 0.5f && distance <= Mathf.Sqrt(2) / 2)
                //     {
                //         Debug.Log(cubePosition);
                //     }
                // }
            }
        }
    }
}