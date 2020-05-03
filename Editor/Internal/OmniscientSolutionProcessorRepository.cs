// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Mojur.Unity.SolutionProcessing.Internal
{
    /// <summary>
    /// Uses reflection to find and instantiate <see cref="ISolutionSyncProcessor"/>s.
    /// </summary>
    internal class OmniscientSolutionProcessorRepository : ISolutionSyncProcessorRepository
    {
        private readonly IEnumerable<Assembly> assemblySearchArea;

        public OmniscientSolutionProcessorRepository(IEnumerable<Assembly> searchArea)
        {
            this.assemblySearchArea = searchArea;
        }

        /// <inheritdoc />
        [Pure]
        public IEnumerable<ISolutionSyncProcessor> GetProcessors()
        {
            return FindProcessorTypesWithin(this.assemblySearchArea)
                .Select(Activator.CreateInstance)
                .Cast<ISolutionSyncProcessor>();
        }

        [Pure]
        public static IEnumerable<Type> FindProcessorTypesWithin(IEnumerable<Assembly> assemblies)
        {
            return from assembly in assemblies
                   from type in assembly.GetTypes()
                   where IsValidProcessorType(type)
                   select type;

            bool IsValidProcessorType(Type type) =>
                typeof(ISolutionSyncProcessor).IsAssignableFrom(type) &&
                !IsIgnoredInvalidType(type) &&
                (type.IsVisible ? true : WarnInvalidType());

            bool IsIgnoredInvalidType(Type type) =>
                type.IsAbstract || type.IsInterface || type.ContainsGenericParameters;

            bool WarnInvalidType()
            {
                Debug.LogWarning(
                    $"Registered {nameof(ISolutionSyncProcessor)}s must be visible concrete classes with no generic parameters.");

                return false;
            }
        }
    }
}
