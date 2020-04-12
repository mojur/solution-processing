// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using UnityEditor;

namespace Mojur.Unity.SolutionProcessing.Internal
{
    /// <summary>
    /// Subscribes to Unity's solution synchronization
    /// reflection calls through <see cref="AssetPostprocessor"/>.
    /// </summary>
    internal class SolutionSyncUnitySubscriber : AssetPostprocessor
    {
        /// <remarks>Must not be read-only to allow for unit testing.</remarks>
        private static ISolutionSyncSubscriber _subscriber = new SolutionSyncPublisher();

        /// <summary>
        /// Called when the solution synchronization is starting (.csproj and .sln files).
        /// </summary>
        /// <remarks>Called by Unity via reflection.</remarks>
        /// <returns>True if the solution synchronization is complete; false otherwise.</returns>
        private static bool OnPreGeneratingCSProjectFiles()
        {
            return _subscriber.OnPreGeneratingCSProjectFiles();
        }

        /// <summary>
        /// Called when the .sln solution file has completed generation.
        /// </summary>
        /// <remarks>Called by Unity via reflection.</remarks>
        /// <param name="path">Full path of the .sln solution file.</param>
        /// <param name="content">Content of the .sln solution file in string form.</param>
        /// <returns>New content of the .sln solution file.</returns>
        private static string OnGeneratedSlnSolution(string path, string content)
        {
            return _subscriber.OnGeneratedSlnSolution(path, content);
        }

        /// <summary>
        /// Called for each .csproj project file that has completed generation.
        /// </summary>
        /// <remarks>Called by Unity via reflection.</remarks>
        /// <param name="path">Full path of the .csproj project file.</param>
        /// <param name="content">Content of the .csproj project file in string form.</param>
        /// <returns>New content of the .csproj project file.</returns>
        private static string OnGeneratedCSProject(string path, string content)
        {
            return _subscriber.OnGeneratedCSProject(path, content);
        }

        /// <summary>
        /// Called when the solution synchronization is complete
        /// </summary>
        /// <remarks>Called by Unity via reflection.</remarks>
        private static void OnGeneratedCSProjectFiles()
        {
            _subscriber.OnGeneratedCSProjectFiles();
        }
    }
}
