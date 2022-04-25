

using Projeto.Models;

namespace Projeto.Data {

    public class SeedingService {

        private AppDbContext _appDbContext;

        public SeedingService(AppDbContext appDbContext) {

            _appDbContext = appDbContext;
        }

        public void Seed() {

            if (_appDbContext.Categorias.Any() || _appDbContext.Produtos.Any()) {

                return; // DB foi populado
            }

            Categoria c1 = new Categoria("Vinho Tinto", "O vinho tinto é um vinho tranquilo de cor vermelha e que possui níveis consideráveis de tanino. São elaborados necessariamente com castas tintas.");

            Categoria c2 = new Categoria("Vinho Branco", "Vinhos brancos são vinhos produzidos, em sua maioria, a partir de uvas brancas como Chardonnay e Sauvignon Blanc, e sua coloração - que varia do mais pálido amarelo-esverdeado até o dourado-âmbar, ");

            Produto p1 = new Produto("Vinho Marques de Casa Concha Cabernet Sauvignon", "Marques Cabernet Sauvignon é um vinho tipico chileno",
            "Marques Cabernet Sauvignon é um vinho tipico chileno, elegante e equilibrado, elaborado com uvas provenientes de Puente Alto aos pés da Cordilhera dos Andes. Vermelho profundo, possui grande concentração de sabores moldurados por seus taninos firmes. Uma textura suave, quase sedosa, que cobre a firme estrutura tânica que aparece no início do seu longo final.", 144, 50, "~/images/vinhoTinto.jpg", 1.5, c1);

            Produto p2 = new Produto("Vinho Branco Monte Da Serra", "Vinho Regional Tejo, Cor palha. Nariz de fruta madura (abacaxi e manga) e floral (rosa). Na boca é leve e macio, final frutado. ", "CARACTERÍSTICAS ORGANOLÉPTICAS: Cor palha com tons esverdeados. Nariz bem marcado com fruta madura, em especial, abacaxi e manga combinado com floral (rosa). O passo na boca é leve, contagiante pela maciez e intenso final frutado", 85, 63, "~/images/vinhoBranco.jpg", 1.2, c2);

            Produto p3 = new Produto("VINHO BRANCO SECO RIESLING CASTELLAMARE", "De cor verde dourada.Apresenta um aroma fino, elegante e frutado.É um vinho leve e de grande frescor", "De cor verde dourada.Apresenta um aroma fino, elegante e frutado.É um vinho leve e de grande frescor. Ideal para acompanhar peixes, queijos leves e carnes brancas em geral.É ótimo também como aperitivo.", 35, 50, "~/images/vinhoBranco2.jpg", 1.2, c2);

            Produto p4 = new Produto("Valparaíso Vinho Branco Vitale Chardonnay (NATURAL)", "Vinho Chardonnay de vinificação natural, com leveduras selvagens, sem correção e não filtrado.", "Vinho Chardonnay de vinificação natural, com leveduras selvagens, sem correção e não filtrado. Aroma de frutas tropicais, abacaxi, pêssego e banana. Acidez equilibrada e leve dulçor em boca. Ideal para acompanhar carnes brancas, queijos leves e canapés.", 99, 36, "~/images/vinhoBranco4.jpg", 1.2, c2);

            Produto p5 = new Produto("Vinho Tinto Português Porto Ruby Dom José 750ml", "Vinho Porto Dom Jose Ruby 750ml possui cor ruby e nuances violeta, rico em aromas de frutos silvestres.", "Vinho Porto Dom Jose Ruby 750ml possui cor ruby e nuances violeta, rico em aromas de frutos silvestres. É caracterizado por sua suavidade e harmonia no paladar. Produzido com uvas viníferas tintas européias. Envelhecido durante 3 anos em cascos de madeira.", 142, 30, "~/images/vinhoTinto2.jpg", 1.5, c1);

            Produto p6 = new Produto("Vinho Tinto De Mesa Quinta Jubair Bordô Suave - 750ml", "Vinho tinto 100% Bordô de aspecto límpido e brilhante. Suave, doce e bem estruturado.", "Em seu aroma, predominam as frutas vermelhas. Servir: entre 8ºC à 10ºC. Graduação Alcoólica: 10,5%vol. Volume: 750ml Harmonização: Acompanha carne. Vinho tinto 100% Bordô de aspecto límpido e brilhante. Suave, doce e bem estruturado. Em seu aroma, predominam as frutas vermelhas.", 62, 55, "~/images/vinhoTinto3.jpg", 1.5, c1);


            _appDbContext.Categorias.AddRange(c1, c2);
            _appDbContext.Produtos.AddRange(p1, p2, p3, p4, p5, p6);

            _appDbContext.SaveChanges();
        }

    }
}