using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EstructurasDeDatos
{
    public class AVLTreeNode<T>
    {
        public T Value { get; set; }
        public AVLTreeNode<T> Left { get; set; }
        public AVLTreeNode<T> Right { get; set; }
        public AVLTreeNode<T> Padre { get; set; }

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

    public class TreeAVL<T> where T : IComparable
    {
        public bool dateOrNumber;

        public AVLTreeNode<T> Root { get; set; }

        public TreeAVL() { Root = null; }

        #region MainAVLMethods

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
                        nNuevo.Padre = nPadre;
                        nPadre.Left = nNuevo;
                        if (nPadre == Root)
                        {
                            InsertBalance(nPadre);
                        }
                        else
                        {
                            InsertBalance(nPadre.Padre);
                        }
                    }
                    else
                    {
                        InsertarHijo(nNuevo, nPadre.Left);
                    }
                }
                else
                {

                    if (nPadre.Right == null)
                    {
                        nNuevo.Padre = nPadre;
                        nPadre.Right = nNuevo;
                        if (nPadre == Root)
                        {
                            InsertBalance(nPadre);
                        }
                        InsertBalance(nPadre.Padre);
                    }
                    else
                    {
                        InsertarHijo(nNuevo, nPadre.Right);
                    }
                    
                }
            }
            else
            {
                nPadre = nNuevo;
                InsertBalance(nPadre.Padre);
            }

            AVLTreeNode<T> Unbalanced = FindUnbalancedNode();
            if (Unbalanced != null)
            {
                InsertBalance(Unbalanced);
            }
        }

        private AVLTreeNode<T> MinNode(AVLTreeNode<T> Node)
        {
            AVLTreeNode<T> Aux = Node;

            while (Aux.Left != null)
            {
                Aux = Aux.Left;
            }

            return Aux;
        }

        public AVLTreeNode<T> Eliminar(T valor)
        {
            AVLTreeNode<T> root = Root;
            AVLTreeNode<T> nPadre = Root;

            while (root.Value.CompareTo(valor) != 0)
            {
                nPadre = root;
                if (valor.CompareTo(root.Value) <= 0)
                {
                    root = root.Left;
                }
                else
                {
                    root = root.Left;
                }

                if (root == null)
                    return null;
            }

            if (root.Left == null || root.Right == null)
            {
                AVLTreeNode<T> Aux;
                if (root.Left != null)
                {
                    Aux = root.Left;
                }
                else
                {
                    Aux = root.Right;
                }

                if (Aux == null) //Sin hijos
                {
                    Aux = root;
                    root = null;
                }
                else            //Un solo hijo
                {
                    root = Aux;
                }
            }
            else
            {
                // El más a la izquierda del subarbol derecho
                AVLTreeNode<T> Aux = MinNode(root.Right);
                root = Aux;
                root.Right = Eliminar(root.Right.Value);
            }

            if (root == null)
            {
                return root;
            }

            DeleteBalance(root);

            return root;
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

        #endregion

        #region ABBmethods

        public bool IsBalanced()
        {
            if (Root == null)
                return true;

            return IsBalanced(Root);
        }

        public bool IsBalanced(AVLTreeNode<T> Node)
        {
            bool Valor;
            if (Node.Left == null && Node.Right != null)
            {
                Valor = this.IsBalanced(Node.Right) && (Math.Abs(0 - GetHeight(Node.Right)) <= 1);
                return Valor;
            }
            else if (Node.Right == null && Node.Left != null)
            {
                Valor = this.IsBalanced(Node.Left) && (Math.Abs(GetHeight(Node.Left) - 0) <= 1);
                return Valor;
            }
            else if (Node.Left == null && Node.Right == null)
            {
                return true;
            }
            else
            {
                Valor = this.IsBalanced(Node.Left) && this.IsBalanced(Node.Right) && (Math.Abs(GetHeight(Node.Left) - GetHeight(Node.Right)) <= 1);
                return Valor;
            }
        }

        #endregion

        #region BalanceMethods

        public void InsertBalance(AVLTreeNode<T> Node)
        {
            int Balance = GetBalance(Node);

            if (Balance > 1 && GetBalance(Node.Left) == -1)
            {
                Node.Left = LeftRotation(Node.Left);
                Node = RightRotation(Node);
            }
            else if (Balance < -1 && GetBalance(Node.Right) == 1)
            {
                Node.Right = RightRotation(Node.Right);
                Node = LeftRotation(Node);
            }
            else if (Balance > 1)
            {
                Node = LeftRotation(Node);
            }
            else if (Balance < -1)
            {
                Node = RightRotation(Node);
            }
        }

        public AVLTreeNode<T> DeleteBalance(AVLTreeNode<T> Node)
        {
            int Balance = GetBalance(Node);

            if (Balance > 1 && GetBalance(Node.Left) >= 0)
            {
                return RightRotation(Node);
            }
            if (Balance > 1 && GetBalance(Node.Left) < 0)
            {
                Node.Left = LeftRotation(Node.Left);
                return RightRotation(Node);
            }
            if (Balance < -1 && GetBalance(Node.Right) <= 0)
            {
                return LeftRotation(Node);
            }
            if (Balance < -1 && GetBalance(Node.Right) <= 0)
            {
                return LeftRotation(Node);
            }
            if (Balance < -1 && GetBalance(Node.Right) > 0)
            {
                Node.Right = RightRotation(Node.Right);
                return LeftRotation(Node);
            }

            return Node;
        } 

        private AVLTreeNode<T> RightRotation(AVLTreeNode<T> Node)
        {   
            AVLTreeNode<T> NewRoot = Node.Left;
            Node.Left = NewRoot.Right;
            NewRoot.Padre = Node.Padre;
            NewRoot.Right = Node;
            Node.Padre = NewRoot;


            if (NewRoot.Padre == null)
            {
                Root = NewRoot;
            }
            else
            {
                NewRoot.Padre.Right = NewRoot;
            }

            return NewRoot;
        }

        private AVLTreeNode<T> LeftRotation(AVLTreeNode<T> Node)
        {
            AVLTreeNode<T> NewRoot = Node.Right;

            NewRoot.Padre = Node.Padre;
            Node.Right = NewRoot.Left;
            NewRoot.Left = Node;
            Node.Padre = NewRoot;


            if (NewRoot.Padre == null)
            {
                Root = NewRoot;
            }
            else
            {
                NewRoot.Padre.Left = NewRoot;
            }

            return NewRoot;
        }

        public int GetBalance(AVLTreeNode<T> Node)
        {
            if (Node == null)
            {
                return 0;
            }
            return GetHeight(Node.Left) - GetHeight(Node.Right);
        }

        private AVLTreeNode<T> FindUnbalancedNode()
        {
            return FindUnbalancedNode(Root);
        }

        private AVLTreeNode<T> FindUnbalancedNode(AVLTreeNode<T> Node)
        {
            if (Node != null)
            {
                int Balance = Math.Abs(GetHeight(Node.Left) - GetHeight(Node.Right));
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

        #endregion

        #region Orders

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

        #endregion
        
    }
}
