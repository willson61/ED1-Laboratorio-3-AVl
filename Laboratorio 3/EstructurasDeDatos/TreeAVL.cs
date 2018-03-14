using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasDeDatos
{
    public class AVLTreeNode<T>
    {
        public T Value { get; set; }
        public AVLTreeNode<T> Left { get; set; }
        public AVLTreeNode<T> Right { get; set; }
        public AVLTreeNode<T> Padre { get; set; }
        public int LeftSize { get; set; }
        public int RightSize { get; set; }

        public AVLTreeNode(T value, AVLTreeNode<T> left, AVLTreeNode<T> right, AVLTreeNode<T> padre)
        {
            Value = value;
            Left = left;
            Right = right;
            Padre = padre;
        }

        public AVLTreeNode(T value) : this(value, null, null, null) { }

        public AVLTreeNode() { }

        public bool IsLeaf() { return Left == null && Right == null; }

        public bool Full() { return Left != null && Right != null; }

        public bool IsDegenerate()
        {
            if (this.Left != null)
            {
                if (this.Right != null)
                {
                    return false; // Un nodo fue encontrado con dos hijos
                }
                else
                {
                    return this.Left.IsDegenerate();
                }
            }
            else
            {
                if (this.Right != null)
                {
                    return this.Right.IsDegenerate();
                }
                else
                {
                    return true; // Ningún nodo tiene dos hijos
                }
            }
        }


    }


    class TreeAVL<T> where T: IComparable
    {
        public AVLTreeNode<T> Root { get; set; }

        public TreeAVL() { Root = null; }

        public void Insert(T value)
        {
            AVLTreeNode<T> NewNode = new AVLTreeNode<T>(value);
            if (Root == null)
            {
                Root = NewNode;
            }
            else
            {
                InsertarHijo(NewNode, Root);
            }
        }

        private void InsertarHijo(AVLTreeNode<T> nNuevo, AVLTreeNode<T> nPadre)
        {
            if (nPadre != null)
            {
                if (nNuevo.Value.CompareTo(nPadre.Value) <= 0)
                {
                    if (nPadre.Left == null)
                    {
                        nPadre.Left = nNuevo;
                    }
                    else
                    {
                        InsertarHijo(nNuevo, nPadre.Left);
                    }
                }
                else
                {
                    if (nNuevo.Value.CompareTo(nPadre.Value) > 0)
                    {
                        if (nPadre.Right == null)
                        {
                            nPadre.Right = nNuevo;
                        }
                        else
                        {
                            InsertarHijo(nNuevo, nPadre.Right);
                        }
                    }
                }
            }
        }

        public AVLTreeNode<T> Eliminar(T valor)
        {
            AVLTreeNode<T> nAux = Root;
            AVLTreeNode<T> nPadre = Root;
            bool isLeftLeaf = true;

            while (nAux.Value.CompareTo(valor) != 0)
            {
                nPadre = nAux;
                if (valor.CompareTo(nAux.Value) <= 0)
                {
                    isLeftLeaf = true;
                    nAux = nAux.Left;
                }
                else
                {
                    isLeftLeaf = false;
                    nAux = nAux.Left;
                }

                if (nAux == null)
                    return null;
            }

            if (nAux.Left == null && nAux.Right == null)
            {
                if (nAux == Root)
                {
                    Root = null;
                }
                else if (isLeftLeaf)
                {
                    nPadre.Left = null;
                }
                else
                {
                    nPadre.Right = null;
                }
            }
            else if (nAux.Right == null)
            {
                if (nAux == Root)
                {
                    Root = nAux.Left;
                }
                else if (isLeftLeaf)
                {
                    nPadre.Left = nAux.Left;
                }
                else
                {
                    nPadre.Right = nAux.Left;
                }
            }
            else if (nAux.Left == null)
            {
                if (nAux == Root)
                {
                    Root = nAux.Right;
                }
                else if (isLeftLeaf)
                {
                    nPadre.Left = nAux.Right;
                }
                else
                {
                    nPadre.Right = nAux.Right;
                }
            }
            else
            {
                AVLTreeNode<T> nReplace = Replace(nAux);
                if (nAux == Root)
                {
                    Root = nReplace;
                }
                else if (isLeftLeaf)
                {
                    nPadre.Left = nReplace;
                }
                else
                {
                    nPadre.Right = nReplace;
                }
                nReplace.Left = nAux.Left;
            }

            return nAux;
        }

        public AVLTreeNode<T> Edit(T valor)
        {
            AVLTreeNode<T> nAux = Root;
            AVLTreeNode<T> nPadre = Root;
            bool isLeftLeaf = true;

            while (nAux.Value.CompareTo(valor) != 0)
            {
                nPadre = nAux;
                if (valor.CompareTo(nAux.Value) <= 0)
                {
                    isLeftLeaf = true;
                    nAux = nAux.Left;
                }
                else
                {
                    isLeftLeaf = false;
                    nAux = nAux.Left;
                }

                if (nAux == null)
                    return null;
            }

            AVLTreeNode<T> nReplace = Replace(nAux);
            if (nAux == Root)
            {
                Root = nReplace;
            }
            else if (isLeftLeaf)
            {
                nPadre.Left = nReplace;
            }
            else
            {
                nPadre.Right = nReplace;
            }
            nReplace.Left = nAux.Left;

            return nReplace;

        }

        private AVLTreeNode<T> Replace(AVLTreeNode<T> nElimina)
        {
            AVLTreeNode<T> rPadre = nElimina;
            AVLTreeNode<T> rReplace = nElimina;
            AVLTreeNode<T> Aux = nElimina.Right;
            while (Aux != null)
            {
                rPadre = rReplace;
                rReplace = Aux;
                Aux = Aux.Left;
            }
            if (rReplace != nElimina.Right)
            {
                rPadre.Left = rReplace.Right;
                rReplace.Right = nElimina.Right;
            }
            return rReplace;
        }

        public AVLTreeNode<T> Find(T value)
        {
            AVLTreeNode<T> Aux = Root;
            while (Aux.Value.CompareTo(value) != 0)
            {
                if (value.CompareTo(Aux.Value) < 0)
                {
                    Aux = Aux.Left;
                }
                else
                {
                    Aux = Aux.Right;
                }

                if (Aux == null)
                    return null;
            }

            return Aux;
        }

        bool IsEmpty()
        {
            return Root == null;
        }

        public bool IsDegenerate()
        {
            if (Root == null)
                return false;

            return Root.IsDegenerate();
        }

        public bool IsBalanced()
        {
            if (Root == null)
                return true;

            return IsBalanced(Root);
        }

        private int AbsoluteValue(int Minuendo, int Sustraendo)
        {
            if ((Minuendo - Sustraendo) < 0)
            {
                return (Minuendo - Sustraendo) * -1;
            }
            else
            {
                return (Minuendo - Sustraendo);
            }
        }

        public bool IsBalanced(AVLTreeNode<T> Node)
        {
            bool Valor;
            if (Node.Left == null && Node.Right != null)
            {
                Valor = this.IsBalanced(Node.Right) && ((Math.Abs(0-GetHeight(Node.Right))) <= 1);
                return Valor;
            }
            else if (Node.Right == null && Node.Left != null)
            {
                Valor = this.IsBalanced(Node.Left) && ((Math.Abs(GetHeight(Node.Left)-0)) <= 1);
                return Valor;
            }
            else if (Node.Left == null && Node.Right == null)
            {
                return true;
            }
            else
            {
                Valor = this.IsBalanced(Node.Left) && this.IsBalanced(Node.Right) && ((Math.Abs(GetHeight(Node.Left)-GetHeight(Node.Right))) <= 1);
                return Valor;
            }
        }

        public AVLTreeNode<T> FindUnbalancedNode()
        {
            return FindUnbalancedNode(Root);
        }

        private AVLTreeNode<T> FindUnbalancedNode(AVLTreeNode<T> Node)
        {
            if (Node != null)
            {
                int Balance = Math.Abs(GetHeight(Node.Left)-GetHeight(Node.Right));
                if (Balance <= 1)
                {
                    if (Node.Left != null)
                    {
                        return FindUnbalancedNode(Node.Left);
                    }
                    else
                    {
                        return FindUnbalancedNode(Node.Right);
                    }
                }
                else
                {
                    return Node;
                }
            }
            else
            {
                return null;
            }

        }

        public int GetHeight(AVLTreeNode<T> Node)
        {
            if (Node == null)
            {
                return -1;
            }
            else
            {
                int LeftHeight = GetHeight(Node.Left);
                int RightHeight = GetHeight(Node.Right);

                if (LeftHeight > RightHeight)
                {
                    return LeftHeight + 1;
                }
                else
                {
                    return RightHeight + 1;
                }
            }
        }

        private void InOrder(AVLTreeNode<T> Root, ref List<T> Elements)
        {
            if (Root != null)
            {
                InOrder(Root.Left, ref Elements);
                Elements.Add(Root.Value);
                InOrder(Root.Right, ref Elements);
            }
        }

        private void PostOrder(AVLTreeNode<T> Root, ref List<T> Elements)
        {
            if (Root != null)
            {
                PostOrder(Root.Left, ref Elements);
                PostOrder(Root.Right, ref Elements);
                Elements.Add(Root.Value);
            }
        }

        private void PreOrder(AVLTreeNode<T> Root, ref List<T> Elements)
        {
            if (Root != null)
            {
                Elements.Add(Root.Value);
                PreOrder(Root.Left, ref Elements);
                PreOrder(Root.Right, ref Elements);
            }
        }

        public List<T> Orders(string Order)
        {
            List<T> Elements = new List<T>();
            switch (Order)
            {
                case "PreOrder":
                    PreOrder(Root, ref Elements);
                    break;
                case "InOrder":
                    InOrder(Root, ref Elements);
                    break;
                case "PostOrder":
                    PostOrder(Root, ref Elements);
                    break;
            }
            return Elements;
        }


    }
}
