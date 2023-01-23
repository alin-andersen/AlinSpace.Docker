namespace AlinSpace.Docker
{
    public class ContainerInfo
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public string Tag { get; set; }

        public IList<EnvironmentVariable> EnvironmentVariables { get; set; } = new List<EnvironmentVariable>();

        public IList<PortMapping> PortMappings { get; set; } = new List<PortMapping>();

        public IList<BindMount> BindMounts { get; set; } = new List<BindMount>();

        public bool AutoRestart { get; set; } = true;

        public ContainerInfo()
        {
        }

        public ContainerInfo(
            string name, 
            string image, 
            string tag = null,
            IEnumerable<EnvironmentVariable> environmentVariables = null,
            IEnumerable<PortMapping> portMappings = null, 
            IEnumerable<BindMount> bindMounts = null,
            bool autoRestart = true)
        {
            Name = name;
            Image = image;
            Tag = tag ?? "latest";
            EnvironmentVariables = environmentVariables?.ToList() ?? new List<EnvironmentVariable>();
            PortMappings = portMappings?.ToList() ?? new List<PortMapping>();
            BindMounts = bindMounts?.ToList() ?? new List<BindMount>();
            AutoRestart = autoRestart;
        }
    }
}
