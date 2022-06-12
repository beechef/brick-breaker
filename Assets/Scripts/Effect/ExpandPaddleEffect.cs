public class ExpandPaddleEffect : Effect
{
    private const string Name = "ExpandPaddle";
    private readonly Paddle _paddle;
    private float _scaleX;

    public ExpandPaddleEffect(float value, float times, Paddle paddle) : base(Name, value, times)
    {
        _paddle = paddle;
    }

    public override void OnStart()
    {
        _scaleX = _paddle.transform.localScale.x;
        _paddle.ExpandPaddle(value);
    }

    public override void OnUpdate()
    {
    }

    public override void OnEnd()
    {
        _paddle.ExpandPaddle(-value);
    }
}