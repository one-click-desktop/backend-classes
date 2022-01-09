namespace OneClickDesktop.BackendClasses.Model.States
{
    /// <summary>
    /// State of session
    /// </summary>
    public enum SessionState
    {
        /// <summary>
        /// Session created and waiting for machine
        /// </summary>
        Pending,

        /// <summary>
        /// Session has machine assigned
        /// </summary>
        Running,

        /// <summary>
        /// Session cancelled by user or system (cannot be matched with machine)
        /// </summary>
        Cancelled,

        /// <summary>
        /// Session ended
        /// </summary>
        WaitingForRemoval
    }
}