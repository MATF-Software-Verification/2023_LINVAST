using System;
using Antlr4.Runtime;
using LINVAST.Nodes;
using NUnit.Framework;

namespace LINVAST.Tests.Imperative.Builders
{
    internal abstract class ASTBuilderTestBase
    {
        protected abstract ASTNode GenerateAST(string src);

        protected virtual ASTNode GenerateAST(string src, Func<Parser, ParserRuleContext> entryPoint) => GenerateAST(src);

        protected void AssertChildrenParentProperties(ASTNode node)
        {
            foreach (ASTNode child in node.Children)
                Assert.That(child.Parent, Is.EqualTo(node));
        }
    }
}
