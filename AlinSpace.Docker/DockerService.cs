using AlinSpace.ConsoleHelper;
using System.Text;

namespace AlinSpace.Docker
{
    public class DockerService : IDockerService
    {
        public static IDockerService Instance { get; } = new DockerService();

        public async Task LoginAsync(string username, string password, string server)
        {
            if (string.IsNullOrWhiteSpace(server))
            {
                server = "registry-1.docker.io";
            }

            var command = $"docker login {server} --username {username} --password {password}";
            await CommandLineInterface.ExecuteAsync(command);
        }

        public async Task LogoutAsync()
        {
            var command = $"docker logout";
            await CommandLineInterface.ExecuteAsync(command);
        }

        public async Task CreateContainerAsync(ContainerInfo containerInfo)
        {
            await CommandLineInterface.ExecuteAsync(GetCreateContainerCommand(containerInfo));
        }

        public async Task StartContainerAsync(ContainerInfo containerInfo)
        {
            await CommandLineInterface.ExecuteAsync($"docker container start {containerInfo.Name}");
        }

        public async Task StopContainerAsync(ContainerInfo containerInfo)
        {
            await CommandLineInterface.ExecuteAsync($"docker container stop {containerInfo.Name}");
        }

        public async Task RemoveContainerAsync(ContainerInfo containerInfo)
        {
            await CommandLineInterface.ExecuteAsync($"docker container rm {containerInfo.Name}");
        }

        public string GetCreateContainerCommand(ContainerInfo container)
        {
            var commandBuilder = new StringBuilder();

            commandBuilder.Append($"docker create --name {container.Name} ");

            #region Environment Variables

            foreach(var enviornmentVariable in container.EnvironmentVariables)
            {
                commandBuilder.Append($"-e {enviornmentVariable.Key}={enviornmentVariable.Value} ");
            }

            #endregion

            #region Bind Mounts

            foreach (var bindMount in container.BindMounts)
            {
                var hostPath = bindMount.HostPath;

                if (string.IsNullOrWhiteSpace(bindMount.Type))
                    throw new Exception("Bind mount type not specified.");

                if (hostPath.Contains('\\') || hostPath.Contains(' '))
                {
                    hostPath = $"\"{hostPath}\"";
                }

                var containerPath = bindMount.ContainerPath;

                if (containerPath.Contains('\\') || containerPath.Contains(' '))
                {
                    containerPath = $"\"{containerPath}\"";
                }

                commandBuilder.Append($"--mount type={bindMount.Type},Source={hostPath},Target={containerPath} ");
            }

            #endregion

            #region Port Mappings

            foreach (var portMapping in container.PortMappings)
            {
                if (portMapping.HostPort < 1)
                    throw new Exception("Invalid host port.");

                if (portMapping.ContainerPort < 1)
                    throw new Exception("Invalid container port.");

                commandBuilder.Append($"-p {portMapping.HostPort}:{portMapping.ContainerPort} ");
            }

            #endregion

            if (string.IsNullOrWhiteSpace(container.Image))
                throw new Exception("Container image not specified.");

            var tag = container.Tag;

            if (string.IsNullOrWhiteSpace(tag))
            {
                tag = "latest";
            }

            commandBuilder.Append($"{container.Image}:{tag}");

            var command = commandBuilder.ToString().Trim();

            return command;
        }
    }
}
