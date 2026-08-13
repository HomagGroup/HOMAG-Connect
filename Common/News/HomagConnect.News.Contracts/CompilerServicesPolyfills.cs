#if NETSTANDARD2_0
namespace System.Runtime.CompilerServices
{
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    internal static class IsExternalInit
    {
    }

    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    [global::System.AttributeUsage(global::System.AttributeTargets.All, Inherited = false)]
    internal sealed class RequiredMemberAttribute : global::System.Attribute
    {
    }

    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    [global::System.AttributeUsage(global::System.AttributeTargets.All, Inherited = false)]
    internal sealed class CompilerFeatureRequiredAttribute : global::System.Attribute
    {
        public CompilerFeatureRequiredAttribute(string featureName)
        {
            FeatureName = featureName;
        }

        public string FeatureName { get; }

        public bool IsOptional { get; init; }
    }
}

namespace System.Diagnostics.CodeAnalysis
{
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    [global::System.AttributeUsage(global::System.AttributeTargets.Constructor, Inherited = false)]
    internal sealed class SetsRequiredMembersAttribute : global::System.Attribute
    {
    }
}
#endif
