namespace AlinSpace.Docker
{
    public class PortMapping
    {
        public int HostPort { get; set; }

        public int ContainerPort { get; set; }

        public PortMapping()
        {
        }

        public PortMapping(int hostPort, int containerPort)
        {
            HostPort = hostPort;
            ContainerPort = containerPort;
        }

        public static PortMapping New(int hostPort, int containerPort)
        {
            return new PortMapping(hostPort, containerPort);
        }
    }
}
