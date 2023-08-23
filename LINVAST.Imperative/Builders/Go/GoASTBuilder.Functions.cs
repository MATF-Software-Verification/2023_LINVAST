using LINVAST.Builders;
using LINVAST.Nodes;

namespace LINVAST.Imperative.Builders.Go
{
    public sealed partial class GoASTBuilder : GoParserBaseVisitor<ASTNode>, IASTBuilder<GoParser>
    {
        public override ASTNode VisitArguments(GoParser.ArgumentsContext context) => base.VisitArguments(context);

        public override ASTNode VisitParameters(GoParser.ParametersContext context) => base.VisitParameters(context);
        
        public override ASTNode VisitParameterDecl(GoParser.ParameterDeclContext context) => base.VisitParameterDecl(context);
        
        public override ASTNode VisitFunctionDecl(GoParser.FunctionDeclContext context) => base.VisitFunctionDecl(context);
        
        public override ASTNode VisitSignature(GoParser.SignatureContext context) => base.VisitSignature(context);
        
        # region method-specific stuff
        public override ASTNode VisitMethodDecl(GoParser.MethodDeclContext context) => base.VisitMethodDecl(context);

        public override ASTNode VisitReceiver(GoParser.ReceiverContext context) => base.VisitReceiver(context);

        public override ASTNode VisitReceiverType(GoParser.ReceiverTypeContext context) => base.VisitReceiverType(context);

        public override ASTNode VisitMethodSpec(GoParser.MethodSpecContext context) => base.VisitMethodSpec(context);
        #endregion
    }
}