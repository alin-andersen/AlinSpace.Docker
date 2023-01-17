namespace AlinSpace.Docker
{
    /// <summary>
    /// Represents the docker service interface.
    /// </summary>
    public interface IDockerService
    {
        /// <summary>
        /// Creates and starts the given containers asynchronously.
        /// </summary>
        /// <param name="containers">Enumerable of containers.</param>
        /// <remarks>
        /// Note: If the containers already exists, they will be stopped, removed, created and then started.
        /// Note: If you use the tag "latest", you will get the latest version every time. This might not be what is wanted. Use the exact version instead.
        /// </remarks>
        Task CreateAndStartContainersAsync(IEnumerable<Container> containers);

        /// <summary>
        /// Stops and removes the given containers asynchronously.
        /// </summary>
        /// <param name="containers">Enumerable of containers.</param>
        Task StopAndRemoveContainersAsync(IEnumerable<Container> containers);

        /// <summary>
        /// Stops and removes all containers asynchronously.
        /// </summary>
        Task StopAndRemoveAllContainers();
    }
}
