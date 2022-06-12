using System.Collections.Generic;

public class SpeedUpEffect : Effect
{
    private static readonly List<SpeedUpEffect> SpeedUpEffects = new List<SpeedUpEffect>();
    private const string Name = "SpeedUpEffect";
    private Paddle _paddle;

    public SpeedUpEffect(float value, float times, Paddle paddle) : base(Name, value, times)
    {
        _paddle = paddle;
    }

    private void UpdateSpeed()
    {
        float moveSpeed = _paddle.GetMoveSpeed();
        float value = 0f;
        foreach (var speedUpEffect in SpeedUpEffects)
        {
            value += speedUpEffect.value;
        }

        value /= 100;
        _paddle.UpdateMoveSpeed(moveSpeed * value);
    }
    public override void OnStart()
    {
        SpeedUpEffects.Add(this);
        UpdateSpeed();
    }

    public override void OnUpdate()
    {
    }

    public override void OnEnd()
    {
        SpeedUpEffects.Remove(this);
        UpdateSpeed();
    }
}