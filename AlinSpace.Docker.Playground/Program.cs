namespace AlinSpace.Docker.Playground
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var containers = new List<ContainerInfo>
            {
                new ContainerInfo(
                    "mysql",
                    "mysql",
                    "latest",
                    new List<EnvironmentVariable>
                    {
                        new EnvironmentVariable("MYSQL_ROOT_PASSWORD", "Master1234"),
                    },
                    new List<PortMapping>
                    {
                        new PortMapping(3306, 3306),
                    },
                    new List<BindMount>
                    {
                        new BindMount("bind", @"C:\AlinSpace\Databases\mysql", "/var/lib/mysql"),
                    }),
            };

            DockerService.Instance.CreateAndStartContainersAsync(containers).Wait();

            DockerService.Instance.StopAndRemoveContainersAsync(containers).Wait();
        }
    }
}