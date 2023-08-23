using LINVAST.Builders;
using LINVAST.Nodes;

namespace LINVAST.Imperative.Builders.Go
{
    public sealed partial class GoASTBuilder : GoParserBaseVisitor<ASTNode>, IASTBuilder<GoParser>
    {
        public override ASTNode VisitType_(GoParser.Type_Context context) => base.VisitType_(context);

        public override ASTNode VisitTypeName(GoParser.TypeNameContext context) => base.VisitTypeName(context);

        public override ASTNode VisitTypeLit(GoParser.TypeLitContext context) => base.VisitTypeLit(context);

        public override ASTNode VisitTypeList(GoParser.TypeListContext context) => base.VisitTypeList(context);

        public override ASTNode VisitArrayType(GoParser.ArrayTypeContext context) => base.VisitArrayType(context);

        public override ASTNode VisitArrayLength(GoParser.ArrayLengthContext context) => base.VisitArrayLength(context);

        public override ASTNode VisitStructType(GoParser.StructTypeContext context) => base.VisitStructType(context);

        public override ASTNode VisitPointerType(GoParser.PointerTypeContext context) => base.VisitPointerType(context);

        public override ASTNode VisitResult(GoParser.ResultContext context) => base.VisitResult(context);

        public override ASTNode VisitFunctionType(GoParser.FunctionTypeContext context) => base.VisitFunctionType(context);

        public override ASTNode VisitInterfaceType(GoParser.InterfaceTypeContext context) => base.VisitInterfaceType(context);

        public override ASTNode VisitSliceType(GoParser.SliceTypeContext context) => base.VisitSliceType(context);

        public override ASTNode VisitMapType(GoParser.MapTypeContext context) => base.VisitMapType(context);

        public override ASTNode VisitChannelType(GoParser.ChannelTypeContext context) => base.VisitChannelType(context);

        public override ASTNode VisitConversion(GoParser.ConversionContext context) => base.VisitConversion(context);

        public override ASTNode VisitEmbeddedField(GoParser.EmbeddedFieldContext context) => base.VisitEmbeddedField(context);

        public override ASTNode VisitTypeSpec(GoParser.TypeSpecContext context) => base.VisitTypeSpec(context);

        public override ASTNode VisitNonNamedType(GoParser.NonNamedTypeContext context) => base.VisitNonNamedType(context);
    }
}