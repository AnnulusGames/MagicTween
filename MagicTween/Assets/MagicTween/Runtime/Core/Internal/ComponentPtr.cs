using Unity.Collections.LowLevel.Unsafe;

namespace MagicTween.Core
{
    public unsafe struct ComponentPtr<TComponent> where TComponent : unmanaged
    {
        [NativeDisableUnsafePtrRestriction] public TComponent* Ptr;
        public ComponentPtr(TComponent* Ptr) => this.Ptr = Ptr;
    }
}