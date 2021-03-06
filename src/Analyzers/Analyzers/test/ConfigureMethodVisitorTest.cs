// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Xunit;

namespace Microsoft.AspNetCore.Analyzers
{
    public class ConfigureMethodVisitorTest : AnalyzerTestBase
    {
        [Fact]
        public async Task FindConfigureMethods_AtDifferentScopes()
        {
            // Arrange
            var expected = new string[]
            {
                "global::ANamespace.Nested.Startup.Configure",
                "global::ANamespace.Nested.Startup.NestedStartup.Configure",
                "global::ANamespace.Startup.ConfigureDevelopment",
                "global::ANamespace.Startup.NestedStartup.ConfigureTest",
                "global::Another.AnotherStartup.Configure",
                "global::GlobalStartup.Configure",
            };

            var compilation = await CreateCompilationAsync("Startup");
            var symbols = new StartupSymbols(compilation);

            // Act
            var results = ConfigureMethodVisitor.FindConfigureMethods(symbols, compilation.Assembly);

            // Assert
            var actual = results
                .Select(m => m.ContainingType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) + "." + m.Name)
                .OrderBy(s => s)
                .ToArray();
            Assert.Equal(expected, actual);
        }
    }
}
