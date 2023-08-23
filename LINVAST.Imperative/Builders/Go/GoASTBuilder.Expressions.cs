using LINVAST.Builders;
using LINVAST.Nodes;

namespace LINVAST.Imperative.Builders.Go
{
    public sealed partial class GoASTBuilder : GoParserBaseVisitor<ASTNode>, IASTBuilder<GoParser>
    {
        public override ASTNode VisitExpression(GoParser.ExpressionContext context) => base.VisitExpression(context);

        public override ASTNode VisitExpressionList(GoParser.ExpressionListContext context) => base.VisitExpressionList(context);

        public override ASTNode VisitPrimaryExpr(GoParser.PrimaryExprContext context) => base.VisitPrimaryExpr(context);
        
        public override ASTNode VisitMethodExpr(GoParser.MethodExprContext context) => base.VisitMethodExpr(context);

        public override ASTNode VisitLiteral(GoParser.LiteralContext context) => base.VisitLiteral(context);

        public override ASTNode VisitLiteralType(GoParser.LiteralTypeContext context) => base.VisitLiteralType(context);

        public override ASTNode VisitLiteralValue(GoParser.LiteralValueContext context) => base.VisitLiteralValue(context);

        public override ASTNode VisitOperand(GoParser.OperandContext context) => base.VisitOperand(context);

        public override ASTNode VisitOperandName(GoParser.OperandNameContext context) => base.VisitOperandName(context);

        public override ASTNode VisitBasicLit(GoParser.BasicLitContext context) => base.VisitBasicLit(context);

        public override ASTNode VisitCompositeLit(GoParser.CompositeLitContext context) => base.VisitCompositeLit(context);

        public override ASTNode VisitElement(GoParser.ElementContext context) => base.VisitElement(context);

        public override ASTNode VisitElementList(GoParser.ElementListContext context) => base.VisitElementList(context);

        public override ASTNode VisitElementType(GoParser.ElementTypeContext context) => base.VisitElementType(context);

        public override ASTNode VisitKeyedElement(GoParser.KeyedElementContext context) => base.VisitKeyedElement(context);

        public override ASTNode VisitKey(GoParser.KeyContext context) => base.VisitKey(context);

        public override ASTNode VisitFunctionLit(GoParser.FunctionLitContext context) => base.VisitFunctionLit(context);

        public override ASTNode VisitQualifiedIdent(GoParser.QualifiedIdentContext context) => base.VisitQualifiedIdent(context);
        
        public override ASTNode VisitTypeAssertion(GoParser.TypeAssertionContext context) => base.VisitTypeAssertion(context);

        public override ASTNode VisitInteger(GoParser.IntegerContext context) => base.VisitInteger(context);

        public override ASTNode VisitString_(GoParser.String_Context context) => base.VisitString_(context);

        public override ASTNode VisitIndex(GoParser.IndexContext context) => base.VisitIndex(context);

        public override ASTNode VisitSlice_(GoParser.Slice_Context context) => base.VisitSlice_(context);
    }
}