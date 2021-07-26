using System;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using LINVAST.Builders;
using LINVAST.Exceptions;
using LINVAST.Imperative.Nodes;
using LINVAST.Logging;
using LINVAST.Nodes;
using static LINVAST.Imperative.Builders.Java.JavaParser;

namespace LINVAST.Imperative.Builders.Java
{
    public sealed partial class JavaASTBuilder : JavaBaseVisitor<ASTNode>, IASTBuilder<JavaParser>
    {

        public override ASTNode VisitPackageDeclaration([NotNull] PackageDeclarationContext context) => base.VisitPackageDeclaration(context);
        public override ASTNode VisitImportDeclaration([NotNull] ImportDeclarationContext context) => base.VisitImportDeclaration(context);
        public override ASTNode VisitClassDeclaration([NotNull] ClassDeclarationContext context) => base.VisitClassDeclaration(context);
        public override ASTNode VisitEnumDeclaration([NotNull] EnumDeclarationContext context) => base.VisitEnumDeclaration(context);
        public override ASTNode VisitEnumBodyDeclarations([NotNull] EnumBodyDeclarationsContext context) => base.VisitEnumBodyDeclarations(context);
        public override ASTNode VisitInterfaceDeclaration([NotNull] InterfaceDeclarationContext context) => base.VisitInterfaceDeclaration(context);
        public override ASTNode VisitClassBodyDeclaration([NotNull] ClassBodyDeclarationContext context) => base.VisitClassBodyDeclaration(context);
        public override ASTNode VisitMemberDeclaration([NotNull] MemberDeclarationContext context) => base.VisitMemberDeclaration(context);
        public override ASTNode VisitMethodDeclaration([NotNull] MethodDeclarationContext context) => base.VisitMethodDeclaration(context);
        public override ASTNode VisitGenericMethodDeclaration([NotNull] GenericMethodDeclarationContext context) => base.VisitGenericMethodDeclaration(context);
        public override ASTNode VisitGenericConstructorDeclaration([NotNull] GenericConstructorDeclarationContext context) => base.VisitGenericConstructorDeclaration(context);
        public override ASTNode VisitConstructorDeclaration([NotNull] ConstructorDeclarationContext context) => base.VisitConstructorDeclaration(context);
        public override ASTNode VisitFieldDeclaration([NotNull] FieldDeclarationContext context) => base.VisitFieldDeclaration(context);
        public override ASTNode VisitInterfaceBodyDeclaration([NotNull] InterfaceBodyDeclarationContext context) => base.VisitInterfaceBodyDeclaration(context);
        public override ASTNode VisitInterfaceMemberDeclaration([NotNull] InterfaceMemberDeclarationContext context) => base.VisitInterfaceMemberDeclaration(context);
        public override ASTNode VisitConstDeclaration([NotNull] ConstDeclarationContext context) => base.VisitConstDeclaration(context);
        public override ASTNode VisitConstantDeclarator([NotNull] ConstantDeclaratorContext context) => base.VisitConstantDeclarator(context);
        public override ASTNode VisitInterfaceMethodDeclaration([NotNull] InterfaceMethodDeclarationContext context) => base.VisitInterfaceMethodDeclaration(context);
        public override ASTNode VisitGenericInterfaceMethodDeclaration([NotNull] GenericInterfaceMethodDeclarationContext context) => base.VisitGenericInterfaceMethodDeclaration(context);
        public override ASTNode VisitVariableDeclarators([NotNull] VariableDeclaratorsContext context) => base.VisitVariableDeclarators(context);
        public override ASTNode VisitVariableDeclarator([NotNull] VariableDeclaratorContext context) => base.VisitVariableDeclarator(context);
        public override ASTNode VisitVariableDeclaratorId([NotNull] VariableDeclaratorIdContext context) => base.VisitVariableDeclaratorId(context);
        public override ASTNode VisitAnnotationTypeDeclaration([NotNull] AnnotationTypeDeclarationContext context) => base.VisitAnnotationTypeDeclaration(context);
        public override ASTNode VisitAnnotationTypeElementDeclaration([NotNull] AnnotationTypeElementDeclarationContext context) => base.VisitAnnotationTypeElementDeclaration(context);
        public override ASTNode VisitLocalVariableDeclaration([NotNull] LocalVariableDeclarationContext context) => base.VisitLocalVariableDeclaration(context);
        public override ASTNode VisitLocalTypeDeclaration([NotNull] LocalTypeDeclarationContext context) => base.VisitLocalTypeDeclaration(context);


    }
}
