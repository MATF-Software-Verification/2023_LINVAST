using System;
using System.Linq;
using LINVAST.Imperative.Builders.Java;
using LINVAST.Imperative.Nodes;
using LINVAST.Nodes;
using LINVAST.Tests.Imperative.Builders.Common;
using NUnit.Framework;

namespace LINVAST.Tests.Imperative.Builders.Java
{
    internal sealed class ExpressionFunctionsTests: ExpressionTestsBase
    {
        [Test]
        public void TestLiteralExpressions()
        {
            string src1 = "5";
            LitExprNode ast1 = this.GenerateAST(src1).As<LitExprNode>();
            Assert.That(ast1.Value, Is.EqualTo(5));

            string src2 = "5.8";
            LitExprNode ast2 = this.GenerateAST(src2).As<LitExprNode>();
            Assert.That(ast2.Value, Is.EqualTo(5.8));

            string src3 = "null";
            LitExprNode ast3 = this.GenerateAST(src3).As<LitExprNode>();
            Assert.That(ast3.GetText(), Is.EqualTo("null"));

            string src4 = "true";
            LitExprNode ast4 = this.GenerateAST(src4).As<LitExprNode>();
            Assert.That(ast4.GetText(), Is.EqualTo("True"));
        }

        [Test]
        public void TestPrimary()
        {
            string src1 = "super";
            FuncCallExprNode ast1 = this.GenerateAST(src1).As<FuncCallExprNode>();
            Assert.That(ast1.Identifier, Is.EqualTo("super"));
            Assert.That(ast1.TemplateArguments, Is.EqualTo(null));
            Assert.That(ast1.Arguments, Is.EqualTo(null));

            string src2 = "name";
            IdNode ast2 = this.GenerateAST(src2).As<IdNode>();
            Assert.That(ast2.Identifier, Is.EqualTo("name"));

            string src3 = "2";
            LitExprNode ast3 = this.GenerateAST(src3).As<LitExprNode>();
            Assert.That(ast3.Value, Is.EqualTo(2));
        }

        [Test]
        public void TestParExpression() {
            string src1 = "(2.6)";
            LitExprNode ast1 = this.GenerateAST(src1).As<LitExprNode>();
            Assert.That(ast1.Value, Is.EqualTo(2.6));
        }

        [Test]
        public void TestLambdaExpression()
        {
            string src1 = "x -> x*x";
            LambdaFuncExprNode ast1 = this.GenerateAST(src1).As<LambdaFuncExprNode>();
            Assert.That(ast1.Definition.GetText(), Is.EqualTo("{ (x * x) }"));
            Assert.That(ast1.GetText(), Is.EqualTo("lambda (object x): { (x * x) }"));

            string src2 = "a -> a+9";
            LambdaFuncExprNode ast2 = this.GenerateAST(src2).As<LambdaFuncExprNode>();
            Assert.That(ast2.Definition.GetText(), Is.EqualTo("{ (a + 9) }"));
            Assert.That(ast2.GetText(), Is.EqualTo("lambda (object a): { (a + 9) }"));

            string src3 = "(x, y) -> x*y";
            LambdaFuncExprNode ast3 = this.GenerateAST(src3).As<LambdaFuncExprNode>();
            Assert.That(ast3.Definition.GetText(), Is.EqualTo("{ (x * y) }"));
            Assert.That(ast3.GetText(), Is.EqualTo("lambda (object x, object y): { (x * y) }"));

            string src4 = "(int x) -> x-2";
            LambdaFuncExprNode ast4 = this.GenerateAST(src4).As<LambdaFuncExprNode>();
            Assert.That(ast4.Definition.GetText(), Is.EqualTo("{ (x - 2) }"));
            Assert.That(ast4.GetText(), Is.EqualTo("lambda (int x): { (x - 2) }"));

            string src5 = "(bool x, bool y) -> x&&y";
            LambdaFuncExprNode ast5 = this.GenerateAST(src5).As<LambdaFuncExprNode>();
            Assert.That(ast5.Definition.GetText(), Is.EqualTo("{ (x && y) }"));
            Assert.That(ast5.GetText(), Is.EqualTo("lambda (bool x, bool y): { (x && y) }"));
        }

        [Test]
        public void TestConditionalExpr()
        {
            string src1 = "(x==5) ? (y=2) : (z-=7)";
            CondExprNode ast1 = this.GenerateAST(src1).As<CondExprNode>();
            Assert.That(ast1.Condition.GetText(), Is.EqualTo("(x == 5)"));
            Assert.That(ast1.ThenExpression.GetText(), Is.EqualTo("(y = 2)"));
            Assert.That(ast1.ElseExpression.GetText(), Is.EqualTo("(z -= 7)"));
        }

        [Test]
        public void TestCreator()
        {
            string src1 = "new int[3]";
            ArrAccessExprNode ast1 = this.GenerateAST(src1).As<ArrAccessExprNode>();
            Assert.That(ast1.Array.GetText(), Is.EqualTo("int"));
            Assert.That(ast1.IndexExpression.GetText(), Is.EqualTo("3"));
            Assert.That(ast1.GetText(), Is.EqualTo("int[3]"));

            string src2 = "new Point<int>[3]";
            ArrAccessExprNode ast2 = this.GenerateAST(src2).As<ArrAccessExprNode>();
            Assert.That(ast2.Array.GetText(), Is.EqualTo("Point<int>"));
            Assert.That(ast2.IndexExpression.GetText(), Is.EqualTo("3"));
            Assert.That(ast2.GetText(), Is.EqualTo("Point<int>[3]"));

            string src3 = "new Integer(3)";
            FuncCallExprNode ast3 = this.GenerateAST(src3).As<FuncCallExprNode>();
            Assert.That(ast3.Identifier, Is.EqualTo("Integer"));
            Assert.That(ast3.TemplateArguments.GetText(), Is.EqualTo(""));
            Assert.That(ast3.Arguments.GetText(), Is.EqualTo("3"));
            Assert.That(ast3.GetText(), Is.EqualTo("Integer<>(3)"));

            string src4 = "new Point<int>(3)";
            FuncCallExprNode ast4 = this.GenerateAST(src4).As<FuncCallExprNode>();
            Assert.That(ast4.Identifier, Is.EqualTo("Point<int>"));
            Assert.That(ast4.TemplateArguments.GetText(), Is.EqualTo(""));
            Assert.That(ast4.Arguments.GetText(), Is.EqualTo("3"));
            Assert.That(ast4.GetText(), Is.EqualTo("Point<int><>(3)"));

            string src5 = "new <string>Point<int>(3)";
            FuncCallExprNode ast5 = this.GenerateAST(src5).As<FuncCallExprNode>();
            Assert.That(ast5.Identifier, Is.EqualTo("Point<int>"));
            Assert.That(ast5.TemplateArguments.GetText(), Is.EqualTo("string"));
            Assert.That(ast5.Arguments.GetText(), Is.EqualTo("3"));
            Assert.That(ast5.GetText(), Is.EqualTo("Point<int><string>(3)"));
        }

        [Test]
        public void TestUnaryExpr()
        {
            string src1 = "!a";
            UnaryExprNode ast1 = this.GenerateAST(src1).As<UnaryExprNode>();
            Assert.That(ast1.Operator.GetText(), Is.EqualTo("!"));
            Assert.That(ast1.Operand.GetText(), Is.EqualTo("a"));
            Assert.That(ast1.GetText(), Is.EqualTo("!(a)"));

            string src2 = "~(a||b)";
            UnaryExprNode ast2 = this.GenerateAST(src2).As<UnaryExprNode>();
            Assert.That(ast2.Operator.GetText(), Is.EqualTo("~"));
            Assert.That(ast2.Operand.GetText(), Is.EqualTo("(a || b)"));
            Assert.That(ast2.GetText(), Is.EqualTo("~((a || b))"));

            string src3 = "a++";
            IncExprNode ast3 = this.GenerateAST(src3).As<IncExprNode>();
            Assert.That(ast3.Expr.GetText(), Is.EqualTo("a"));
            Assert.That(ast3.GetText(), Is.EqualTo("a++"));

            string src4 = "--a";
            DecExprNode ast4 = this.GenerateAST(src4).As<DecExprNode>();
            Assert.That(ast4.Expr.GetText(), Is.EqualTo("a"));
            Assert.That(ast4.GetText(), Is.EqualTo("a--"));
        }

        [Test]
        public void TestArthmExpr()
        {
            string src1 = "a+b";
            ArithmExprNode ast1 = this.GenerateAST(src1).As<ArithmExprNode>();
            Assert.That(ast1.Operator.GetText(), Is.EqualTo("+"));
            Assert.That(ast1.LeftOperand.GetText(), Is.EqualTo("a"));
            Assert.That(ast1.RightOperand.GetText(), Is.EqualTo("b"));
            Assert.That(ast1.GetText(), Is.EqualTo("(a + b)"));

            string src2 = "a%b";
            ArithmExprNode ast2 = this.GenerateAST(src2).As<ArithmExprNode>();
            Assert.That(ast2.Operator.GetText(), Is.EqualTo("%"));
            Assert.That(ast2.LeftOperand.GetText(), Is.EqualTo("a"));
            Assert.That(ast2.RightOperand.GetText(), Is.EqualTo("b"));
            Assert.That(ast2.GetText(), Is.EqualTo("(a % b)"));

            string src3 = "a*(b/c)";
            ArithmExprNode ast3 = this.GenerateAST(src3).As<ArithmExprNode>();
            Assert.That(ast3.Operator.GetText(), Is.EqualTo("*"));
            Assert.That(ast3.LeftOperand.GetText(), Is.EqualTo("a"));
            Assert.That(ast3.RightOperand.GetText(), Is.EqualTo("(b / c)"));
            Assert.That(ast3.RightOperand.As<ArithmExprNode>().Operator.GetText(), Is.EqualTo("/"));
            Assert.That(ast3.GetText(), Is.EqualTo("(a * (b / c))"));

            string src4 = "(a-b)%c";
            ArithmExprNode ast4 = this.GenerateAST(src4).As<ArithmExprNode>();
            Assert.That(ast4.Operator.GetText(), Is.EqualTo("%"));
            Assert.That(ast4.LeftOperand.GetText(), Is.EqualTo("(a - b)"));
            Assert.That(ast4.RightOperand.GetText(), Is.EqualTo("c"));
            Assert.That(ast4.LeftOperand.As<ArithmExprNode>().Operator.GetText(), Is.EqualTo("-"));
            Assert.That(ast4.GetText(), Is.EqualTo("((a - b) % c)"));

            string src5 = "a << b";
            ArithmExprNode ast5 = this.GenerateAST(src5).As<ArithmExprNode>();
            Assert.That(ast5.Operator.GetText(), Is.EqualTo("<<"));
            Assert.That(ast5.LeftOperand.GetText(), Is.EqualTo("a"));
            Assert.That(ast5.RightOperand.GetText(), Is.EqualTo("b"));
            Assert.That(ast5.GetText(), Is.EqualTo("(a << b)"));

            string src6 = "a >> b";
            ArithmExprNode ast6 = this.GenerateAST(src6).As<ArithmExprNode>();
            Assert.That(ast6.Operator.GetText(), Is.EqualTo(">>"));
            Assert.That(ast6.LeftOperand.GetText(), Is.EqualTo("a"));
            Assert.That(ast6.RightOperand.GetText(), Is.EqualTo("b"));
            Assert.That(ast6.GetText(), Is.EqualTo("(a >> b)"));
        }

        [Test]
        public void TestLogicExpr()
        {
            string src1 = "a&&b";
            LogicExprNode ast1 = this.GenerateAST(src1).As<LogicExprNode>();
            Assert.That(ast1.Operator.GetText(), Is.EqualTo("&&"));
            Assert.That(ast1.LeftOperand.GetText(), Is.EqualTo("a"));
            Assert.That(ast1.RightOperand.GetText(), Is.EqualTo("b"));
            Assert.That(ast1.GetText(), Is.EqualTo("(a && b)"));

            string src2 = "a||b";
            LogicExprNode ast2 = this.GenerateAST(src2).As<LogicExprNode>();
            Assert.That(ast2.Operator.GetText(), Is.EqualTo("||"));
            Assert.That(ast2.LeftOperand.GetText(), Is.EqualTo("a"));
            Assert.That(ast2.RightOperand.GetText(), Is.EqualTo("b"));
            Assert.That(ast2.GetText(), Is.EqualTo("(a || b)"));
        }

        [Test]
        public void TestRelationalExpr()
        {
            string src1 = "a>=b";
            RelExprNode ast1 = this.GenerateAST(src1).As<RelExprNode>();
            Assert.That(ast1.Operator.GetText(), Is.EqualTo(">="));
            Assert.That(ast1.LeftOperand.GetText(), Is.EqualTo("a"));
            Assert.That(ast1.RightOperand.GetText(), Is.EqualTo("b"));
            Assert.That(ast1.GetText(), Is.EqualTo("(a >= b)"));

            string src2 = "a<=b";
            RelExprNode ast2 = this.GenerateAST(src2).As<RelExprNode>();
            Assert.That(ast2.Operator.GetText(), Is.EqualTo("<="));
            Assert.That(ast2.LeftOperand.GetText(), Is.EqualTo("a"));
            Assert.That(ast2.RightOperand.GetText(), Is.EqualTo("b"));
            Assert.That(ast2.GetText(), Is.EqualTo("(a <= b)"));

            string src3 = "a==b";
            RelExprNode ast3 = this.GenerateAST(src3).As<RelExprNode>();
            Assert.That(ast3.Operator.GetText(), Is.EqualTo("=="));
            Assert.That(ast3.LeftOperand.GetText(), Is.EqualTo("a"));
            Assert.That(ast3.RightOperand.GetText(), Is.EqualTo("b"));
            Assert.That(ast3.GetText(), Is.EqualTo("(a == b)"));

            string src4 = "a!=b";
            RelExprNode ast4 = this.GenerateAST(src4).As<RelExprNode>();
            Assert.That(ast4.Operator.GetText(), Is.EqualTo("!="));
            Assert.That(ast4.LeftOperand.GetText(), Is.EqualTo("a"));
            Assert.That(ast4.RightOperand.GetText(), Is.EqualTo("b"));
            Assert.That(ast4.GetText(), Is.EqualTo("(a != b)"));

            string src5 = "a<b";
            RelExprNode ast5 = this.GenerateAST(src5).As<RelExprNode>();
            Assert.That(ast5.Operator.GetText(), Is.EqualTo("<"));
            Assert.That(ast5.LeftOperand.GetText(), Is.EqualTo("a"));
            Assert.That(ast5.RightOperand.GetText(), Is.EqualTo("b"));
            Assert.That(ast5.GetText(), Is.EqualTo("(a < b)"));

            string src6 = "a>b";
            RelExprNode ast6 = this.GenerateAST(src6).As<RelExprNode>();
            Assert.That(ast6.Operator.GetText(), Is.EqualTo(">"));
            Assert.That(ast6.LeftOperand.GetText(), Is.EqualTo("a"));
            Assert.That(ast6.RightOperand.GetText(), Is.EqualTo("b"));
            Assert.That(ast6.GetText(), Is.EqualTo("(a > b)"));
        }

        [Test]
        public void TestAssignExpr()
        {
            string src1 = "a=b";
            AssignExprNode ast1 = this.GenerateAST(src1).As<AssignExprNode>();
            Assert.That(ast1.Operator.GetText(), Is.EqualTo("="));
            Assert.That(ast1.LeftOperand.GetText(), Is.EqualTo("a"));
            Assert.That(ast1.RightOperand.GetText(), Is.EqualTo("b"));
            Assert.That(ast1.GetText(), Is.EqualTo("(a = b)"));

            string src2 = "a+=b";
            AssignExprNode ast2 = this.GenerateAST(src2).As<AssignExprNode>();
            Assert.That(ast2.Operator.GetText(), Is.EqualTo("+="));
            Assert.That(ast2.LeftOperand.GetText(), Is.EqualTo("a"));
            Assert.That(ast2.RightOperand.GetText(), Is.EqualTo("b"));
            Assert.That(ast2.GetText(), Is.EqualTo("(a += b)"));

            string src3 = "a*=b";
            AssignExprNode ast3 = this.GenerateAST(src3).As<AssignExprNode>();
            Assert.That(ast3.Operator.GetText(), Is.EqualTo("*="));
            Assert.That(ast3.LeftOperand.GetText(), Is.EqualTo("a"));
            Assert.That(ast3.RightOperand.GetText(), Is.EqualTo("b"));
            Assert.That(ast3.GetText(), Is.EqualTo("(a *= b)"));

            string src4 = "a|=b";
            AssignExprNode ast4 = this.GenerateAST(src4).As<AssignExprNode>();
            Assert.That(ast4.Operator.GetText(), Is.EqualTo("|="));
            Assert.That(ast4.LeftOperand.GetText(), Is.EqualTo("a"));
            Assert.That(ast4.RightOperand.GetText(), Is.EqualTo("b"));
            Assert.That(ast4.GetText(), Is.EqualTo("(a |= b)"));

            string src5 = "a^=b";
            AssignExprNode ast5 = this.GenerateAST(src5).As<AssignExprNode>();
            Assert.That(ast5.Operator.GetText(), Is.EqualTo("^="));
            Assert.That(ast5.LeftOperand.GetText(), Is.EqualTo("a"));
            Assert.That(ast5.RightOperand.GetText(), Is.EqualTo("b"));
            Assert.That(ast5.GetText(), Is.EqualTo("(a ^= b)"));

            string src6 = "a>>=b";
            AssignExprNode ast6 = this.GenerateAST(src6).As<AssignExprNode>();
            Assert.That(ast6.Operator.GetText(), Is.EqualTo(">>="));
            Assert.That(ast6.LeftOperand.GetText(), Is.EqualTo("a"));
            Assert.That(ast6.RightOperand.GetText(), Is.EqualTo("b"));
            Assert.That(ast6.GetText(), Is.EqualTo("(a >>= b)"));
        }

        [Test]
        public void TestMethodCall() 
        {
            string src1 = "f(x)";
            FuncCallExprNode ast1 = this.GenerateAST(src1).As<FuncCallExprNode>();
            Assert.That(ast1.Identifier, Is.EqualTo("f"));
            Assert.That(ast1.TemplateArguments.GetText(), Is.EqualTo(""));
            Assert.That(ast1.Arguments.GetText(), Is.EqualTo("x"));
            Assert.That(ast1.GetText(), Is.EqualTo("f<>(x)"));

            string src2 = "func(x,2)";
            FuncCallExprNode ast2 = this.GenerateAST(src2).As<FuncCallExprNode>();
            Assert.That(ast2.Identifier, Is.EqualTo("func"));
            Assert.That(ast2.TemplateArguments.GetText(), Is.EqualTo(""));
            Assert.That(ast2.Arguments.GetText(), Is.EqualTo("x, 2"));
            Assert.That(ast2.GetText(), Is.EqualTo("func<>(x, 2)"));

            string src3 = "this(x,9.8,a)";
            FuncCallExprNode ast3 = this.GenerateAST(src3).As<FuncCallExprNode>();
            Assert.That(ast3.Identifier, Is.EqualTo("this"));
            Assert.That(ast3.TemplateArguments.GetText(), Is.EqualTo(""));
            Assert.That(ast3.Arguments.GetText(), Is.EqualTo("x, 9.8, a"));
            Assert.That(ast3.GetText(), Is.EqualTo("this<>(x, 9.8, a)"));

            string src4 = "super(a)";
            FuncCallExprNode ast4 = this.GenerateAST(src4).As<FuncCallExprNode>();
            Assert.That(ast4.Identifier, Is.EqualTo("super"));
            Assert.That(ast4.TemplateArguments.GetText(), Is.EqualTo(""));
            Assert.That(ast4.Arguments.GetText(), Is.EqualTo("a"));
            Assert.That(ast4.GetText(), Is.EqualTo("super<>(a)"));
        }

        [Test]
        public void TestExprRest()
        {
            string src1 = "Point::new";
            FuncCallExprNode ast1 = this.GenerateAST(src1).As<FuncCallExprNode>();
            Assert.That(ast1.Identifier, Is.EqualTo("Point"));
            Assert.That(ast1.GetText(), Is.EqualTo("Point<>()"));

            string src2 = "Point<int>::new";
            FuncCallExprNode ast2 = this.GenerateAST(src2).As<FuncCallExprNode>();
            Assert.That(ast2.Identifier, Is.EqualTo("Point<int>"));
            Assert.That(ast2.GetText(), Is.EqualTo("Point<int><>()"));

            string src3 = "super.f(a)";
            FuncCallExprNode ast3 = this.GenerateAST(src3).As<FuncCallExprNode>();
            Assert.That(ast3.Identifier, Is.EqualTo("f"));
            Assert.That(ast3.TemplateArguments.GetText(), Is.EqualTo(""));
            Assert.That(ast3.Arguments.GetText(), Is.EqualTo("a"));
            Assert.That(ast3.GetText(), Is.EqualTo("f<>(a)"));
        }

        protected override ASTNode GenerateAST(string src)
            => new JavaASTBuilder().BuildFromSource(src, p => p.expression());
    }
}
