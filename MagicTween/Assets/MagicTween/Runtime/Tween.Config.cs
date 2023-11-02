using Unity.Entities;
using MagicTween.Core;

namespace MagicTween
{
    public partial struct Tween
    {
        public static World World
        {
            get
            {
                return ECSCache.World;
            }
            set
            {
                ECSCache.Create(value);
            }
        }
    }
}