using System;
using API.Models;

namespace API.Models
{
    public class IMC
    {
        public int Id { get; set; } 
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }
        public float Peso { get; set; }
        public float Altura { get; set; }
        public string Classificacao { get; set; }
        public float ValorIMC { get; set; }  
        public DateTime DataCriacao { get; set; }  

        public IMC(int alunoId, Aluno aluno, float peso, float altura, string classificacao, float valorIMC, DateTime dataCriacao)
        {
            AlunoId = alunoId;
            Aluno = aluno;
            Peso = peso;
            Altura = altura;
            Classificacao = classificacao;
            ValorIMC = valorIMC;
            DataCriacao = dataCriacao;
        }
    }
}
