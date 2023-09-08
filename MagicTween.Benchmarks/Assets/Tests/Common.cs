using Unity.Entities;

public struct TestData : IComponentData
{
    public float value;
}

public class TestClass
{
    public float value;
    public float valueProperty
    {
        get => value;
        set => this.value = value;
    }

    public const string PropertyName = nameof(valueProperty);
}