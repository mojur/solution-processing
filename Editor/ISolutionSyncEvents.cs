// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Mojur.Unity.SolutionProcessing
{
    /// <summary>
    /// Provides access to solution synchronization events.
    /// </summary>
    public interface ISolutionSyncEvents
    {
        /// <summary>
        /// Occurs when solution synchronization is starting (.csproj and .sln files).
        /// </summary>
        /// <remarks>
        /// A completed status of true in <see cref="ISolutionSyncStatus"/> results in
        /// neither .csproj's nor .sln's being generated. Thus <see cref="SlnGenerated"/>
        /// and <see cref="CsprojGenerated"/> events are not triggered.
        /// </remarks>
        event Action<ISolutionSyncStatus> SolutionSyncing;

        /// <summary>
        /// Occurs when the .sln file has completed generation.
        /// </summary>
        /// <remarks>
        /// Content of .sln file will be overwritten by the content of <see cref="IGeneratedFile"/>.
        /// Triggered before <see cref="CsprojGenerated"/>.
        /// </remarks>
        event Action<IGeneratedFile> SlnGenerated;

        /// <summary>
        /// Occurs for each .csproj file that has completed generation.
        /// </summary>
        /// <remarks>
        /// Content of .csproj file will be overwritten by the content of <see cref="IGeneratedFile"/>.
        /// Triggered after <see cref="SlnGenerated"/>.
        /// </remarks>
        event Action<IGeneratedFile> CsprojGenerated;

        /// <summary>
        /// Occurs when solution synchronization is complete (.csproj and .sln files).
        /// </summary>
        /// <remarks>
        /// Triggered regardless of whether the synchronization has been marked as complete.
        /// </remarks>
        event Action SolutionSynced;
    }
}
