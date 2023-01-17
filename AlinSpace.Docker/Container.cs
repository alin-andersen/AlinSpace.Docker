namespace AlinSpace.Docker
{
    public class Container
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public string Tag { get; set; }

        public IList<EnvironmentVariable> EnvironmentVariables { get; set; }

        public IList<PortMapping> PortMappings { get; set; }

        public IList<BindMount> BindMounts { get; set; }

        public Container()
        {
        }

        public Container(
            string name, 
            string image, 
            string tag = null,
            IEnumerable<EnvironmentVariable> environmentVariables = null,
            IEnumerable<PortMapping> portMappings = null, 
            IEnumerable<BindMount> bindMounts = null)
        {
            Name = name;
            Image = image;
            Tag = tag ?? "latest";
            EnvironmentVariables = environmentVariables?.ToList() ?? new List<EnvironmentVariable>();
            PortMappings = portMappings?.ToList() ?? new List<PortMapping>();
            BindMounts = bindMounts?.ToList() ?? new List<BindMount>();
        }
    }
}
