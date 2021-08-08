using System;
using LINVAST.Imperative.Builders.Java;
using LINVAST.Imperative.Nodes;
using LINVAST.Nodes;
using LINVAST.Tests.Imperative.Builders.Common;
using NUnit.Framework;

namespace LINVAST.Tests.Imperative.Builders.Java
{
    internal sealed class EnumDeclarationTests : DeclarationTestsBase
    {

        [Test]
        public void NoConstantsEnumDeclTest()
        {
            string src1 = "enum Color {}";
            EnumDeclNode ast1 = this.GenerateAST(src1).As<EnumDeclNode>();
            Assert.That(ast1.Identifier, Is.EqualTo("Color"));
            Assert.That(ast1.GetText(), Is.EqualTo("Color"));
        }

        [Test]
        public void ImplementsInterfaceEnumDeclTest()
        {
            string src1 = "enum Color implements Name1 {}";
            Assert.That(() => this.GenerateAST(src1), Throws.InstanceOf<NotImplementedException>());
        }


        protected override ASTNode GenerateAST(string src)
            => new JavaASTBuilder().BuildFromSource(src, p => p.enumDeclaration());
    }
}
