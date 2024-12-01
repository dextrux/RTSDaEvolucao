using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArvoreAVL
{
    public class Nodo<T> where T : IComparable<T>
    {
        public T valor;
        public Nodo<T> esq;
        public Nodo<T> dir;
        public int altura;

        public Nodo(T valor)
        {
            this.valor = valor;
            altura = 1;  // Inicializando a altura do nodo para 1 quando o nodo eh criado
        }
    }

    public class ArvoreAVL<T> where T : IComparable<T>
    {
        public Nodo<T> raiz;

        public ArvoreAVL()
        {
            raiz = null;
        }

        public bool Pesquisar(T valor)
        {
            return (Pesquisar(valor, raiz));
        }

        private bool Pesquisar(T valor, Nodo<T> n)
        {
            if (n == null)
            {
                return false;
            }
            else
            {
                if (valor.CompareTo(n.valor) < 0)
                {
                    return Pesquisar(valor, n.esq);
                }
                else if (valor.CompareTo(n.valor) > 0)
                {
                    return Pesquisar(valor, n.dir);
                }
                else
                {
                    return true;
                }
            }
        }

        public List<Nodo<T>> Nodos()
        {
            List<Nodo<T>> nodos = new List<Nodo<T>>();
            Nodos(raiz, nodos);

            return nodos;

        }

        private void Nodos(Nodo<T> n, List<Nodo<T>> nodos)
        {
            if (n != null)
            {
                Nodos(n.esq, nodos);
                nodos.Add(n);
                Nodos(n.dir, nodos);
            }

        }

        public int Altura()
        {
            List<Nodo<T>> nodos = Nodos();

            int nivel;
            int max = int.MinValue;
            foreach (Nodo<T> n in nodos)
            {
                nivel = Nivel(n.valor);
                //Console.WriteLine($"O nivel do nodo {n.valor} eh: {nivel}");
                if (nivel > max)
                {
                    max = nivel;
                }
            }

            return max;
        }

        public int Nivel(T valor)
        {
            return (Nivel(valor, raiz));
        }

        private int Nivel(T valor, Nodo<T> n)
        {
            if (n == null)
            {
                throw new Exception("ERRO: Elemento nao existe");
            }
            else
            {
                if (valor.CompareTo(n.valor) < 0)
                {
                    return Nivel(valor, n.esq) + 1;
                }
                else if (valor.CompareTo(n.valor) > 0)
                {
                    return Nivel(valor, n.dir) + 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public void MostrarNaHieraquia()
        {
            int altura = Altura();
            List<Nodo<T>> nodos = Nodos();

            Console.Write("[ ");

            for (int i = 0; i <= altura; i++)
            {
                foreach (Nodo<T> n in nodos)
                {
                    int nivel = Nivel(n.valor);
                    if (nivel == i)
                    {
                        Console.Write($"{n.valor}, ");
                    }
                }
                Console.WriteLine();
            }

            Console.Write(" ]");

        }
        
        private int Altura(Nodo<T> n)
        {
            if (n == null)
                return 0;
            return n.altura;
        }
        private int FatorDeBalanceamento(Nodo<T> n)
        {
            if (n == null)
                return 0;
            return Altura(n.esq) - Altura(n.dir);
        }

        private Nodo<T> RotacaoDireita(Nodo<T> y)
        {
            Nodo<T> x = y.esq;
            Nodo<T> T2 = x.dir;

            // Executar rotacao
            x.dir = y;
            y.esq = T2;

            // Atualizar alturas
            y.altura = 1 + Math.Max(Altura(y.esq), Altura(y.dir));
            x.altura = 1 + Math.Max(Altura(x.esq), Altura(x.dir));

            // Retornar nova raiz
            return x;
        }

        private Nodo<T> RotacaoEsquerda(Nodo<T> x)
        {
            Nodo<T> y = x.dir;
            Nodo<T> T2 = y.esq;

            // Executar rotacao
            y.esq = x;
            x.dir = T2;

            // Atualizar alturas
            x.altura = 1 + Math.Max(Altura(x.esq), Altura(x.dir));
            y.altura = 1 + Math.Max(Altura(y.esq), Altura(y.dir));

            // Retornar nova raiz
            return y;
        }

        private int QuantidadeNodos(Nodo<T> n)
        {
            if (n == null)
            {
                return 0;
            }
            else
            {
                int quantAEsquerda = QuantidadeNodos(n.esq);
                int quantiADireita = QuantidadeNodos(n.dir);

                return (quantAEsquerda + quantiADireita + 1);
            }
        }


        private Nodo<T> Balancear(Nodo<T> n)
        {
            int fatorBalanceamentoN = FatorDeBalanceamento(n);
            int fatorBalanceamentoNEsq = FatorDeBalanceamento(n.esq);
            int fatorBalanceamentoNDir = FatorDeBalanceamento(n.dir);

            if (fatorBalanceamentoN == -2)
            {
                // Caso 1: Rotacao simples a Esquerda
                if (fatorBalanceamentoNEsq <= 0 && fatorBalanceamentoNDir <= 0)
                {
                    return RotacaoEsquerda(n);
                }
                // Caso 3: Rotacao Dupla a Esquerda
                else
                {
                    // Verifica empate
                    if (fatorBalanceamentoNEsq == 1 && fatorBalanceamentoNDir == 1)
                    {
                        int quantNodosEsq = QuantidadeNodos(n.esq);
                        int quantNodosDir = QuantidadeNodos(n.dir);

                        if (quantNodosEsq > quantNodosDir)
                        {
                            n.esq = RotacaoDireita(n.esq);
                            return RotacaoEsquerda(n);
                        }
                        else
                        {
                            n.dir = RotacaoDireita(n.dir);
                            return RotacaoEsquerda(n);
                        }

                    }
                    else if (fatorBalanceamentoNEsq == 1)
                    {
                        n.esq = RotacaoDireita(n.esq);
                        return RotacaoEsquerda(n);
                    }
                    else
                    {
                        n.dir = RotacaoDireita(n.dir);
                        return RotacaoEsquerda(n);
                    }
                }

            }
            else if (fatorBalanceamentoN == 2)
            {
                // Caso 2: Rotacao simples a Direita
                if (fatorBalanceamentoNEsq >= 0 && fatorBalanceamentoNDir >= 0)
                {
                    return RotacaoDireita(n);
                }
                // Caso 4: Rotacao Dupla a Direita
                else
                {
                    // Verifica empate
                    if (fatorBalanceamentoNEsq == -1 && fatorBalanceamentoNDir == -1)
                    {
                        int quantNodosEsq = QuantidadeNodos(n.esq);
                        int quantNodosDir = QuantidadeNodos(n.dir);

                        if (quantNodosEsq > quantNodosDir)
                        {
                            n.esq = RotacaoEsquerda(n.esq);
                            return RotacaoDireita(n);
                        }
                        else
                        {
                            n.dir = RotacaoEsquerda(n.dir);
                            return RotacaoDireita(n);
                        }

                    }
                    else if (fatorBalanceamentoNEsq == -1)
                    {
                        n.esq = RotacaoEsquerda(n.esq);
                        return RotacaoDireita(n);
                    }
                    else
                    {
                        n.dir = RotacaoEsquerda(n.dir);
                        return RotacaoDireita(n);
                    }
                }

            }
            else
            {

             return n;
               
            }

        }

        public void Inserir(T valor)
        {
            raiz = Inserir(valor, raiz);
        }

        private Nodo<T> Inserir(T valor, Nodo<T> n)
        {
            if (n == null)
            {
                return new Nodo<T>(valor);
            }

            if (valor.CompareTo(n.valor) < 0)
            {
                n.esq = Inserir(valor, n.esq);
            }
            else if (valor.CompareTo(n.valor) > 0)
            {
                n.dir = Inserir(valor, n.dir);
            }
            else
            {
                throw new Exception("ERRO: Nao eh possivel inserir elementos repetidos");
            }

            n.altura = 1 + Math.Max(Altura(n.esq), Altura(n.dir));

            n = Balancear(n);

            return n;
        }


        
            public void Remover(T valor)
            {
                raiz = Remover(valor, raiz);
            }

            private Nodo<T> Remover(T valor, Nodo<T> n)
            {
                if (n == null)
                {
                    throw new Exception($"ERRO: Elemento {valor} nao existe na arvore");
                }
                else
                {
                    if (valor.CompareTo(n.valor) < 0)
                    {
                        n.esq = Remover(valor, n.esq);
                    }
                    else if (valor.CompareTo(n.valor) > 0)
                    {
                        n.esq = Remover(valor, n.dir);
                    }
                    else
                    {
                        // CASO 1
                        if (n.esq == null && n.dir == null)
                        {
                            return null;
                        }
                        // CASO 2
                        else if (n.esq != null && n.dir == null)
                        {
                            return n.esq;
                        }
                        // CASO 3
                        else if (n.esq == null && n.dir != null)
                        {
                            return n.dir;
                        }
                        // CASO 4
                        else
                        {
                            n.dir = SubstituiMenorDireita(n, n.dir);
                        }
                    }
                }

                if (n != null)
                {
                    n.altura = 1 + Math.Max(Altura(n.esq), Altura(n.dir));
                    n = Balancear(n);
                }

                return n;

            }

            private Nodo<T> SubstituiMenorDireita(Nodo<T> n, Nodo<T> aux)
            {
                // Ja sabemos que aux sempre sera != null, pois n tem dois filhos
                if (aux.esq != null)
                {
                    aux.esq = SubstituiMenorDireita(n, aux.esq);
                }
                else
                {
                    n.valor = aux.valor;
                    return aux.dir;
                }

                return aux;
            }
            
        }
}
