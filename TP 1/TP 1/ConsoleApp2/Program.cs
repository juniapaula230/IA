using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Solicita ao usuário os coeficientes da equação quadrática.
            Console.WriteLine("Informe os coeficientes da equação quadrática:");
            Console.Write("A: ");
            double A = double.Parse(Console.ReadLine());
            Console.Write("B: ");
            double B = double.Parse(Console.ReadLine());
            Console.Write("C: ");
            double C = double.Parse(Console.ReadLine());

            // Solicita ao usuário o intervalo de busca para o valor de x.
            Console.WriteLine("Informe o intervalo de busca de x:");
            Console.Write("xmin: ");
            double xmin = double.Parse(Console.ReadLine());
            Console.Write("xmax: ");
            double xmax = double.Parse(Console.ReadLine());

            // Solicita ao usuário a escolha do método de otimização.
            Console.WriteLine("Escolha um dos métodos:");
            Console.WriteLine("1. Estratégia Evolutiva");
            Console.WriteLine("2. Programação Evolutiva");
            Console.WriteLine("3. Algoritmo Genético");
            int escolhaMetodo = int.Parse(Console.ReadLine());

            // Inicializa variáveis para o tamanho da população, número de bits e gerações.
            int tamPopulacao = 0, numeroBits = 0, geracoes = 0;

            // Configura parâmetros com base na escolha do método.
            switch (escolhaMetodo)
            {
                case 1:
                    Console.WriteLine("Defina o tamanho do vetor:");
                    numeroBits = int.Parse(Console.ReadLine());
                    break;
                case 2:
                    Console.WriteLine("Defina o tamanho da população:");
                    tamPopulacao = int.Parse(Console.ReadLine());
                    Console.WriteLine("Defina o número de bits:");
                    numeroBits = int.Parse(Console.ReadLine());
                    Console.WriteLine("Defina o número de iterações:");
                    geracoes = int.Parse(Console.ReadLine());
                    break;
                case 3:
                    Console.WriteLine("Defina o tamanho da população:");
                    tamPopulacao = int.Parse(Console.ReadLine());
                    Console.WriteLine("Defina o número de bits:");
                    numeroBits = int.Parse(Console.ReadLine());
                    break;
                default:
                    Console.WriteLine("Método inválido.");
                    return;
            }

            // Inicializa o gerador de números aleatórios.
            Random r = new Random(DateTime.Now.Millisecond);

            // Executa o método selecionado e encontra o melhor indivíduo.
            Individuo melhorIndividuo = null;

            switch (escolhaMetodo)
            {
                case 1:
                    melhorIndividuo = EstrategiaEvolutiva(numeroBits, xmin, xmax, A, B, C, r);
                    break;
                case 2:
                    melhorIndividuo = ProgramacaoEvolutiva(tamPopulacao, numeroBits, geracoes, xmin, xmax, A, B, C, r);
                    break;
                case 3:
                    Console.WriteLine("Defina a probabilidade de mutação (0-100):");
                    double probMutacao = double.Parse(Console.ReadLine()) / 100.0;

                    Console.WriteLine("Defina a probabilidade de recombinação (0-100):");
                    double probRecombinacao = double.Parse(Console.ReadLine()) / 100.0;

                    Console.WriteLine("Defina o número de gerações:");
                    geracoes = int.Parse(Console.ReadLine());

                    melhorIndividuo = AlgoritmoGenetico(tamPopulacao, numeroBits, probMutacao, probRecombinacao, geracoes, xmin, xmax, A, B, C, r);
                    break;
            }

            // Exibe o melhor resultado encontrado.
            if (melhorIndividuo != null)
            {
                Console.WriteLine("Melhor solução encontrada:");
                Console.WriteLine($"Menor valor de F(x): {melhorIndividuo.adaptabilidade}");
                Console.WriteLine($"Valor de x utilizado: {melhorIndividuo.x}");
                Console.WriteLine($"Genótipo binário: {string.Join("", melhorIndividuo.b)}");
                Console.WriteLine($"Genótipo decimal: {Individuo.BinarioParaDecimal(melhorIndividuo.b)}");
            }

            Console.ReadKey();
        }

        // Método para a Estratégia Evolutiva.
        private static Individuo EstrategiaEvolutiva(int numeroBits, double xmin, double xmax, double A, double B, double C, Random r)
        {
            Console.WriteLine("Estratégia Evolutiva selecionada.");

            Populacao p = new Populacao(1, numeroBits, xmin, xmax, A, B, C, r);
            Individuo ind = p.ind[0];

            return ind;
        }

        // Método para a Programação Evolutiva.
        private static Individuo ProgramacaoEvolutiva(int tamPopulacao, int numeroBits, int geracoes, double xmin, double xmax, double A, double B, double C, Random r)
        {
            Console.WriteLine("Programação Evolutiva selecionada.");

            Populacao p = new Populacao(tamPopulacao, numeroBits, xmin, xmax, A, B, C, r);

            // Itera pelas gerações.
            for (int cont = 0; cont < geracoes; cont++)
            {
                // Aplica mutações e avaliações nos indivíduos da população.
                for (int i = 0; i < tamPopulacao; i++)
                {
                    p.ind[i].Mutacao(1.0 / p.ind[i].n);
                    p.ind[i].ConverterDecimal();
                    p.ind[i].avaliacao(A, B, C);
                }
                p.BuscarMelhor();

                // Exibe o melhor indivíduo da geração atual.
                Console.WriteLine($"Geração {cont + 1}: Melhor indivíduo da geração:");
                p.mInd.Imprimir();
            }

            return p.mInd;
        }

        // Método para o Algoritmo Genético.
        private static Individuo AlgoritmoGenetico(int tamPopulacao, int numeroBits, double probMutacao, double probRecombinacao, int geracoes, double xmin, double xmax, double A, double B, double C, Random r)
        {
            Console.WriteLine("Algoritmo Genético selecionado.");

            Populacao populacaoAtual = new Populacao(tamPopulacao, numeroBits, xmin, xmax, A, B, C, r);
            Populacao novaPopulacao = new Populacao(tamPopulacao, numeroBits, xmin, xmax, A, B, C, r);

            int totalMutacoes = 0;
            int totalRecombinacoes = 0;

            // Itera pelas gerações.
            for (int cont = 0; cont < geracoes; cont++)
            {
                populacaoAtual.CalcularRoleta();

                // Aplica recombinação e mutação nos indivíduos.
                for (int i = 0; i < tamPopulacao; i++)
                {
                    if (r.NextDouble() < probRecombinacao)
                    {
                        int indice1 = populacaoAtual.GirarRoleta();
                        int indice2 = populacaoAtual.GirarRoleta();

                        novaPopulacao.ind[i] = new Individuo(numeroBits, xmin, xmax, A, B, C, r);
                        Individuo.Recombinacao(populacaoAtual.ind[indice1], populacaoAtual.ind[indice2], novaPopulacao.ind[i]);

                        totalRecombinacoes++;
                    }
                    else
                    {
                        novaPopulacao.ind[i].Copiar(populacaoAtual.ind[i]);
                    }

                    if (r.NextDouble() < probMutacao)
                    {
                        novaPopulacao.ind[i].Mutacao(probMutacao);
                        totalMutacoes++;
                    }

                    novaPopulacao.ind[i].ConverterDecimal();
                    novaPopulacao.ind[i].avaliacao(A, B, C);
                }

                novaPopulacao.BuscarMelhor();
                populacaoAtual.Copiar(novaPopulacao);

                // Exibe o melhor indivíduo da geração atual.
                Console.WriteLine($"Geração {cont + 1}: Melhor indivíduo da geração:");
                populacaoAtual.mInd.Imprimir();
            }

            // Exibe o total de mutações e recombinações realizadas.
            Console.WriteLine($"Total de mutações: {totalMutacoes}");
            Console.WriteLine($"Total de recombinações: {totalRecombinacoes}");

            return populacaoAtual.mInd;
        }
    }

    public class Individuo
    {
        public int[] b; // Representação binária do indivíduo.
        public double x; // Valor decimal do indivíduo.
        public double adaptabilidade; // Valor de adaptabilidade do indivíduo.
        public int n; // Número de bits.
        public double xmin; // Limite inferior do intervalo de busca.
        public double xmax; // Limite superior do intervalo de busca.
        public double A; // Coeficiente A da equação.
        public double B; // Coeficiente B da equação.
        public double C; // Coeficiente C da equação.
        private Random r; // Gerador de números aleatórios.

        public Individuo(int numeroBits, double xmin, double xmax, double A, double B, double C, Random r)
        {
            n = numeroBits;
            this.xmin = xmin;
            this.xmax = xmax;
            this.A = A;
            this.B = B;
            this.C = C;
            this.r = r;
            b = new int[n];
            Criar();
            ConverterDecimal();
            avaliacao(A, B, C);
        }

        // Gera um indivíduo com valores binários aleatórios.
        private void Criar()
        {
            for (int i = 0; i < n; i++)
            {
                b[i] = r.Next(2);
            }
        }

        // Aplica mutações ao indivíduo com base na probabilidade fornecida.
        public void Mutacao(double prob)
        {
            for (int i = 0; i < n; i++)
            {
                if (r.NextDouble() < prob)
                {
                    b[i] = 1 - b[i];
                }
            }
        }

        // Converte a representação binária em um valor decimal.
        public void ConverterDecimal()
        {
            x = 0;
            for (int i = 0; i < n; i++)
            {
                x += b[i] * Math.Pow(2, n - 1 - i);
            }
            x = xmin + x * (xmax - xmin) / (Math.Pow(2, n) - 1);
        }

        // Avalia o indivíduo de acordo com a equação quadrática.
        public void avaliacao(double A, double B, double C)
        {
            double f = A * Math.Pow(x, 2) + B * x + C;
            adaptabilidade = -f;
        }

        // Converte um vetor binário para um valor decimal.
        public static double BinarioParaDecimal(int[] b)
        {
            double decimalValue = 0;
            for (int i = 0; i < b.Length; i++)
            {
                decimalValue += b[i] * Math.Pow(2, b.Length - 1 - i);
            }
            return decimalValue;
        }

        // Realiza a recombinação entre dois indivíduos para gerar um novo indivíduo.
        public static void Recombinacao(Individuo pai1, Individuo pai2, Individuo filho)
        {
            int pontoCorte = pai1.b.Length / 2;
            for (int i = 0; i < pontoCorte; i++)
            {
                filho.b[i] = pai1.b[i];
            }
            for (int i = pontoCorte; i < pai1.b.Length; i++)
            {
                filho.b[i] = pai2.b[i];
            }
        }

        // Imprime as informações do indivíduo.
        public void Imprimir()
        {
            Console.WriteLine($"Genótipo binário: {string.Join("", b)}");
            Console.WriteLine($"Genótipo decimal: {BinarioParaDecimal(b)}");
            Console.WriteLine($"F(x): {adaptabilidade}");
        }

        // Copia os valores de outro indivíduo para o atual.
        public void Copiar(Individuo ind)
        {
            Array.Copy(ind.b, b, b.Length);
            x = ind.x;
            adaptabilidade = ind.adaptabilidade;
        }
    }

    public class Populacao
    {
        public Individuo[] ind; // Array de indivíduos na população.
        public Individuo mInd; // Melhor indivíduo da população.
        private double totalAdaptabilidade; // Soma das adaptabilidades.
        private Random r; // Gerador de números aleatórios.
        private double[] roleta; // Array para cálculo da roleta.

        public Populacao(int tamanho, int numeroBits, double xmin, double xmax, double A, double B, double C, Random r)
        {
            ind = new Individuo[tamanho];
            this.r = r;
            for (int i = 0; i < tamanho; i++){ //inicializa a população com indivíduos aleatórios.
                ind[i] = new Individuo(numeroBits, xmin, xmax, A, B, C, r);
            }
            BuscarMelhor();
        }

        public void BuscarMelhor(){ //encontra o melhor indivíduo da população com base na adaptabilidade.
            mInd = ind[0];
            foreach (var i in ind){
                if (i.adaptabilidade < mInd.adaptabilidade){
                    mInd = i;
                }
            }
        }

        public void CalcularRoleta(){ //calcula a roleta para seleção dos indivíduos.
            roleta = new double[ind.Length];
            totalAdaptabilidade = 0;
            for (int i = 0; i < ind.Length; i++){
                totalAdaptabilidade += ind[i].adaptabilidade;
            }
            double soma = 0;
            for (int i = 0; i < roleta.Length; i++){
                soma += ind[i].adaptabilidade / totalAdaptabilidade;
                roleta[i] = soma;
            }
        }

        public int GirarRoleta(){ //seleciona um índice com base na roleta.
            double p = r.NextDouble();
            for (int i = 0; i < roleta.Length; i++){
                if (p <= roleta[i]){
                    return i;
                }
            }
            return roleta.Length - 1;
        }

        public void Copiar(Populacao p){ //copia a população de um objeto para outro
            Array.Copy(p.ind, ind, ind.Length);
            mInd = p.mInd;
        }
    }
}
