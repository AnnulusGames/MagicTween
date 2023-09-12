namespace MagicTween.Core
{
    public enum TweenStatusType : byte
    {
        WaitingForStart,
        Delayed,
        Playing,
        Paused,
        RewindCompleted,
        Completed,
        Killed
    }
}