// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NUnit.Framework.Constraints;
using System;
using System.Linq;
using System.Reflection;

namespace Mojur.NUnitExtensions.Reflection
{
    public class MethodExistsConstraint : MemberExistsReflectionConstraint<MethodBase>
    {
        private Type[] parameterTypes;

        private Type expectedReturnType;

        private string description;

        /// <inheritdoc />
        public override string Description
        {
            get => "Method found";
        }

        public MethodExistsConstraint(string methodName) : base(methodName, DefaultMethodSearch)
        {
        }

        /// <inheritdoc />
        public override ConstraintResult ApplyTo(Type subjectType)
        {
            bool doesExist = this.searchCallback(subjectType, this.memberName)
                .Any(method => ParamFilter(method) && ReturnFilter(method));

            return new ConstraintResult(this, subjectType, doesExist);

            bool ReturnFilter(MethodBase method)
            {
                return this.expectedReturnType == null ||
                    !(method is MethodInfo methodInfo) ||
                    methodInfo.ReturnType == this.expectedReturnType;
            }

            bool ParamFilter(MethodBase method)
            {
                var actualParams = method.GetParameters().Select(param => param.ParameterType);

                return this.parameterTypes.Length == 0 || actualParams.SequenceEqual(this.parameterTypes);
            }
        }

        public MethodExistsConstraint WithNoParams
        {
            get => this.WithParams(null);
        }

        public MethodExistsConstraint WithParams(params Type[] parameters)
        {
            this.parameterTypes = parameters ?? new Type[0];

            return this;
        }

        public MethodExistsConstraint ThatReturns(Type returnType)
        {
            this.expectedReturnType = returnType;

            return this;
        }

        private static MethodBase[] DefaultMethodSearch(Type type, string methodName)
        {
            return type.GetMethods().Where(method => method.Name.Equals(methodName)).ToArray();
        }
    }
}
