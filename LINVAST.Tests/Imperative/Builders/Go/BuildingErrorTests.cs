using System;
using System.IO;
using Antlr4.Runtime;
using LINVAST.Exceptions;
using LINVAST.Imperative;
using LINVAST.Imperative.Builders.C;
using LINVAST.Imperative.Builders.Go;
using LINVAST.Nodes;
using LINVAST.Tests.Imperative.Builders.Common;
using NUnit.Framework;

namespace LINVAST.Tests.Imperative.Builders.Go
{
    internal sealed class BuildingErrorTests : BuildingErrorTestsBase
    {
        [Test]
        public void SourceNotFoundTest()
        {
            Assert.Throws<FileNotFoundException>(() => new ImperativeASTFactory().BuildFromFile("404.go"));
        }

        [Test]
        public void InvalidDeclarationTests()
        {
            this.AssertThrows<SyntaxErrorException>("func f int { }", p => ((GoParser)p).functionDecl());
            this.AssertThrows<SyntaxErrorException>("func ()", p => ((GoParser)p).functionDecl());
            this.AssertThrows<SyntaxErrorException>("func f(0, x) int { }", p => ((GoParser)p).functionDecl());
            this.AssertThrows<SyntaxErrorException>("func f(3) int { }", p => ((GoParser)p).functionDecl());
            this.AssertThrows<SyntaxErrorException>("func f(int x[]) int { }", p => ((GoParser)p).functionDecl());
            this.AssertThrows<SyntaxErrorException>("func f[](x int,) int { }", p => ((GoParser)p).functionDecl());
            this.AssertThrows<SyntaxErrorException>("func f(int x, int y,,) int { }", p => ((GoParser)p).functionDecl());
            this.AssertThrows<SyntaxErrorException>("var x = ;;", p => ((GoParser)p).varDecl());
            // this.AssertThrows<SyntaxErrorException>("var x int = .3, 2..", p => ((GoParser)p).varDecl());
            this.AssertThrows<SyntaxErrorException>("var x int = ..3", p => ((GoParser)p).varDecl());
            this.AssertThrows<SyntaxErrorException>("var x int = ()", p => ((GoParser)p).varDecl());
            this.AssertThrows<SyntaxErrorException>("\"math\"", p => ((GoParser)p).importDecl());
            this.AssertThrows<SyntaxErrorException>("import", p => ((GoParser)p).importDecl());
            this.AssertThrows<SyntaxErrorException>("import * \"math\" ", p => ((GoParser)p).importDecl());
        }

        [Test]
        public void InvalidIfStatementTests()
        {
            this.AssertThrows<SyntaxErrorException>("if x ", p=>((GoParser)p).ifStmt());
            this.AssertThrows<SyntaxErrorException>("if {x} {} else {} ", p=>((GoParser)p).ifStmt());
            this.AssertThrows<SyntaxErrorException>("if x then { } else { } ", p=>((GoParser)p).ifStmt());
            this.AssertThrows<SyntaxErrorException>("if (x > 1 {} ", p=>((GoParser)p).ifStmt());
            this.AssertThrows<SyntaxErrorException>("if 1 ;; else ; ", p=>((GoParser)p).ifStmt());
        }

        [Test]
        public void InvalidForStatementTests()
        {
            this.AssertThrows<SyntaxErrorException>("for (x := 0; x < 5; x++) {}", p=>((GoParser)p).forStmt());
            this.AssertThrows<NotImplementedException>("for x := 0; x < 5; x++ {}", p=>((GoParser)p).forStmt());
            this.AssertThrows<SyntaxErrorException>("for (;;;;){}", p=>((GoParser)p).forStmt());
        }


        protected override ASTNode GenerateAST(string src)
            => throw new NotImplementedException("Use GenerateAST(string, Func<Parser, ParserRuleContext) instead; " +
                                                 "building from source always requires a package declaration so it always throws");

        protected override ASTNode GenerateAST(string src, Func<Parser, ParserRuleContext> entryProvider) => 
            new GoASTBuilder().BuildFromSource(src, entryProvider);
    }
}
