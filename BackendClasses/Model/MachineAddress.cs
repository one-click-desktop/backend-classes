namespace OneClickDesktop.BackendClasses.Model
{
    /// <summary>
    /// Describes address of machine
    /// </summary>
    public struct MachineAddress
    {
        /// <summary>
        /// Hostname part of machine address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Port of machine address
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Creates new machine address with hostname and port
        /// </summary>
        /// <param name="address">Hostname part of address</param>
        /// <param name="port">Port of address</param>
        public MachineAddress(string address, int port = 3389)
        {
            Address = address;
            Port = port;
        }
    }
}