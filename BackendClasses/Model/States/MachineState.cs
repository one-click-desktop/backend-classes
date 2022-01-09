namespace OneClickDesktop.BackendClasses.Model.States
{
    /// <summary>
    /// State of machine
    /// </summary>
    public enum MachineState
    {
        /// <summary>
        /// Machine is turned off - treated  as nonexistent
        /// </summary>
        TurnedOff = 0,

        /// <summary>
        /// Machine is booting - treated as busy
        /// </summary>
        Booting,

        /// <summary>
        /// Machine has booted and doesn't have session assigned - treated as available
        /// </summary>
        Free,

        /// <summary>
        /// Machine has session assigned and is waiting for user to connect - treated as busy
        /// </summary>
        Reserved,

        /// <summary>
        /// Machine has session assigned and client connected - treated as busy
        /// </summary>
        Occupied,

        /// <summary>
        /// Machine has session assigned and client disconnected, waiting for reconnect or shutdown - treated as busy
        /// </summary>
        WaitingForShutdown
    }
}