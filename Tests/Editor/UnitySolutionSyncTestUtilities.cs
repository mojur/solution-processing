// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace Mojur.Unity.SolutionProcessing.Tests
{
    /// <summary>
    /// Solution synchronization method names and that Unity searches for and calls upon.
    /// </summary>
    public enum UnitySyncMethodName
    {
        OnPreGeneratingCSProjectFiles,

        OnGeneratedSlnSolution,

        OnGeneratedCSProject,

        OnGeneratedCSProjectFiles
    }

    /// <summary>
    /// Utilities for testing solution synchronization from Unity's point of view.
    /// </summary>
    public static class UnitySolutionSyncTestUtilities
    {
        private const string QueryMethodName = "AllPostProcessorMethodsNamed";

        private const string AssetPostprocessingInternalFullName = "UnityEditor.AssetPostprocessingInternal";

        private const string AssetPostprocessingInternalAssemblyName = "UnityEditor";

        private static Type _assetPostprocessingInternalType;

        public static Type AssetPostprocessingInternalType
        {
            get
            {
                return _assetPostprocessingInternalType =
                    _assetPostprocessingInternalType ?? GetAssetPostprocessingInternalType();
            }
        }

        public static object InvokeStaticSyncMethodFor<TProcessor>(
            UnitySyncMethodName method,
            params object[] parameters) where TProcessor : AssetPostprocessor
        {
            return InvokeStaticMethod(GetStaticSyncMethodFor<TProcessor>(method), parameters);
        }

        [Pure]
        public static MethodInfo GetStaticSyncMethodFor<TProcessor>(UnitySyncMethodName methodName)
            where TProcessor : AssetPostprocessor
        {
            return GetStaticSyncMethodFor(typeof(TProcessor), methodName.ToString());
        }

        [Pure]
        public static MethodInfo GetStaticSyncMethodFor(Type processorType, string methodName)
        {
            return processorType.IsSubclassOf(typeof(AssetPostprocessor))
                ? FindPostprocessorMethodsNamed(methodName)
                    .SingleOrDefault(method => method.DeclaringType == processorType)
                : throw new ArgumentException(
                    $"Must be a subclass of {nameof(AssetPostprocessor)}",
                    nameof(processorType));
        }

        [Pure]
        private static IEnumerable<MethodInfo> FindPostprocessorMethodsNamed(string name)
        {
            var queryMethod = AssetPostprocessingInternalType.GetMethod(
                QueryMethodName,
                BindingFlags.NonPublic | BindingFlags.Static);

            Assume.That(
                queryMethod,
                Is.Not.Null,
                $"{QueryMethodName} could not be found within type {AssetPostprocessingInternalFullName}.");

            return (IEnumerable<MethodInfo>)InvokeStaticMethod(queryMethod, name);
        }

        private static object InvokeStaticMethod(MethodInfo method, params object[] parameters)
        {
            object result = null;
            Assume.That(method, Is.Not.Null);

            try
            {
                result = method.Invoke(null, parameters);
            }
            catch (Exception ex) when (ex is TargetException ||
                ex is ArgumentException ||
                ex is TargetParameterCountException)
            {
                Assert.Inconclusive(
                    $"{ex.Message}. {method.Name} of {AssetPostprocessingInternalFullName} " +
                    "failed to invoke with the given parameters.");
            }

            return result;
        }

        private static Type GetAssetPostprocessingInternalType()
        {
            var assetPostprocessingInternalAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .SingleOrDefault(asm => asm.GetName().Name.Equals(AssetPostprocessingInternalAssemblyName));

            Assume.That(
                assetPostprocessingInternalAssembly,
                Is.Not.Null,
                $"{AssetPostprocessingInternalAssemblyName} assembly can not be found within loaded assemblies.");

            Type assetPostprocessingInternalType = assetPostprocessingInternalAssembly.GetTypes()
                .SingleOrDefault(type => type.FullName == AssetPostprocessingInternalFullName);

            Assume.That(
                assetPostprocessingInternalType,
                Is.Not.Null,
                $"{AssetPostprocessingInternalFullName} type can not be found within assembly" +
                $"{AssetPostprocessingInternalAssemblyName}.");

            return assetPostprocessingInternalType;
        }
    }
}
