public abstract class Effect
{
    public string name;
    public float value;
    public float times; // Seconds

    public Effect(string name, float value, float times)
    {
        this.name = name;
        this.value = value;
        this.times = times;
    }

    public abstract void OnStart();
    public abstract void OnUpdate();
    public abstract void OnEnd();
}