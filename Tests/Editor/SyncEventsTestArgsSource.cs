// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NUnit.Framework;
using System.Collections;

namespace Mojur.Unity.SolutionProcessing.Tests
{
    /// <summary>
    /// Provides a stream of different <see cref="SyncEventsTestArgs"/> for testing.
    /// </summary>
    public class SyncEventsTestArgsSource : IEnumerable
    {
        /// <inheritdoc />
        public IEnumerator GetEnumerator()
        {
            yield return new TestFixtureData(
                new SyncEventsTestArgs
                {
                    SlnPath = "path/to/sln",
                    SlnContent = "sln content",
                    CsprojPath = "path/to/csproj",
                    CsprojContent = "csproj content"
                });
        }
    }

    /// <summary>
    /// Test arguments for solution synchronization events.
    /// </summary>
    public class SyncEventsTestArgs
    {
        public string SlnPath { get; set; }

        public string SlnContent { get; set; }

        public string CsprojPath { get; set; }

        public string CsprojContent { get; set; }
    }
}
