namespace AlinSpace.Docker
{
    public class BindMount
    {
        public string Type { get; set; }

        public string HostPath { get; set; }

        public string ContainerPath { get; set; }

        public BindMount()
        {
        }

        public BindMount(string type, string hostPath, string containerPath)
        {
            Type = type;
            HostPath = hostPath;
            ContainerPath = containerPath;
        }

        public static BindMount New(string type, string hostPort, string containerPort)
        {
            return new BindMount(type, hostPort, containerPort);
        }
    }
}
