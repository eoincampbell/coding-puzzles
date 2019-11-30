namespace DailyCodingProblems.Puzzles.Day3
{
    using Base;
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class Impl : BasePuzzle
    {
        public Impl() : base("Day 003") { }

        protected async override Task<string> ExecuteImpl()
        {
            var node = new Node("root", new Node("left", new Node("left.left")), new Node("right"));

            var s = Serialize(node);
            
            var d = Deserialize(s);

            var s2 = Serialize(d);

            return await Task.FromResult((s == s2) + " " + s);
        }

        private static string Serialize(Node rootNode)
        {
            if (rootNode == null) return "null,";
            
            return $"{rootNode.Name},{Serialize(rootNode.Left)}{Serialize(rootNode.Right)}";
        }

        //Deserialize Binary Tree
        private static Node Deserialize(string data)
        {
            string[] temp = data.Split(',');
            return deserialize(temp, 0);
        }

        private static Node deserialize(string[] data, int depth)
        {
            if (depth > data.Length || data[depth].Equals("null"))
            {
                depth++;
                return null;
            }
            
            //After reading the data, increment index value as indication to read next
            //array value in further iteration
            return new Node(data[depth++], deserialize(data, depth), deserialize(data, depth));
        }

        private class Node
        {
            public Node(string name, Node left = null, Node right = null)
            {
                Name = name; Left = left; Right = right;
            }
            public string Name { get; set; }
            public Node Left { get; set; } = null;
            public Node Right { get; set; } = null;

            public override string ToString()
            {
                return $"Name: {Name} | Left: {Left} | Right: {Right}";
            }
        }
    }
}
