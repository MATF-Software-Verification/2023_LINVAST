using System;
using LINVAST.Builders;
using LINVAST.Imperative.Nodes;
using LINVAST.Nodes;
using System.Linq;


namespace LINVAST.Imperative.Builders.Go
{
    public sealed partial class GoASTBuilder : GoParserBaseVisitor<ASTNode>, IASTBuilder<GoParser>
    {
        public override ASTNode VisitType_(GoParser.Type_Context context)
        {
            if (context.typeName() is not null) {
                return this.Visit(context.typeName()).As<TypeNameNode>();
            }
            if (context.typeLit() is not null) {
                return this.Visit(context.typeLit()).As<TypeNameNode>();
            }

            return this.Visit(context.type_()).As<TypeNameNode>();
        }

        public override ASTNode VisitTypeName(GoParser.TypeNameContext context)
        {
            if (context.qualifiedIdent() is not null) {
                return this.Visit(context.qualifiedIdent()).As<TypeNameNode>();
            }
            string name = context.IDENTIFIER().GetText();
            return new TypeNameNode(context.Start.Line, name);
        }

        public override ASTNode VisitTypeLit(GoParser.TypeLitContext context) => this.Visit(context.children.Single());

        //TODO
        public override ASTNode VisitTypeList(GoParser.TypeListContext context)
        {
            if (context.type_() is not null) {
                return new TypeNameListNode(context.Start.Line, context.type_().Select(t => this.Visit(t).As<TypeNameNode>()));
            };

            return new TypeNameListNode(context.Start.Line);
        }

        public override ASTNode VisitNonNamedType(GoParser.NonNamedTypeContext context)
        {
            if (context.typeLit() is not null) {
                return this.Visit(context.typeLit()).As<TypeNameNode>();
            }

            return this.Visit(context.nonNamedType()).As<TypeNameNode>();
        }

        public override ASTNode VisitArrayLength(GoParser.ArrayLengthContext context) => this.Visit(context.expression()).As<ExprNode>();


        /*public override ASTNode VisitArrayType(GoParser.ArrayTypeContext context)
        {
            ExprStatNode length = this.Visit(context.arrayLength()).As<ExprStatNode>();
            TypeNode elem = this.Visit(context.elementType()).As<TypeNode>();
            throw new NotImplementedException("arrayType");
        }*/


        /*public override ASTNode VisitStructType(GoParser.StructTypeContext context)
        {
            throw new Exception("struct");

        }*/

        /*public override ASTNode VisitPointerType(GoParser.PointerTypeContext context)
        {
            return this.Visit(context.type_()).As<TypeNode>();
        }*/

        public override ASTNode VisitResult(GoParser.ResultContext context)
        {
            if (context.parameters() is not null) {
                return this.Visit(context.parameters());
            }
            return this.Visit(context.type_());
        }

        //public override ASTNode VisitFunctionType(GoParser.FunctionTypeContext context) => this.Visit(context.signature());

        /*public override ASTNode VisitInterfaceType(GoParser.InterfaceTypeContext context)
        {

            //return new InterfaceNode(context.Start.Line, )
            throw new NotImplementedException("interfaceType");
        }*/

        //public override ASTNode VisitSliceType(GoParser.SliceTypeContext context) => this.Visit(context.elementType()).As<TypeNode>();

        /*public override ASTNode VisitMapType(GoParser.MapTypeContext context)
        {
            TypeNode type = this.Visit(context.type_()).As<TypeNode>();
            TypeNode elem = this.Visit(context.elementType()).As<TypeNode>();

            throw new NotImplementedException("Map");
        public override ASTNode VisitInterfaceType(GoParser.InterfaceTypeContext context) => base.VisitInterfaceType(context);
        
        public override ASTNode VisitMethodSpec(GoParser.MethodSpecContext context) => base.VisitMethodSpec(context);

        }*/
        //public override ASTNode VisitChannelType(GoParser.ChannelTypeContext context) => this.Visit(context.elementType()).As<TypeNode>();

        /*public override ASTNode VisitConversion(GoParser.ConversionContext context)
        {
            TypeNode type = this.Visit(context.nonNamedType()).As<TypeNameNode>();
            throw new NotImplementedException("conversion");
        }*/

        //jok
        //public override ASTNode VisitEmbeddedField(GoParser.EmbeddedFieldContext context) => base.VisitEmbeddedField(context);
        //public override ASTNode VisitFieldDecl(GoParser.FieldDeclContext context) => base.VisitFieldDecl(context);

        /*public override ASTNode VisitTypeSpec(GoParser.TypeSpecContext context)
        {

            string name = context.IDENTIFIER().GetText();
            this.Visit(context.type_()).As<TypeNode>();

            throw new NotImplementedException("typespec");

        }*/
    }
}