// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1633:File should have header", Justification = "Not something we need at this stage - jcreek")]
[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1208:System using directives should be placed before other using directives", Justification = "I'd prefer to have the using directives in alphabetical order - jcreek")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:Do not use regions", Justification = "They are really useful, especially when used appropriately - jcreek")]
[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1200:Using directives should be placed correctly", Justification = "I prefer the using directives outside the namespace within the file - jcreek")]
