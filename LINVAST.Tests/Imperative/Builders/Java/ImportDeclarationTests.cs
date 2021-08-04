using System;
using LINVAST.Imperative.Builders.Java;
using LINVAST.Imperative.Nodes;
using LINVAST.Nodes;
using LINVAST.Tests.Imperative.Builders.Common;
using NUnit.Framework;

namespace LINVAST.Tests.Imperative.Builders.Java
{
    internal sealed class ImportDeclarationTests : DeclarationTestsBase
    {

        [Test]
        public void SingleNameImportDeclTest()
        {
            string src1 = "import System;";
            string src2 = "import System ;";


            ImportNode ast1 = this.GenerateAST(src1).As<ImportNode>(); 
            ImportNode ast2 = this.GenerateAST(src2).As<ImportNode>();

            Assert.That(ast1.Directive, Is.EqualTo("System"));
            Assert.That(ast2.Directive, Is.EqualTo("System"));
        }

        [Test]
        public void WithDotsImportDeclTest()
        {
            string src1 = "import Name1.Name2.Name3.Name4;";
            string src2 = "import System.Text.Json;";
            string src3 = "import System.Text ;";
            string src4 = "import System . NNN;";

            ImportNode ast1 = this.GenerateAST(src1).As<ImportNode>();
            ImportNode ast2 = this.GenerateAST(src2).As<ImportNode>();
            ImportNode ast3 = this.GenerateAST(src3).As<ImportNode>();
            ImportNode ast4 = this.GenerateAST(src4).As<ImportNode>();

            Assert.That(ast1.Directive, Is.EqualTo("Name1.Name2.Name3.Name4"));
            Assert.That(ast2.Directive, Is.EqualTo("System.Text.Json"));
            Assert.That(ast3.Directive, Is.EqualTo("System.Text"));
            Assert.That(ast4.Directive, Is.EqualTo("System.NNN"));
        }

        [Test]
        public void WithWildcardImportDeclTest()
        {
            string src1 = "import System.Text.Json.*;";
            string src2 = "import System.Text. *;";
            string src3 = "import System.*;";

            ImportNode ast1 = this.GenerateAST(src1).As<ImportNode>();
            ImportNode ast2 = this.GenerateAST(src2).As<ImportNode>();
            ImportNode ast3 = this.GenerateAST(src3).As<ImportNode>();

            Assert.That(ast1.Directive, Is.EqualTo("System.Text.Json.*"));
            Assert.That(ast2.Directive, Is.EqualTo("System.Text.*"));
            Assert.That(ast3.Directive, Is.EqualTo("System.*"));
        }

        [Test]
        public void StaticImportDeclTest()
        {
            string src1 = "import static System.Text.Json.*;";
            string src2 = "import static System;";
            string src3 = "import static System.Text;";

            Assert.That(() => this.GenerateAST(src1), Throws.InstanceOf<NotImplementedException>());
            Assert.That(() => this.GenerateAST(src2), Throws.InstanceOf<NotImplementedException>());
            Assert.That(() => this.GenerateAST(src3), Throws.InstanceOf<NotImplementedException>());
        }


        protected override ASTNode GenerateAST(string src) 
            => new JavaASTBuilder().BuildFromSource(src, p => p.importDeclaration());
    }
}
