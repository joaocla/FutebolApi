using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FutebolApi
{
    public class Jogador
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Time { get; set; }
        public string? Nacionalidade { get; set; }
        public int Idade { get; set; }
        public double ValorMercado { get; set; }
        public double Salario { get; set; }
    }
}