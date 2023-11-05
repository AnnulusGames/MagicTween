using Unity.Entities;

namespace MagicTween.Benchmark
{
    public struct TestData : IComponentData
    {
        public float value;
    }

    public sealed class TestClass
    {
        public float value;
        public float valueProperty
        {
            get => value;
            set => this.value = value;
        }

        public const string PropertyName = nameof(valueProperty);
    }
}