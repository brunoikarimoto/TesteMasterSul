using System;

namespace app
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            int v1 = 0;
            int b1 = 0;

            try
            {
                Console.WriteLine("Insira somente 0 ou 1.");

                // ENTRADA DE DADOS DO USUÁRIO.
                // Números negativos e 0 são false, positivos são true
                Console.WriteLine("S1:");
                bool s1 = Convert.ToBoolean(Convert.ToInt16(Console.ReadLine()));
                Console.WriteLine("S2:");
                bool s2 = Convert.ToBoolean(Convert.ToInt16(Console.ReadLine()));
                Console.WriteLine("S3:");
                bool s3 = Convert.ToBoolean(Convert.ToInt16(Console.ReadLine()));
                Console.WriteLine("S4:");
                bool s4 = Convert.ToBoolean(Convert.ToInt16(Console.ReadLine()));

                // Faço essa parte para que eu consiga ver o mal funcionamento das duas bombas
                int erro = 0;

                if(s1 && !s2)
                {
                    Console.WriteLine("Os sensores de C1 estão com mal funcionamento.");
                    erro = 1;
                }

                if(s3 && !s4)
                {
                    Console.WriteLine("Os sensores de C2 estão com mal funcionamento.");
                    erro = 1;
                }

                if(erro == 1){
                    return;
                }

                // Parte principal da lógica
                if(!s4){
                    b1 = 1;
                }
                else if(!s3){
                    b1 = 1;
                }

                if(!s2){
                    v1 = 1;
                    b1 = 0;
                }
                else if(!s1){
                    v1 = 1;
                }

                Console.WriteLine("V1: " + v1);
                Console.WriteLine("B1: " + b1);
            }
            catch(Exception)
            {
                throw new Exception("Insira apenas números.");
            }
        }
    }
}
