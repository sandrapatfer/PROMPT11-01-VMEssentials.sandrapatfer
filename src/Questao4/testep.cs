using System;

namespace Sessao1
{
    class Program
    {
        static void Main(string[] args)
        {
			Ponto p1 = new Ponto(1,2), p2 = new Ponto(3,4);
			Console.WriteLine(p1.ToString());
			Console.WriteLine(p2.ToString());
			Console.WriteLine("Distancia: {0}", p1.Distance(p2));
			Console.WriteLine("Pontos iguais: {0}", p1.CompareTo(p2) == 0? "Sim" : "Não");
        }
    }
}
