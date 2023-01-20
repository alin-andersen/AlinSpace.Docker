﻿namespace AlinSpace.Docker
{
    /// <summary>
    /// Represents the docker service interface.
    /// </summary>
    public interface IDockerService
    {
        /// <summary>
        /// Login asynchronously.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        /// <param name="server">Server.</param>
        Task LoginAsync(string username, string password, string server = null);

        /// <summary>
        /// Logout asynchronously.
        /// </summary>
        Task LogoutAsync();

        /// <summary>
        /// Creates the contaienr asynchronously.
        /// </summary>
        /// <param name="containerInfo">Container info.</param>
        Task CreateContainerAsync(ContainerInfo containerInfo);

        /// <summary>
        /// Starts the contaienr asynchronously.
        /// </summary>
        /// <param name="containerInfo">Container info.</param>
        Task StartContainerAsync(ContainerInfo containerInfo);

        /// <summary>
        /// Stops the contaienr asynchronously.
        /// </summary>
        /// <param name="containerInfo">Container info.</param>
        Task StopContainerAsync(ContainerInfo containerInfo);

        /// <summary>
        /// Removes the contaienr asynchronously.
        /// </summary>
        /// <param name="containerInfo">Container info.</param>
        Task RemoveContainerAsync(ContainerInfo containerInfo);
    }
}
