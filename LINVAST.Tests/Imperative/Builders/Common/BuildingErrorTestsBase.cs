using System;
using Antlr4.Runtime;
using NUnit.Framework;

namespace LINVAST.Tests.Imperative.Builders.Common
{
    internal abstract class BuildingErrorTestsBase : ASTBuilderTestBase
    {
        protected void AssertThrows<TException>(string src) where TException : Exception
            => Assert.That(() => this.GenerateAST(src), Throws.InstanceOf<TException>());
        
        protected void AssertThrows<TException>(string src, Func<Parser, ParserRuleContext> entryProvider) where TException : Exception
            => Assert.That(() => this.GenerateAST(src, entryProvider), Throws.InstanceOf<TException>());
    }
}
