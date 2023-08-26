using System.Linq;
using LINVAST.Builders;
using LINVAST.Imperative.Nodes;
using LINVAST.Nodes;

namespace LINVAST.Imperative.Builders.Go
{
    public sealed partial class GoASTBuilder : GoParserBaseVisitor<ASTNode>, IASTBuilder<GoParser>
    {
        public override ASTNode VisitPackageClause(GoParser.PackageClauseContext context) => base.VisitPackageClause(context);
        
        public override ASTNode VisitDeclaration(GoParser.DeclarationContext context) => base.VisitDeclaration(context);

        public override ASTNode VisitConstDecl(GoParser.ConstDeclContext context) => base.VisitConstDecl(context);

        public override ASTNode VisitFieldDecl(GoParser.FieldDeclContext context) => base.VisitFieldDecl(context);

        public override ASTNode VisitImportDecl(GoParser.ImportDeclContext context) => base.VisitImportDecl(context);

        public override ASTNode VisitTypeDecl(GoParser.TypeDeclContext context) => base.VisitTypeDecl(context);

        public override ASTNode VisitVarDecl(GoParser.VarDeclContext context) => base.VisitVarDecl(context);

        public override ASTNode VisitShortVarDecl(GoParser.ShortVarDeclContext context) => base.VisitShortVarDecl(context);

        public override ASTNode VisitConstSpec(GoParser.ConstSpecContext context) => base.VisitConstSpec(context);

        public override ASTNode VisitIdentifierList(GoParser.IdentifierListContext context) =>
            new DeclListNode(context.Start.Line, context.IDENTIFIER().Select(i =>
                new VarDeclNode(context.Start.Line, new IdNode(context.Start.Line, i.GetText()))));

        public override ASTNode VisitVarSpec(GoParser.VarSpecContext context) => base.VisitVarSpec(context);

        public override ASTNode VisitImportPath(GoParser.ImportPathContext context) => base.VisitImportPath(context);

        public override ASTNode VisitImportSpec(GoParser.ImportSpecContext context) => base.VisitImportSpec(context);
    }
}