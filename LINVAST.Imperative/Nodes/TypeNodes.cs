using System.Collections.Generic;
using System.Linq;
using System.Text;
using LINVAST.Nodes;
using Newtonsoft.Json;

namespace LINVAST.Imperative.Nodes
{
    public sealed class TypeDeclNode : DeclNode
    {
        [JsonIgnore]
        public IEnumerable<DeclStatNode> Declarations => this.Children.Skip(3).Cast<DeclStatNode>();

        [JsonIgnore]
        public IdListNode TemplateVariables => this.Children[1].As<IdListNode>();

        [JsonIgnore]
        public IdListNode BaseTypes => this.Children[2].As<IdListNode>();


        public TypeDeclNode(int line, IdNode identifier, IdListNode templateVars, IdListNode baseTypes, IEnumerable<DeclStatNode> declarations)
            : base(line, identifier, new ASTNode[] { templateVars, baseTypes }.Concat(declarations)) { }

        public TypeDeclNode(int line, IdNode identifier, IdListNode templateVars, IdListNode baseTypes, params DeclStatNode[] declarations)
            : base(line, identifier, new ASTNode[] { templateVars, baseTypes }.Concat(declarations)) { }


        public override string GetText()
        {
            var sb = new StringBuilder();
            sb.Append(this.Identifier);
            if (this.TemplateVariables.Expressions.Any())
                sb.Append('<').AppendJoin(',', this.TemplateVariables.Expressions).Append('>');
            if (this.BaseTypes.Expressions.Any())
                sb.Append(" : ").AppendJoin(',', this.TemplateVariables.Expressions);
            sb.AppendLine();
            sb.Append(" { ").AppendJoin('\n', this.Declarations).AppendLine(" }");
            return sb.ToString();
        }
    }

    public sealed class EnumDeclNode : DeclNode
    {
        [JsonIgnore]
        public IEnumerable<VarDeclNode> Constants => this.Children.Cast<VarDeclNode>();


        public EnumDeclNode(int line, IdNode identifier, IEnumerable<VarDeclNode> constants)
            : base(line, identifier, constants) { }

        public EnumDeclNode(int line, IdNode identifier, params VarDeclNode[] constants)
            : base(line, identifier, constants) { }
    }

    public abstract class TypeNode : DeclStatNode
    {
        protected string Category { get; }

        protected TypeNode(int line, string category, DeclSpecsNode specifiers, TypeDeclNode decl)
            : base(line, specifiers, new DeclListNode(line, decl)) 
        {
            this.Category = category;
        }


        public override string ToString() => $"{this.Specifiers} {this.Category} {this.DeclaratorList}";
    }

    public sealed class ClassNode : TypeNode
    {
        public ClassNode(int line, DeclSpecsNode specifiers, TypeDeclNode decl)
            : base(line, "class", specifiers, decl) { }


        public override string ToString() => base.ToString();
    }

    public sealed class StructNode : TypeNode
    {
        public StructNode(int line, DeclSpecsNode specifiers, TypeDeclNode decl)
            : base(line, "struct", specifiers, decl) { }
    }

    public sealed class InterfaceNode : TypeNode
    {
        public InterfaceNode(int line, DeclSpecsNode specifiers, TypeDeclNode decl)
            : base(line, "interface", specifiers, decl) { }
    }

    public sealed class EnumNode : DeclStatNode
    {
        public EnumNode(int line, DeclSpecsNode specifiers, EnumDeclNode decl)
            : base(line, specifiers, new DeclListNode(line, decl)) { }
    }
}
