// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;

namespace Mojur.Unity.SolutionProcessing
{
    /// <summary>
    /// Provides data of a generated file.
    /// </summary>
    public interface IGeneratedFile
    {
        /// <summary>
        /// <see cref="FileInfo"/> of the generated file.
        /// </summary>
        FileInfo File { get; }

        /// <summary>
        /// Content of the generated file.
        /// </summary>
        string Content { get; set; }
    }
}
