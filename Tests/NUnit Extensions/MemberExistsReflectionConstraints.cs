// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NUnit.Framework.Constraints;
using System;

namespace Mojur.NUnitExtensions.Reflection
{
    public abstract class MemberExistsReflectionConstraint<TMember> : Constraint
    {
        public delegate TMember[] MemberSearchCallback(Type type, string memberName);

        protected readonly string memberName;

        protected MemberSearchCallback searchCallback;

        protected MemberExistsReflectionConstraint(string memberName, MemberSearchCallback callback)
        {
            this.memberName = memberName;
            this.searchCallback = callback;
        }

        /// <inheritdoc />
        public override ConstraintResult ApplyTo(object actual)
        {
            return this.ApplyTo(actual is Type type ? type : actual.GetType());
        }

        public abstract ConstraintResult ApplyTo(Type subjectType);

        public MemberExistsReflectionConstraint<TMember> Using(MemberSearchCallback callback)
        {
            this.searchCallback = callback;

            return this;
        }
    }
}
