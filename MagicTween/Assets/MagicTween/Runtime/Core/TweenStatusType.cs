namespace MagicTween.Core
{
    public enum TweenStatusType : byte
    {
        Invalid,
        WaitingForStart,
        Delayed,
        Playing,
        Paused,
        RewindCompleted,
        Completed,
        Killed
    }
}