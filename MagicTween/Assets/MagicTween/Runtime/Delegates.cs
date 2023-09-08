namespace MagicTween
{
    public delegate T TweenGetter<out T>();
    public delegate void TweenSetter<in T>(T value);

    public delegate T TweenGetter<in TObject, out T>(TObject obj) where TObject : class;
    public delegate void TweenSetter<in TObject, in T>(TObject obj, T value) where TObject : class;
}