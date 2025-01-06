package fibonnacid;

import java.util.Scanner;

/**
 *
 * @author 0040838
 */
public class Fibonnacid {

    static double[] tabela;

    public static double fibonaccid(int N) {
        if (tabela[N] == -1) {
            tabela[N] = fibonaccid(N - 1) + fibonaccid(N - 2);
        }
        return tabela[N];
    }

    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        System.out.print("Digite a posição desejada na sequência Fibonacci: ");
        int position = scanner.nextInt();
        scanner.close();

        tabela = new double[position + 1];
        tabela[0] = 0;
        tabela[1] = 1;
        for (int i = 2; i <= position; i++) {
            tabela[i] = -1; //inicializando a tabela com -1
        }

        System.out.println("O valor na posição " + position + " da sequência Fibonacci é: " + fibonaccid(position));
    }
}
