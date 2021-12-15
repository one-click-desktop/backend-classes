namespace OneClickDesktop.BackendClasses.Model
{
    public struct MachineAddress
    {
        public string Address { get; set; }
        public int Port { get; set; }

        public MachineAddress(string address, int port = 3389)
        {
            Address = address;
            Port = port;
        }
    }
}