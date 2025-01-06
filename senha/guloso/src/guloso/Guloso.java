package guloso;

import java.util.Scanner;
/**
 *
 * @author 0040838
 */
public class Guloso {
    /**
     * @param args the command line arguments
     */
   public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        double[] coins = {0.01, 0.05, 0.11, 0.20}; //VALORES DISPONIVEIS DE MOEDAS
        
        System.out.print("Digite o preço do refrigerante: ");
        double price = scanner.nextDouble();
        
        System.out.print("Digite o valor pago: ");
        double amountPaid = scanner.nextDouble();
        
        double change = amountPaid - price;
        System.out.println("Troco: $" + change);
        
        //CHAMA FUNÇÃO PARA CALCULAR MOEDAS NECESSÁRIAS
        int[] coinsCount = calcularMoedas(change, coins);
        
        System.out.println("Moedas necessárias:");
        for (int i = 0; i < coins.length; i++) {
            if (coinsCount[i] > 0) {
                System.out.println(coinsCount[i] + " moeda(s) de $" + coins[i]);
            }
        }
        scanner.close();
    }
    //CALCULA O NÚMERO MÍNIMO DE MOEDAS
    public static int[] calcularMoedas(double change, double[] coins) {
        int[] coinsCount = new int[coins.length]; //ARRAY PARA ARMAZENAR A COTAGEM DE CADA MOEDA
        for (int i = coins.length - 1; i >= 0; i--) { //PECORRE AS MOEDAS DE FORMA DECRESCENTE
            coinsCount[i] = (int) (change / coins[i]); //CALCULA A QUANTIDADE DA MOEDA ATUAL NO TROCO
            change %= coins[i]; //ATUALIZA O VALOR RESTANTE DO TROCO
        }
        
        //RETORNA A CONTAGEM DAS MOEDAS
        return coinsCount;
    }
}
