using System;
using LINVAST.Imperative.Builders.Java;
using LINVAST.Nodes;
using LINVAST.Tests.Imperative.Builders.Common;
using NUnit.Framework;

namespace LINVAST.Tests.Imperative.Builders.Java
{
    internal sealed class PackageDeclarationTests : DeclarationTestsBase
    {

        [Test]
        public void PackageDeclarationTest()
        {
            string src1 = "package mypkg;";
            Assert.That(() => this.GenerateAST(src1), Throws.InstanceOf<NotImplementedException>());
        }


        protected override ASTNode GenerateAST(string src)
            => new JavaASTBuilder().BuildFromSource(src, p => p.packageDeclaration());
    }
}
