using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace ProvaAdmissionalCSharpApisul
{
    public interface IElevadorService
    {
        /// <summary> Deve retornar uma List contendo o(s) andar(es) menos utilizado(s). </summary> 
        List<int> andarMenosUtilizado();

        /// <summary> Deve retornar uma List contendo o(s) elevador(es) mais frequentado(s). </summary> 
        List<char> elevadorMaisFrequentado();

        /// <summary> Deve retornar uma List contendo o período de maior fluxo de cada um dos elevadores mais frequentados (se houver mais de um). </summary> 
        List<char> periodoMaiorFluxoElevadorMaisFrequentado();

        /// <summary> Deve retornar uma List contendo o(s) elevador(es) menos frequentado(s). </summary> 
        List<char> elevadorMenosFrequentado();

        /// <summary> Deve retornar uma List contendo o período de menor fluxo de cada um dos elevadores menos frequentados (se houver mais de um). </summary> 
        List<char> periodoMenorFluxoElevadorMenosFrequentado();

        /// <summary> Deve retornar uma List contendo o(s) periodo(s) de maior utilização do conjunto de elevadores. </summary> 
        List<char> periodoMaiorUtilizacaoConjuntoElevadores();

        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador A em relação a todos os serviços prestados. </summary> 
        float percentualDeUsoElevadorA();

        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador B em relação a todos os serviços prestados. </summary> 
        float percentualDeUsoElevadorB();

        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador C em relação a todos os serviços prestados. </summary> 
        float percentualDeUsoElevadorC();

        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador D em relação a todos os serviços prestados. </summary> 
        float percentualDeUsoElevadorD();

        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador E em relação a todos os serviços prestados. </summary> 
        float percentualDeUsoElevadorE();
    }

    class ElevadorService : IElevadorService
    {
        List<DadoUsoElevador> lDadoUso = new List<DadoUsoElevador>();
        int quantidadeAndar = 16;

        public void deserializeDado()
        {
            using (StreamReader reader = new StreamReader("input.json"))
            {
                string jsonString = reader.ReadToEnd();
                lDadoUso = JsonConvert.DeserializeObject<List<DadoUsoElevador>>(jsonString);
            }
        }

        public int[] contadorAndar()
        {
            int[] aQuantidadeAndar = new int[quantidadeAndar];

            foreach (var item in lDadoUso)
            {
                aQuantidadeAndar[item.andar]++;
            }
            return aQuantidadeAndar;
        }

        public int[] contadorElevador(out List<string> lElevador)
        {
            string[] aElevador = new string[lDadoUso.Count];

            for (int i = 0; i < lDadoUso.Count; i++)
            {
                aElevador[i] = lDadoUso[i].elevador;
            }
            Array.Sort(aElevador, StringComparer.InvariantCulture);
            int[] aRankingElevador = new int[aElevador.ToList().Distinct().Count()];
            lElevador = aElevador.ToList().Distinct().ToList();

            for (int i = 0; i < aElevador.ToList().Distinct().Count(); i++)
            {
                aRankingElevador[i] = lDadoUso.FindAll(e => e.Equals(aElevador[i].ToList().Distinct())).Count;
            }
            return aRankingElevador;
        }

        public List<int> andarMenosUtilizado()
        {
            List<int> lAndarMenosUtilizado = new List<int>();
            lAndarMenosUtilizado.Add(Array.BinarySearch(contadorAndar(), contadorAndar().ToList().Min()));
            return lAndarMenosUtilizado;
        }

        public List<char> elevadorMaisFrequentado()
        {
            List<char> lElevadorMaisFrequentado = new List<char>();
            List<string> lElevador = new List<string>();
            lElevadorMaisFrequentado.Add(Convert.ToChar(lElevador[contadorElevador(out lElevador).ToList().Max()]));
            return lElevadorMaisFrequentado;
        }

        public List<char> elevadorMenosFrequentado()
        {
            List<char> lElevadorMenosFrequentado = new List<char>();
            List<string> lElevador = new List<string>();
            lElevadorMenosFrequentado.Add(Convert.ToChar(lElevador[contadorElevador(out lElevador).ToList().Min()]));
            return lElevadorMenosFrequentado;
        }

        public float percentualDeUsoElevadorA()
        {
            List<string> lElevador = new List<string>();
            int vIndice = lElevador.BinarySearch("A");
            int vValorElevador = Array.BinarySearch(contadorElevador(out lElevador), vIndice);
            int vTotalElevador = 0;

            for (int i = 0; i < contadorElevador(out lElevador).Length; i++)
            {
                vTotalElevador += contadorElevador(out lElevador)[i];
            }
            float vPercentuarDeUsoElevadorA = ((vValorElevador / 100) * vTotalElevador);
            return vPercentuarDeUsoElevadorA;
        }

        public float percentualDeUsoElevadorB()
        {
            List<string> lElevador = new List<string>();
            int vIndice = lElevador.BinarySearch("B");
            int vValorElevador = Array.BinarySearch(contadorElevador(out lElevador), vIndice);
            int vTotalElevador = 0;

            for (int i = 0; i < contadorElevador(out lElevador).Length; i++)
            {
                vTotalElevador += contadorElevador(out lElevador)[i];
            }
            float vPercentuarDeUsoElevadorB = ((vValorElevador / 100) * vTotalElevador);
            return vPercentuarDeUsoElevadorB;
        }

        public float percentualDeUsoElevadorC()
        {
            List<string> lElevador = new List<string>();
            int vIndice = lElevador.BinarySearch("C");
            int vValorElevador = Array.BinarySearch(contadorElevador(out lElevador), vIndice);
            int vTotalElevador = 0;

            for (int i = 0; i < contadorElevador(out lElevador).Length; i++)
            {
                vTotalElevador += contadorElevador(out lElevador)[i];
            }
            float vPercentuarDeUsoElevadorC = ((vValorElevador / 100) * vTotalElevador);
            return vPercentuarDeUsoElevadorC;
        }

        public float percentualDeUsoElevadorD()
        {
            List<string> lElevador = new List<string>();
            int vIndice = lElevador.BinarySearch("D");
            int vValorElevador = Array.BinarySearch(contadorElevador(out lElevador), vIndice);
            int vTotalElevador = 0;

            for (int i = 0; i < contadorElevador(out lElevador).Length; i++)
            {
                vTotalElevador += contadorElevador(out lElevador)[i];
            }
            float vPercentuarDeUsoElevadorD = ((vValorElevador / 100) * vTotalElevador);
            return vPercentuarDeUsoElevadorD;
        }

        public float percentualDeUsoElevadorE()
        {
            List<string> lElevador = new List<string>();
            int vIndice = lElevador.BinarySearch("E");
            int vValorElevador = Array.BinarySearch(contadorElevador(out lElevador), vIndice);
            int vTotalElevador = 0;

            for (int i = 0; i < contadorElevador(out lElevador).Length; i++)
            {
                vTotalElevador += contadorElevador(out lElevador)[i];
            }
            float vPercentuarDeUsoElevadorE = ((vValorElevador / 100) * vTotalElevador);
            return vPercentuarDeUsoElevadorE;
        }

        //PERDÃO, INFELIZMENTE NÃO FUI CAPAZ DE CONCLUIR COM O TEMPO QUE TIVE PARA DISPOR.
        public List<char> periodoMaiorFluxoElevadorMaisFrequentado()
        {
            throw new NotImplementedException();
        }

        public List<char> periodoMaiorUtilizacaoConjuntoElevadores()
        {
            throw new NotImplementedException();
        }

        public List<char> periodoMenorFluxoElevadorMenosFrequentado()
        {
            throw new NotImplementedException();
        }
    }

    class DadoUsoElevador
    {
        public int andar { get; set; }
        public string elevador { get; set; }
        public string turno { get; set; }
    }
}