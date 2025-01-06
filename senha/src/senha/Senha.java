package senha;

import java.util.Scanner;

/**
 *
 * @author 0040838
 */
public class Senha {
    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        Scanner ler = new Scanner(System.in);
        System.out.println("Digite o valor de n: ");
        int n = ler.nextInt();
        int dig=0, x=0, cont=0;
        int k = (int)Math.pow(10,n);
        for (int j=0; j<n; j++){
            System.out.print("0");
        }
        System.out.println("");
        for (int i=1; i<k; i++){
            int casas = (int)Math.log10(i)+1;
                for (int j=0; j<n-casas; j++){
                    System.out.print("0");
                }
                System.out.println(i);
                x=(x+1)%10;
                cont++;
            }
        }
    }
