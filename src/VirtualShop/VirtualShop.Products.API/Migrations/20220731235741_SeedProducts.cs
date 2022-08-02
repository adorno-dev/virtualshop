using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations;
using VirtualShop.Products.API.Models;

#nullable disable

namespace VirtualShop.Products.API.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            var products = GetProducts();

            foreach (var p in products)
            {
                mb.Sql($"INSERT INTO Products (name,price,description,stock,ImageURL,categoryid) VALUES ('{p.Name}', {p.Price},  '{p.Description}', {p.Stock}, '{p.ImageURL}', {p.CategoryId})");    
            }

            // mb.Sql("INSERT INTO Products (name,price,description,stock,ImageURL,categoryid) VALUES ('Álcool em gell', 7.55,  'Caderno', 10, 'caderno1.jpg', 1)");
            // mb.Sql("INSERT INTO Products (name,price,description,stock,ImageURL,categoryid) VALUES ('Lapis', 3.45,  'Lapis preto', 20, 'lapis1.jpg', 1)");
            // mb.Sql("INSERT INTO Products (name,price,description,stock,ImageURL,categoryid) VALUES ('Clips', 5.33,  'Clips para papel', 50, 'clips1.jpg', 2)");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Products");
        }

        private IList<Product> GetProducts()
        {
            return new Product[] {
                new Product {
                    Name = "Álcool em gel",
                    Price = 15.00M,
                    Description = "Composto por 70% de alcool etilico, agua, emulsificantes (que dao a consistencia de gel) e, no caso de algumas marcas, extratos hidratantes como o aloe vera, e um grande aliado contra o Covid19. Isso porque e um produto eficiente na higienizacao das maos assim como de objetos e superficies.",
                    Stock = 1000,
                    ImageURL = "escolar/alcool-em-gel.jpg",
                    CategoryId = 1
                },
                new Product {
                    Name = "Apontador",
                    Price = 5.00M,
                    Description = "O seu uso e muito simples: basta inserir o lapis no furo e girar ate que a ponta fique no formato desejado. Todavia, evite colocar muita pressao e posicione o lapis reto. Desse modo, evitara a quebra da ponta e o desgaste desnecessario do item, contribuindo para que dure por mais tempo.",
                    Stock = 800,
                    ImageURL = "escolar/apontador-material-escolar.jpg",
                    CategoryId = 1
                },
                new Product {
                    Name = "Borracha",
                    Price = 2.50M,
                    Description = "Encontrase na cor branca, a mais tradicional, mas tambem verde escuro, azul, roxo e tons neons como amarelo, pink e laranja. Pode ter ou nao capa protetora e os modelos vao do basico aos que sao acoplados na ponta do lapis. Algumas opcoes possuem ate cheirinho de fruta!",
                    Stock = 600,
                    ImageURL = "escolar/borracha-material-escolar.jpg",
                    CategoryId = 1
                },
                new Product {
                    Name = "Caderno",
                    Price = 12.00M,
                    Description = "E o caso dos cadernos universitarios. Fabricados com capa dura, sao encontrados com acabamento em espiral e tambem brochura. Aqueles que apresentam espiral, permitem a fixacao de um numero maior de folhas, comportando 1, 10, 15 e ate 20 materias. Ou seja, sao perfeitos para alunos que gostam de manter todo conteudo organizado em um unico lugar. ",
                    Stock = 300,
                    ImageURL = "escolar/caderno-material-escolar.jpg",
                    CategoryId = 1
                },
                new Product {
                    Name = "Caneta esferografica",
                    Price = 4.75M,
                    Description = "Ainda sobre a ponta, de acordo com a milimetragem, possibilita a criacao de tracos finos ou grossos. Alem das cores basicas – azul, preta e vermelha – ha kits com opcoes coloridas – tons intensos e pasteis – que sao otimas para destacar palavras, grifar frases ou desenhar.",
                    Stock = 500,
                    ImageURL = "escolar/caneta-material-escolar.jpg",
                    CategoryId = 1
                },
                new Product {
                    Name = "Canetinhas",
                    Price = 24.00M,
                    Description = "Ha tambem as canetinhas magicas. Apos pintar com o modelo comum, e utilizada uma segunda que provoca mudanca de cor, possibilitando criar trabalhos com efeitos degrade. Costuma ser um produto lavavel, portanto, nao ha com que se preocupar caso a imaginacao dos pequenos va alem da folha de papel.",
                    Stock = 50,
                    ImageURL = "escolar/canetinha-material-escolar.jpg",
                    CategoryId = 1
                },
                new Product {
                    Name = "Cola branca",
                    Price = 7.50M,
                    Description = "A execucao de varios trabalhos manuais fica muito mais facil com o auxilio da cola branca. E utilizada para criar montagens, fixando fotos, recortes de jornal, retalhos de tecido, palito de sorvete, dentre outros materiais, em papel sulfite, pardo, papelao e cartolina. Como e um produto à base de agua, pode ser manipulado pelas criancas sem grandes preocupacoes. ",
                    Stock = 30,
                    ImageURL = "escolar/cola-material-escolar.jpg",
                    CategoryId = 1
                },
                new Product {
                    Name = "Compasso",
                    Price = 25.50M,
                    Description = "Com ele e possivel criar arcos e circulos perfeitos de diferentes diâmetros. Para isso, basta fixar a ponta de metal no papel, abrir o compasso de acordo com o tamanho da circunferencia desejada e, entao, movimentar a haste com o grafite. Alem do uso nas aulas de geometria, e uma ferramenta utilizada por profissionais que criam desenhos tecnicos como arquitetos, engenheiros e projetistas  ",
                    Stock = 250,
                    ImageURL = "escolar/compasso-material-escolar.jpg",
                    CategoryId = 1
                },
                new Product {
                    Name = "Corretivo",
                    Price = 7.45M,
                    Description = "Quando se escreve algo errado com o lapis, e muito facil apagar com o auxilio da borracha. Contudo, e quando o texto foi escrito à caneta? Tambem nao e motivo para desespero: gracas ao corretivo, da para corrigir.",
                    Stock = 200,
                    ImageURL = "escolar/corretivo-material-escolar.jpg",
                    CategoryId = 1
                },
                new Product {
                    Name = "Estojo escolar",
                    Price = 45.80M,
                    Description = "E muito comum adquirir um modelo pelo design, cores e estampas, todavia, antes de qualquer coisa, e fundamental analisar tudo o que sera armazenado. Dessa maneira, nao ha o risco de comprar um estojo lindo e moderno, mas que nao comporta tudo o que precisa.",
                    Stock = 150,
                    ImageURL = "escolar/estojo-material-escolar.jpg",
                    CategoryId = 1
                },
                new Product {
                    Name = "Lapis de cor",
                    Price = 50.00M,
                    Description = "Os lapis de cor acompanham os alunos desde os primeiros anos na escola. E a ferramenta que da vida aos elementos da imaginacao. Assim como as canetinhas, apresentam formatos diferentes: os redondos sao classicos enquanto os sextavados tem a vantagem de nao rolarem facilmente pela superficie, evitando quedas. Ja os triangulares, encaixam melhor entre os dedos dos pequenos, garantindo maior conforto.",
                    Stock = 30,
                    ImageURL = "escolar/lapis-de-cor-material-escolar.jpg",
                    CategoryId = 1
                },
                new Product {
                    Name = "Lapis preto",
                    Price = 8.00M,
                    Description = "Mais um item parceiro dos estudantes e o lapis preto. No inicio do periodo escolar, e utilizado para fazer tracos, figuras geometricas e as primeiras letrinhas. Quando o aluno se da conta, esta escrevendo longos textos com ele. Assim como o de cor, e encontrado nos formatos redondo, sextavado ou triangular. ",
                    Stock = 80,
                    ImageURL = "escolar/lapis-preto-material-escolar.jpg",
                    CategoryId = 1
                },
                new Product {
                    Name = "Marca-texto",
                    Price = 15.50M,
                    Description = "Na hora de estudar para a prova, ter um marca-texto por perto ajuda muito. Tambem chamado de marcador, e um tipo de caneta hidrografica com tinta translucida que permite destacar trechos importantes de um texto.",
                    Stock = 40,
                    ImageURL = "escolar/marca-texto-material-escolar.jpg",
                    CategoryId = 1
                },
                new Product {
                    Name = "Mascara",
                    Price = 10.00M,
                    Description = "Outro recurso de protecao que passou a integrar a lista de material escolar em razao da pandemia do Covid-19 e a mascara. Ha modelos descartaveis em TNT e tambem lavaveis confeccionadas com tecido.",
                    Stock = 300,
                    ImageURL = "acessorio/mascara-material-escolar.jpg",
                    CategoryId = 2
                },
                new Product {
                    Name = "Massinha de modelar",
                    Price = 22.70M,
                    Description = " Com textura macia e disponivel em varias cores (inclusive, com acabamento em glitter), e igualmente uma maneira divertida de promover o conhecimento sobre cores, formas e texturas, alem de estimular a socializacao. ",
                    Stock = 100,
                    ImageURL = "acessorio/massa-de-modelar-material-escolar.jpg",
                    CategoryId = 2
                },
                new Product {
                    Name = "Mochila",
                    Price = 65.50M,
                    Description = "E importante ressaltar, porem, que o visual e apenas uma das caracteristicas relevantes. Ao mesmo tempo, e preciso verificar se o produto e capaz de comportar todos os itens e se e confortavel.",
                    Stock = 100,
                    ImageURL = "acessorio/mochila-material-escolar.jpg",
                    CategoryId = 2
                },
                new Product {
                    Name = "Papel sulfite",
                    Price = 30.00M,
                    Description = " Encontra-se nas cores branca, rosa, verde, amarela, azul e bege (versao reciclada), assim como em gramaturas diferentes. Desse modo, atente-se a descricao para nao adquirir o produto errado.",
                    Stock = 500,
                    ImageURL = "escolar/papel-sulfite-chamequinho-material-escolar.jpg",
                    CategoryId = 1
                },
                new Product {
                    Name = "Pastas",
                    Price = 10.50M,
                    Description = "Pode ser com fechamento em elastico ou botao, feita de polipropileno, papel cartao ou ainda modelo catalogo com saquinhos plasticos. Independente do modelo pelo qual optar, os deveres estarao bem protegidos.",
                    Stock = 50,
                    ImageURL = "escolar/pasta-material-escolar.jpg",
                    CategoryId = 1
                },
                new Product {
                    Name = "Regua",
                    Price = 5.50M,
                    Description = "A regua e um instrumento usado para tracar retas, medir e fazer marcacoes indicando centimetros ou milimetros, auxiliando na criacao de formas geometricas e desenhos.",
                    Stock = 25,
                    ImageURL = "escolar/regua-material-escolar.jpg",
                    CategoryId = 1
                },
                new Product {
                    Name = "Tesoura",
                    Price = 15.00M,
                    Description = "A tesoura escolar e uma ferramenta muito util nas aulas de arte. Com ela, e possivel recortar gravuras, formas, numeros, entre outros, trabalhando a coordenacao motora e a atencao.",
                    Stock = 45,
                    ImageURL = "escolar/tesoura-material-escolar.jpg",
                    CategoryId = 1
                }
            };
        }
    }
}
