namespace MagicTween
{
    public enum LinkBehaviour : byte
    {
        KillOnDestroy,
        KillOnDisable,
        CompleteOnDisable,
        CompleteAndKillOnDisable,
        PlayOnEnable,
        PauseOnDisable,
        PauseOnDisablePlayOnEnable,
        PauseOnDisableRestartOnEnable,
        RestartOnEnable
    }
}