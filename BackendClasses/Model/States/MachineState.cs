namespace OneClickDesktop.BackendClasses.Model.States
{
    public enum MachineState
    {
        TurnedOff = 0,
        Booting,
        Free,
        Reserved,
        Occupied,
        WaitingForShutdown
    }
}
