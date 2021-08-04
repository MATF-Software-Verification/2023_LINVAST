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


        // class, enum, interface declarations:

        public override ASTNode VisitClassDeclaration([NotNull] ClassDeclarationContext ctx)
        {
            var identifier = new IdNode(ctx.Start.Line, ctx.IDENTIFIER().GetText());

            TypeNameListNode templateParams;
            if (ctx.typeParameters() is { } typeParamsCtx)
                templateParams = this.Visit(typeParamsCtx).As<TypeNameListNode>();
            else
                templateParams = new TypeNameListNode(ctx.Start.Line, new TypeNameNode[] { });

            IEnumerable<TypeNameNode> baseTypes;
            int baseTypesStartLine = ctx.Start.Line;
            if (ctx.typeList() is { } typeListCtx) {
                baseTypes = this.Visit(typeListCtx).As<TypeNameListNode>().Types;
                baseTypesStartLine = typeListCtx.Start.Line;
            }
            else baseTypes = new TypeNameNode[] { };
            if (ctx.typeType() is { } typeTypeCtx) {
                baseTypes = baseTypes.Append(this.Visit(typeTypeCtx).As<TypeNameNode>());
                baseTypesStartLine = typeTypeCtx.Start.Line;
            }

            IEnumerable<DeclStatNode> declarations;
            BlockStatNode block = this.Visit(ctx.classBody()).As<BlockStatNode>();
            declarations = block.Children.Select(c => c.As<DeclStatNode>());

            return new TypeDeclNode(ctx.Start.Line, identifier, templateParams,
                new TypeNameListNode(baseTypesStartLine, baseTypes),
                declarations);
        }

        public override ASTNode VisitEnumDeclaration([NotNull] EnumDeclarationContext ctx)
        {
            var identifier = new IdNode(ctx.Start.Line, ctx.IDENTIFIER().GetText());

            if (ctx.typeList() is { })
                throw new NotImplementedException("enum implements interface");

            IEnumerable<VarDeclNode> constants = new VarDeclNode[] { };
            if (ctx.enumConstants() is { }) {
                BlockStatNode block = this.Visit(ctx.enumConstants()).As<BlockStatNode>();
                constants = block.Children.Select(c => c.As<VarDeclNode>());
            }

            if (ctx.enumBodyDeclarations() is { })
                throw new NotImplementedException("enum class body");

            return new EnumDeclNode(ctx.Start.Line, identifier, constants);
        }

        public override ASTNode VisitEnumBodyDeclarations([NotNull] EnumBodyDeclarationsContext ctx)
            => throw new NotImplementedException("Enum class body");

        public override ASTNode VisitInterfaceDeclaration([NotNull] InterfaceDeclarationContext ctx)
        {
            var identifier = new IdNode(ctx.Start.Line, ctx.IDENTIFIER().GetText());

            TypeNameListNode? templateParams = null;
            if (ctx.typeParameters() is { })
                templateParams = this.Visit(ctx.typeParameters()).As<TypeNameListNode>();

            TypeNameListNode? baseTypes = null;
            if (ctx.typeList() is { })
                baseTypes = this.Visit(ctx.typeList()).As<TypeNameListNode>();

            IEnumerable<DeclStatNode> declarations;
            BlockStatNode block = this.Visit(ctx.interfaceBody()).As<BlockStatNode>();
            declarations = block.Children.Select(c => c.As<DeclStatNode>());

            return new TypeDeclNode(ctx.Start.Line, identifier,
                templateParams ?? new TypeNameListNode(ctx.Start.Line),
                baseTypes ?? new TypeNameListNode(ctx.Start.Line),
                declarations);
        }

        public override ASTNode VisitClassBodyDeclaration([NotNull] ClassBodyDeclarationContext ctx)
        {
            if (ctx.SEMI() is { })
                return new EmptyStatNode(ctx.Start.Line);

            if (ctx.STATIC() is { } || ctx.block() is { })
                throw new NotImplementedException("static- and non-static- blocks in a class");


            // we use private method ProcessModifier instead of overriding VisitModifier
            string modifiers = "";
            int? declSpecsStartLine = null;
            if (ctx.modifier() is { } modifierCtxList && modifierCtxList.Any()) {
                modifiers = string.Join(" ", modifierCtxList.Select(modCtx => this.ProcessModifier(modCtx)));
                declSpecsStartLine = modifierCtxList.First().Start.Line;
            }

            MemberDeclarationContext memberDeclCtx = ctx.memberDeclaration();

            DeclSpecsNode declSpecs;
            TypeNameNode? typeName = TypeName(memberDeclCtx);
            if (typeName is { }) { // if memberDeclaration is anything but constructor- or genericConstructor- Declaration
                declSpecsStartLine ??= typeName.Line;
                declSpecs = new DeclSpecsNode(declSpecsStartLine ?? ctx.Start.Line, modifiers, typeName);
            }
            else // if memberDeclaration is constructor- or genericConstructor- Declaration
                throw new NotImplementedException("constructors");

            DeclListNode declList;
            if (memberDeclCtx.fieldDeclaration() is { })
                declList = this.Visit(memberDeclCtx).As<DeclListNode>();
            else
                declList = new DeclListNode(memberDeclCtx.Start.Line, this.Visit(memberDeclCtx).As<DeclNode>());

            return new DeclStatNode(ctx.Start.Line, declSpecs, declList);

            TypeNameNode? TypeName([NotNull] MemberDeclarationContext ctx)
            {
                if (ctx.classDeclaration() is { } clsDeclCtx)
                    return new TypeNameNode(clsDeclCtx.Start.Line, clsDeclCtx.IDENTIFIER().GetText());

                if (ctx.interfaceDeclaration() is { } interfaceDeclCtx)
                    return new TypeNameNode(interfaceDeclCtx.Start.Line, interfaceDeclCtx.IDENTIFIER().GetText());

                if (ctx.enumDeclaration() is { } enumDeclCtx)
                    return new TypeNameNode(enumDeclCtx.Start.Line, enumDeclCtx.IDENTIFIER().GetText());

                if (ctx.annotationTypeDeclaration() is { })
                    throw new NotImplementedException("annotation type declaration");

                if (ctx.methodDeclaration() is { } methodDeclCtx)
                    return this.Visit(methodDeclCtx.typeTypeOrVoid()).As<TypeNameNode>();

                if (ctx.genericMethodDeclaration() is { } genMethDeclCtx)
                    return this.Visit(genMethDeclCtx.methodDeclaration().typeTypeOrVoid()).As<TypeNameNode>();

                if (ctx.fieldDeclaration() is { } fieldDeclCtx)
                    return this.Visit(fieldDeclCtx.typeType()).As<TypeNameNode>();

                if (ctx.constructorDeclaration() is { })
                    return null;

                if (ctx.genericConstructorDeclaration() is { })
                    return null;

                // unreachable path
                throw new SyntaxErrorException("Source file contained unexpected content");
            }
        }


        // class member declarations:

        public override ASTNode VisitMemberDeclaration([NotNull] MemberDeclarationContext ctx)
            => this.Visit(ctx.children.Single());

        public override ASTNode VisitMethodDeclaration([NotNull] MethodDeclarationContext ctx)
        {
            var identifier = new IdNode(ctx.Start.Line, ctx.IDENTIFIER().GetText());

            FuncParamsNode @params = this.Visit(ctx.formalParameters()).As<FuncParamsNode>();

            // brackets applies to the return type, historical reasons
            if (ctx.LBRACK().Length > 0)
                throw new NotImplementedException("brackets after method definition");

            if (ctx.THROWS() is { })
                throw new NotImplementedException("exceptions");

            BlockStatNode body = this.Visit(ctx.methodBody()).As<BlockStatNode>();

            return new FuncDeclNode(ctx.Start.Line, identifier, @params, body);
        }

        public override ASTNode VisitGenericMethodDeclaration([NotNull] GenericMethodDeclarationContext ctx)
        {
            TypeNameListNode templateArgs = this.Visit(ctx.typeParameters()).As<TypeNameListNode>();
            FuncDeclNode func = this.Visit(ctx.methodDeclaration()).As<FuncDeclNode>();

            // throwing exception to suppress warnings
            return new FuncDeclNode(ctx.Start.Line, func.IdentifierNode, templateArgs,
                    func.ParametersNode ?? throw new SyntaxErrorException("Unknown construct"),
                    func.Definition ?? throw new SyntaxErrorException("Unknown construct"));
        }

        public override ASTNode VisitGenericConstructorDeclaration([NotNull] GenericConstructorDeclarationContext ctx)
            => throw new NotImplementedException("constructors");

        public override ASTNode VisitConstructorDeclaration([NotNull] ConstructorDeclarationContext ctx)
            => throw new NotImplementedException("constructors");

        public override ASTNode VisitFieldDeclaration([NotNull] FieldDeclarationContext ctx)
            => this.Visit(ctx.variableDeclarators()); // DeclListNode



        // private methods instead of visiting Modifier Contexts:

        private string ProcessModifier(ModifierContext modifierCtx)
        {
            if (modifierCtx.classOrInterfaceModifier() is { } classOrInterfaceModCtx)
                return this.ProcessClassOrInterfaceModifier(classOrInterfaceModCtx);

            return modifierCtx.GetText();
        }

        private string ProcessClassOrInterfaceModifier([NotNull] ClassOrInterfaceModifierContext classOrInterfaceModCtx)
        {
            if (classOrInterfaceModCtx.annotation() is { })
                throw new NotImplementedException("annotations");

            return classOrInterfaceModCtx.GetText();
        }

        private string ProcessInterfaceMethodModifier([NotNull] InterfaceMethodModifierContext interfaceMethodModCtx)
        {
            if (interfaceMethodModCtx.annotation() is { })
                throw new NotImplementedException("annotations");

            return interfaceMethodModCtx.GetText();
        }

        private string ProcessVariableModifier(VariableModifierContext variableModifierCtx)
        {
            if (variableModifierCtx.annotation() is { })
                throw new NotImplementedException("annotations");

            return variableModifierCtx.GetText();
        }


        // other (overriden just for the purposes of testing the above methods):

        public override ASTNode VisitQualifiedName([NotNull] QualifiedNameContext ctx)
            => new IdNode(ctx.Start.Line,
                string.Join('.', ctx.IDENTIFIER().Select(id => id.GetText())));

        public override ASTNode VisitClassBody([NotNull] ClassBodyContext ctx)
            => new BlockStatNode(ctx.Start.Line);

        public override ASTNode VisitInterfaceBody([NotNull] InterfaceBodyContext ctx)
            => new BlockStatNode(ctx.Start.Line);

        public override ASTNode VisitTypeType([NotNull] TypeTypeContext ctx)
            => new TypeNameNode(ctx.Start.Line, "String");

        public override ASTNode VisitTypeTypeOrVoid([NotNull] TypeTypeOrVoidContext ctx)
            => new TypeNameNode(ctx.Start.Line, "String");

        public override ASTNode VisitTypeParameters([NotNull] TypeParametersContext ctx)
            => new TypeNameListNode(ctx.Start.Line, new TypeNameNode(ctx.Start.Line, "Class1"));

        public override ASTNode VisitMethodBody([NotNull] MethodBodyContext ctx)
            => new BlockStatNode(ctx.Start.Line);

        public override ASTNode VisitFormalParameters([NotNull] FormalParametersContext ctx)
            => new FuncParamsNode(ctx.Start.Line);



    }
}
