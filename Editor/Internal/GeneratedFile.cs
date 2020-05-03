// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;

namespace Mojur.Unity.SolutionProcessing.Internal
{
    internal class GeneratedFile : IGeneratedFile
    {
        /// <inheritdoc/>
        public FileInfo File { get; }

        /// <inheritdoc/>
        public string Content { get; set; }

        public GeneratedFile(FileInfo file, string contents)
        {
            this.File = file;
            this.Content = contents;
        }
    }
}
