// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace Mojur.Unity.SolutionProcessing.Tests
{
    public class GeneratedFileEqualityComparer : IEqualityComparer<IGeneratedFile>
    {
        /// <inheritdoc />
        public bool Equals(IGeneratedFile x, IGeneratedFile y)
        {
            return ReferenceEquals(x, y) || (x?.File.Name == y?.File.Name && x.Content == y.Content);
        }

        /// <inheritdoc />
        public int GetHashCode(IGeneratedFile obj)
        {
            return (obj.File.GetHashCode() * 7) ^ obj.Content.GetHashCode();
        }
    }
}
