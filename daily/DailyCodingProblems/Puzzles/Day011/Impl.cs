namespace DailyCodingProblems.Puzzles.Day011
{
    using Base;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using CodingPuzzles.Helpers;

    public class Impl : BasePuzzle
    {
        public Impl() : base("Day 011") { }

        protected override async Task<string> ExecuteImpl()
        {
            string[] s = {"dog", "dear", "deer", "deal", "dealing", "dealer", "dealt", "death", "dead", "deadly"};

            var root = new TreeNode<char>(' ');
            foreach (var a in s)
            {
                var result = root;
                foreach (var c in (a + '\0'))
                    result = result.Children.FirstOrDefault(f => f.Value == c) ?? result.AddChild(c);
                
            }

            var prefix = "de";

            var found = true;
            foreach (var p in prefix)
            {
                root = root.Children.FirstOrDefault(f => f.Value == p);
                if (root != null) continue;
                found = false;
                break;
            }

            var count = 0;
            if (found)
            {
                root.Traverse(c =>
                {
                    if (!c.Children.Any()) count++;
                });
            }

            return await Task.FromResult($"{count}");
        }
    }

    public class TreeNode<T>
    {
        
        private readonly List<TreeNode<T>> _children = new List<TreeNode<T>>();

        public TreeNode(T value)
        {
            Value = value;
        }

        public TreeNode<T> this[int i]
        {
            get { return _children[i]; }
        }

        public TreeNode<T> Parent { get; private set; }

        public T Value { get; }

        public ReadOnlyCollection<TreeNode<T>> Children
        {
            get { return _children.AsReadOnly(); }
        }

        public TreeNode<T> AddChild(T value)
        {
            var node = new TreeNode<T>(value) { Parent = this };
            _children.Add(node);
            return node;
        }

        public TreeNode<T> AddChild(TreeNode<T> node)
        {
            node.Parent = this;
            _children.Add(node);
            return node;
        }

        public TreeNode<T>[] AddChildren(params T[] values)
        {
            return values.Select(AddChild).ToArray();
        }

        public bool RemoveChild(TreeNode<T> node)
        {
            return _children.Remove(node);
        }

        public void Traverse(Action<T> action)
        {
            action(Value);
            foreach (var child in _children)
                child.Traverse(action);
        }
        public void Traverse(Action<TreeNode<T>> action)
        {
            action(this);
            foreach (var child in _children)
                child.Traverse(action);
        }

        public IEnumerable<T> Flatten()
        {
            return new[] { Value }.Concat(_children.SelectMany(x => x.Flatten()));
        }

        public override string ToString() => Value.ToString();
    }
}
