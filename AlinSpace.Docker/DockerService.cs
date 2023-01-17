using AlinSpace.ConsoleHelper;
using AlinSpace.Exceptions;
using System.Text;

namespace AlinSpace.Docker
{
    public class DockerService : IDockerService
    {
        public static IDockerService Instance { get; } = new DockerService();

        public async Task CreateAndStartContainersAsync(IEnumerable<Container> containers)
        {
            foreach(var container in containers)
            {
                ConsoleWriter.WriteLineWithPrefix($"Working on container for {container.Name} ...", "⬤", ConsoleColor.DarkYellow);

                // Stop container.
                var command = $"docker stop {container.Name}";
                ConsoleWriter.WriteLineWithPrefix($"→ {command}", "~", ConsoleColor.Yellow);
                await Try.CatchIgnoreAsync(() => CommandLineInterface.ExecuteAsync(command));
                ConsoleWriter.WriteLineWithPrefix($"→ {command}", "OK", ConsoleColor.Green);

                // Delete container.
                command = $"docker rm {container.Name}";
                ConsoleWriter.WriteLineWithPrefix($"→ {command}", "~", ConsoleColor.Yellow);
                await Try.CatchIgnoreAsync(() => CommandLineInterface.ExecuteAsync(command));
                ConsoleWriter.WriteLineWithPrefix($"→ {command}", "OK", ConsoleColor.Green);

                // Create container.
                command = GetCreateContainerCommand(container);
                ConsoleWriter.WriteLineWithPrefix($"→ {command}", "~", ConsoleColor.Yellow);
                await CommandLineInterface.ExecuteAsync(command);
                ConsoleWriter.WriteLineWithPrefix($"→ {command}", "OK", ConsoleColor.Green);

                // Start container.
                command = $"docker start {container.Name}";
                ConsoleWriter.WriteLineWithPrefix($"→ {command}", "~", ConsoleColor.Yellow);
                await CommandLineInterface.ExecuteAsync(command);
                ConsoleWriter.WriteLineWithPrefix($"→ {command}", "OK", ConsoleColor.Green);
            }
        }

        public async Task StopAndRemoveContainersAsync(IEnumerable<Container> containers)
        {
            foreach (var container in containers)
            {
                ConsoleWriter.WriteLineWithPrefix($"Working on container for {container.Name} ...", "⬤", ConsoleColor.DarkYellow);

                // Stop container.
                var command = $"docker stop {container.Name}";
                await Try.CatchIgnoreAsync(() => CommandLineInterface.ExecuteAsync(command));
                await CommandLineInterface.ExecuteAsync(command);
                ConsoleWriter.WriteLineWithPrefix($"→ {command}", "OK", ConsoleColor.Green);

                // Delete container.
                command = $"docker rm {container.Name}";
                ConsoleWriter.WriteLineWithPrefix($"→ {command}", "~", ConsoleColor.Yellow);
                await Try.CatchIgnoreAsync(() => CommandLineInterface.ExecuteAsync(command));
                ConsoleWriter.WriteLineWithPrefix($"→ {command}", "OK", ConsoleColor.Green);
            }
        }

        public Task StopAndRemoveAllContainers()
        {
            throw new NotImplementedException();
        }

        private string GetCreateContainerCommand(Container container)
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

                if (hostPath.Contains('\\') || hostPath.Contains(' '))
                {
                    hostPath = $"{@""""}{hostPath}{@""""}";
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
                commandBuilder.Append($"-p {portMapping.HostPort}:{portMapping.ContainerPort} ");
            }

            #endregion

            commandBuilder.Append($"{container.Image}:{container.Tag}");

            var command = commandBuilder.ToString().Trim();

            return command;
        }
    }
}
