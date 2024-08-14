using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemConstrutor
    {
        [Theory]
        [InlineData("", null, "2024-01-01", "2024-01-02", 0, false)]
        [InlineData("Origem teste", "Destino Teste", "2024-01-01", "2024-01-02", 100, true)]
        public void RetornaEhValidoDeAcordoComDadosDeEntrada(string origim, string destino, string dataIda, string dataVolta, double preco, bool validacao)
        {
            Rota rota = new Rota(origim, destino);
            Periodo periodo = new Periodo(DateTime.Parse(dataIda), DateTime.Parse(dataVolta));

            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            Assert.Equal(validacao, oferta.EhValido);
        }

        [Fact]
        public void RetornaMensagemDeErroDeRotaNulaQuandoForPassadoValorNulo()
        {
            Rota rota = null;
            Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
            double preco = 100.0;

            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            Assert.Contains("A oferta de viagem não possui rota ou período válidos.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Fact]
        public void RetornaMensagemDeErroQuandoPrecoInvalidoQuandoPrecoMenorQueZero()
        {
            //arrange
            Rota rota = new Rota("Origem teste", "Destino Teste");
            Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
            double preco = -100.0;

            //act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //assert
            Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);

        }

        [Fact]
        public void RetornaTresErrosDeValidacaoQuandoRotaPeriodoERecoSaoInvalidos()
        {
            //arrange
            int quantidadeEsperada = 3;
            Rota rota = null;
            Periodo periodo = new Periodo(new DateTime(2024, 6, 1), new DateTime(2024, 5, 10));
            double preco = -100.0;
            //assert

            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);
            //act
            Assert.Equal(quantidadeEsperada, oferta.Erros.Count());
        }
    }
}