using System.Drawing;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] public float minRelativePosX = 1f; // assumes paddle size of 1 relative unit

    [SerializeField] public float maxRelativePosX = 15f; // assumes paddle size of 1 relative unit

    [SerializeField] public float fixedRelativePosY = .64f; // paddle does not move on the Y directiob

    // Unity units of the WIDTH of the screen (e.g. 16)
    [SerializeField] public float screenWidthUnits = 16;

    private float _speedUnit = 0.2f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float addingMoveSpeed;

    private Camera _camera;
    public float sensitive = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        mousePos.x *= (moveSpeed + addingMoveSpeed) * _speedUnit;

        transform.position = GetUpdatedPaddlePosition(_camera.ScreenToWorldPoint(mousePos).x);
    }

    public Vector2 GetUpdatedPaddlePosition(float relativePosX)
    {
        // clamps the X position
        float clampedRelativePosX = Mathf.Clamp(relativePosX, minRelativePosX, maxRelativePosX);
        
        Vector2 newPaddlePosition = new Vector2(clampedRelativePosX, fixedRelativePosY);
        return newPaddlePosition;
    }
    
    public void ExpandPaddle(float value)
    {
        Vector3 localScale = transform.localScale;
        localScale.x += value;
        transform.localScale = localScale;
        minRelativePosX += value;
        maxRelativePosX -= value;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public void UpdateMoveSpeed(float value)
    {
        addingMoveSpeed = value;
    }
}