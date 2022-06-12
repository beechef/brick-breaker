using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] public float minRelativePosX = 1f; // assumes paddle size of 1 relative unit

    private float _baseMinRelativePosX = 0f;
    [SerializeField] public float maxRelativePosX = 15f; // assumes paddle size of 1 relative unit
    private float _baseMaxRelativePosX = 16f;

    [SerializeField] public float fixedRelativePosY = .64f; // paddle does not move on the Y directiob

    // Unity units of the WIDTH of the screen (e.g. 16)
    [SerializeField] public float screenWidthUnits = 16;

    private float _speedUnit = 0.2f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float addingMoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        float startPosX = ConvertPixelToRelativePosition(screenWidthUnits / 2, Screen.width);
        transform.position = GetUpdatedPaddlePosition(startPosX);
    }

    // Update is called once per frame
    void Update()
    {
        var relativePosX = ConvertPixelToRelativePosition(
            pixelPosition: Input.mousePosition.x * (moveSpeed + addingMoveSpeed) * _speedUnit, Screen.width);
        transform.position = GetUpdatedPaddlePosition(relativePosX);
    }

    public Vector2 GetUpdatedPaddlePosition(float relativePosX)
    {
        // clamps the X position
        float clampedRelativePosX = Mathf.Clamp(relativePosX, minRelativePosX, maxRelativePosX);

        Vector2 newPaddlePosition = new Vector2(clampedRelativePosX, fixedRelativePosY);
        return newPaddlePosition;
    }

    public float ConvertPixelToRelativePosition(float pixelPosition, int screenWidth)
    {
        var relativePosition = pixelPosition / screenWidth * screenWidthUnits;
        return relativePosition;
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