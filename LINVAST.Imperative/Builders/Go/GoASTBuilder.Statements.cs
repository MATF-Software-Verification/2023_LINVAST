using LINVAST.Builders;
using LINVAST.Nodes;

namespace LINVAST.Imperative.Builders.Go
{
    public sealed partial class GoASTBuilder : GoParserBaseVisitor<ASTNode>, IASTBuilder<GoParser>
    {
        public override ASTNode VisitStatement(GoParser.StatementContext context) => base.VisitStatement(context);

        public override ASTNode VisitStatementList(GoParser.StatementListContext context) => base.VisitStatementList(context);
        
        public override ASTNode VisitAssignment(GoParser.AssignmentContext context) => base.VisitAssignment(context);

        public override ASTNode VisitAssign_op(GoParser.Assign_opContext context) => base.VisitAssign_op(context);

        public override ASTNode VisitBreakStmt(GoParser.BreakStmtContext context) => base.VisitBreakStmt(context);

        public override ASTNode VisitContinueStmt(GoParser.ContinueStmtContext context) => base.VisitContinueStmt(context);

        public override ASTNode VisitDeferStmt(GoParser.DeferStmtContext context) => base.VisitDeferStmt(context);

        public override ASTNode VisitEmptyStmt(GoParser.EmptyStmtContext context) => base.VisitEmptyStmt(context);

        public override ASTNode VisitFallthroughStmt(GoParser.FallthroughStmtContext context) => base.VisitFallthroughStmt(context);

        public override ASTNode VisitForStmt(GoParser.ForStmtContext context) => base.VisitForStmt(context);

        public override ASTNode VisitForClause(GoParser.ForClauseContext context) => base.VisitForClause(context);

        public override ASTNode VisitRangeClause(GoParser.RangeClauseContext context) => base.VisitRangeClause(context);

        public override ASTNode VisitGoStmt(GoParser.GoStmtContext context) => base.VisitGoStmt(context);

        public override ASTNode VisitGotoStmt(GoParser.GotoStmtContext context) => base.VisitGotoStmt(context);

        public override ASTNode VisitIfStmt(GoParser.IfStmtContext context) => base.VisitIfStmt(context);

        public override ASTNode VisitLabeledStmt(GoParser.LabeledStmtContext context) => base.VisitLabeledStmt(context);
        
        public override ASTNode VisitRecvStmt(GoParser.RecvStmtContext context) => base.VisitRecvStmt(context);

        public override ASTNode VisitReturnStmt(GoParser.ReturnStmtContext context) => base.VisitReturnStmt(context);

        public override ASTNode VisitSelectStmt(GoParser.SelectStmtContext context) => base.VisitSelectStmt(context);

        public override ASTNode VisitCommCase(GoParser.CommCaseContext context) => base.VisitCommCase(context);

        public override ASTNode VisitCommClause(GoParser.CommClauseContext context) => base.VisitCommClause(context);

        public override ASTNode VisitSendStmt(GoParser.SendStmtContext context) => base.VisitSendStmt(context);

        public override ASTNode VisitSimpleStmt(GoParser.SimpleStmtContext context) => base.VisitSimpleStmt(context);

        public override ASTNode VisitSwitchStmt(GoParser.SwitchStmtContext context) => base.VisitSwitchStmt(context);

        public override ASTNode VisitIncDecStmt(GoParser.IncDecStmtContext context) => base.VisitIncDecStmt(context);

        public override ASTNode VisitTypeSwitchStmt(GoParser.TypeSwitchStmtContext context) => base.VisitTypeSwitchStmt(context);
        
        public override ASTNode VisitExpressionStmt(GoParser.ExpressionStmtContext context) => base.VisitExpressionStmt(context);

        public override ASTNode VisitExprSwitchStmt(GoParser.ExprSwitchStmtContext context) => base.VisitExprSwitchStmt(context);
        
        public override ASTNode VisitExprCaseClause(GoParser.ExprCaseClauseContext context) => base.VisitExprCaseClause(context);

        public override ASTNode VisitExprSwitchCase(GoParser.ExprSwitchCaseContext context) => base.VisitExprSwitchCase(context);

        public override ASTNode VisitTypeSwitchCase(GoParser.TypeSwitchCaseContext context) => base.VisitTypeSwitchCase(context);
        
        public override ASTNode VisitTypeSwitchGuard(GoParser.TypeSwitchGuardContext context) => base.VisitTypeSwitchGuard(context);

        public override ASTNode VisitTypeCaseClause(GoParser.TypeCaseClauseContext context) => base.VisitTypeCaseClause(context);

        public override ASTNode VisitBlock(GoParser.BlockContext context) => base.VisitBlock(context);
    }
}