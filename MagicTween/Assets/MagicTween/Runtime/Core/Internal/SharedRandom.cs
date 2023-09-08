using Unity.Mathematics;
using Unity.Burst;

namespace MagicTween.Core
{
    internal static class SharedRandom
    {
        static readonly SharedStatic<Random> sharedRandom = SharedStatic<Random>.GetOrCreate<RandomSharedStaticKey>();
        readonly struct RandomSharedStaticKey { }

        public static int NextInt(int min, int max) => sharedRandom.Data.NextInt(min, max);
        public static float NextFloat(float min, float max) => sharedRandom.Data.NextFloat(min, max);
        public static void InitState(uint seed) => sharedRandom.Data.InitState(seed);
    }
}