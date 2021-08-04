using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr4.Runtime.Misc;
using LINVAST.Builders;
using LINVAST.Exceptions;
using LINVAST.Imperative.Nodes;
using LINVAST.Nodes;
using static LINVAST.Imperative.Builders.Java.JavaParser;

namespace LINVAST.Imperative.Builders.Java
{
    public sealed partial class JavaASTBuilder : JavaBaseVisitor<ASTNode>, IASTBuilder<JavaParser>
    {

        // package and import declarations:

        public override ASTNode VisitPackageDeclaration([NotNull] PackageDeclarationContext ctx)
            => throw new NotImplementedException("Package declarations.");

        public override ASTNode VisitImportDeclaration([NotNull] ImportDeclarationContext ctx)
        {
            if (ctx.STATIC() is { })
                throw new NotImplementedException("static import");

            var directive = new StringBuilder(this.Visit(ctx.qualifiedName()).As<IdNode>().GetText());
            if (ctx.MUL() is { })
                directive.Append(".*");

            return new ImportNode(ctx.Start.Line, directive.ToString());
        }

        

        // other (overriden just for the purposes of testing the above methods):

        public override ASTNode VisitQualifiedName([NotNull] QualifiedNameContext ctx)
            => new IdNode(ctx.Start.Line,
                string.Join('.', ctx.IDENTIFIER().Select(id => id.GetText())));


    }
}
