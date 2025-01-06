package genetico;

import java.util.Random;
/**
 *
 * @author 0040838
 */
public class Genetico {
   public static double binaryToDecimal(String bits) {
        double decimal = 0; //armazena o valor decimal resultante da conversão dos bits
        int length = bits.length();
        
        for (int i = 0; i < length; i++) {
            char bitChar = bits.charAt(i);
            if (bitChar == '1') {
                decimal += Math.pow(2, length - 1 - i); //converte o bit binário em sua representação decimal e acumula-o em decimal
            } else if (bitChar != '0') {
                System.out.println("Entrada inválida. Use apenas '0' e '1'.");
                return -1;
            }
        }
        return decimal;
    }
    public static double normalize(double decimal) {
        return decimal / 31 * 6; //ajusta o valor para um intervalo específico, neste caso, para um intervalo entre 0 e 6.
    }
    public static double calcularFX(double normalizar) {
        return Math.pow(normalizar, 2) - 5 * normalizar + 6; //calcula a função f(x) = x^2 - 5x + 6 usando o valor normalizado passado como argumento
    }
    public static int countBits(String bits) {
        return bits.length();
    }
    public static String generateRandomGene() { //gera uma sequência aleatória de bits (um gene) de comprimento 5
        StringBuilder gene = new StringBuilder(); //constroi a sequência de bits
        Random random = new Random(); //gera números aleatórios
        for (int i = 0; i < 5; i++) { //itera cinco vezes para adicionar cinco bits aleatórios à sequência gene
            gene.append(random.nextInt(2));
        }
        return gene.toString();
    }
    public static void main(String[] args) {
        String bits = generateRandomGene(); //gera uma sequência aleatória de bits e armazená-la na variável bits

        double decimal = binaryToDecimal(bits); //converte a sequência de bits gerada em seu equivalente decimal e armazena o resultado na variável decimal
        if (decimal != -1) { //verifica se a conversão do binário para decimal foi bem sucedida
            double normalizar = normalize(decimal);
            double fx = calcularFX(normalizar);

            System.out.println("Gene: " + bits);
            System.out.println("Decimal: " + decimal);
            System.out.println("Normalizado: " + normalizar);
            System.out.println("f(x): " + fx);
            System.out.println("Número de Bits: " + countBits(bits));
        }
    }
}
