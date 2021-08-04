using System;
using System.Linq;
using LINVAST.Imperative.Builders.Java;
using LINVAST.Imperative.Nodes;
using LINVAST.Nodes;
using LINVAST.Tests.Imperative.Builders.Common;
using NUnit.Framework;

namespace LINVAST.Tests.Imperative.Builders.Java
{
    internal sealed class InterfaceBodyDeclarationTests : DeclarationTestsBase
    {
        [Test]
        public void EmptyInterfaceBodyDeclTest()
        {
            string src1 = ";";
            EmptyStatNode ast1 = this.GenerateAST(src1).As<EmptyStatNode>();
            Assert.That(ast1.GetText(), Is.EqualTo(";"));
        }

        [Test]
        public void WithAnnotationClassBodyDeclTest()
        {
            string src1 = "@Override public string toString() {return this.attr.toString();}";

            Assert.That(() => this.GenerateAST(src1), Throws.InstanceOf<NotImplementedException>());
        }

        [Test]
        public void ConstDeclarationTest()
        {
            string src1 = "String str = null;";
            DeclStatNode ast1 = this.GenerateAST(src1).As<DeclStatNode>();
            Assert.That(ast1.GetText(), Is.EqualTo("String str = null;"));
        }

        [Test]
        public void MultipleDeclaratorsConstDeclTest()
        {
            string src1 = "String str1 = null, str2 = null;";
            DeclStatNode ast1 = this.GenerateAST(src1).As<DeclStatNode>();
            Assert.That(ast1.DeclaratorList.Children.Count, Is.EqualTo(2));
            Assert.That(ast1.GetText(), Is.EqualTo("String str1 = null, str2 = null;"));
        }

        [Test]
        public void InterfaceMethodTest()
        {
            string src1 = "String f(){}";
            DeclStatNode ast1 = this.GenerateAST(src1).As<DeclStatNode>();
            StringAssert.IsMatch(@"f\(<>\){\s*}", ast1.GetText());
        }

        [Test]
        public void WithModifiersInterfaceMethodTest()
        {
            string src1 = "public static String f() {}";
            string src2 = "public String f() {}";

            DeclStatNode ast1 = this.GenerateAST(src1).As<DeclStatNode>();
            DeclStatNode ast2 = this.GenerateAST(src2).As<DeclStatNode>();
            StringAssert.IsMatch(@"public static String f\(<>\){\s*}", ast1.GetText());
            StringAssert.IsMatch(@"public String f\(<>\){\s*}", ast2.GetText());
        }

        [Test]
        public void WithInterfaceSpecificModifiersIMethodTest()
        {
            string src1 = "public static default String f() {}";
            string src2 = "default String f() {}";

            DeclStatNode ast1 = this.GenerateAST(src1).As<DeclStatNode>();
            DeclStatNode ast2 = this.GenerateAST(src2).As<DeclStatNode>();

            StringAssert.IsMatch(@"public static default String f\(<>\){\s*}", ast1.GetText());
            StringAssert.IsMatch(@"default String f\(<>\){\s*}", ast2.GetText());
        }


        [Test]
        public void WithBracketsInterfaceMethodTest()
        {
            string src1 = "String f()[] {}";
            Assert.That(() => this.GenerateAST(src1), Throws.InstanceOf<NotImplementedException>());
        }

        [Test]
        public void GenericInterfaceMethodTest()
        {
            string src1 = "<Class1> String f(){}";
            DeclStatNode ast1 = this.GenerateAST(src1).As<DeclStatNode>();
            StringAssert.IsMatch(@"String f\(<Class1>\){\s*}", ast1.GetText());
        }

        [Test]
        public void WitModifiersGenericInterfaceMethodTest()
        {
            string src1 = "public <Class1> String f(){}";
            string src2 = "volatile <Class1> String f(){}";

            DeclStatNode ast1 = this.GenerateAST(src1).As<DeclStatNode>();
            DeclStatNode ast2 = this.GenerateAST(src2).As<DeclStatNode>();

            Assert.That(ast1.DeclaratorList.Declarators.First().Identifier, Is.EqualTo("f"));
            Assert.That(ast1.Specifiers.TypeName, Is.EqualTo("String"));
            Assert.That(ast1.Modifiers.ToString(), Is.EqualTo("public"));
            StringAssert.IsMatch(@"public String f\(<Class1>\){\s*}", ast1.GetText());


            Assert.That(ast2.Modifiers.ToString(), Is.EqualTo("volatile"));
            StringAssert.IsMatch(@"volatile String f\(<Class1>\){\s*}", ast2.GetText());

        }

        protected override ASTNode GenerateAST(string src)
            => new JavaASTBuilder().BuildFromSource(src, p => p.interfaceBodyDeclaration());
    }
}
