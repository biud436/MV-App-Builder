using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cordova_Builder.Cordova.Common.UI
{
    internal class VirtualTree
    {
        private VirtualNode _root;

        public VirtualTree()
        {
            _root = new VirtualNode("Root", null);
        }

        public VirtualNode Root => _root;

        public VirtualNode AddNode(string name, Control control, VirtualNode parent = null)
        {
            if (parent == null)
            {
                parent = _root;
            }

            var node = new VirtualNode(name, control);
            parent.AddChild(node);

            return node;
        }

        public VirtualNode FindNode(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return _root;
            }

            string[] parts = path.Split('/');
            VirtualNode current = _root;

            foreach (string part in parts)
            {
                if (string.IsNullOrEmpty(part))
                {
                    continue;
                }

                current = current.Children.FirstOrDefault(n => n.Name == part);
                if (current == null)
                {
                    return null;
                }
            }

            return current;
        }

        public void Build(Action<VirtualNode> builder)
        {
            builder?.Invoke(_root);
        }

        public void Traverse(Action<VirtualNode> action)
        {
            TraverseRecursive(_root, action);
        }

        private void TraverseRecursive(VirtualNode node, Action<VirtualNode> action)
        {
            action?.Invoke(node);

            foreach (var child in node.Children)
            {
                TraverseRecursive(child, action);
            }
        }
    }

    internal class VirtualNode
    {
        private List<VirtualNode> _children;

        public string Name { get; set; }
        public Control Control { get; set; }
        public VirtualNode Parent { get; set; }
        public List<VirtualNode> Children => _children;

        public VirtualNode(string name, Control control)
        {
            Name = name;
            Control = control;
            _children = new List<VirtualNode>();
        }

        public VirtualNode AddChild(string name, Control control)
        {
            var node = new VirtualNode(name, control);
            node.Parent = this;
            _children.Add(node);
            return node;
        }

        public VirtualNode AddChild(VirtualNode node)
        {
            node.Parent = this;
            _children.Add(node);
            return node;
        }

        public VirtualNode GetChild(string name)
        {
            return _children.FirstOrDefault(n => n.Name == name);
        }

        public VirtualNode GetChild(int index)
        {
            if (index >= 0 && index < _children.Count)
            {
                return _children[index];
            }
            return null;
        }

        public T GetControl<T>() where T : Control
        {
            return Control as T;
        }

        public bool HasChildren => _children.Count > 0;

        public int ChildCount => _children.Count;

        public string GetPath()
        {
            if (Parent == null || Parent.Name == "Root")
            {
                return Name;
            }

            return $"{Parent.GetPath()}/{Name}";
        }

        public void ForEach(Action<VirtualNode> action)
        {
            foreach (var child in _children)
            {
                action?.Invoke(child);
            }
        }

        public VirtualNode this[string name] => GetChild(name);

        public VirtualNode this[int index] => GetChild(index);
    }
}
