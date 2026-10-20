using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
namespace Berum_WebAPI.PDF.Componentes
{
    public class ReporteCompactoComponent : IComponent
    {
        private readonly string Titulo;
        private readonly List<string> Items;
        private readonly string Image;

        public ReporteCompactoComponent(string titulo, List<string> items, string image)
        {
            Titulo = titulo;
            Items = items;
            Image = image;
        }

        public void Compose(IContainer container)
        {
            var esEquipo = Titulo == "EQUIPO";

            container.Row(row =>
            {
                // ICONO
                row.ConstantItem(55).Element(c =>
                {
                    c.Width(45)
                     .Height(45)
                     .Background("#0F2344")
                     .CornerRadius(25)
                     .AlignCenter()
                     .AlignMiddle()
                     .Padding(8)
                     .Image(Image);
                });

                // LINEA DORADA VERTICAL
                row.ConstantItem(8).Element(c =>
                {
                    c.Width(2)
                     .MinHeight(60)
                     .Background("#C89D3D");
                });

                // CONTENIDO
                row.RelativeItem()
                   .PaddingLeft(16)
                   .Column(col =>
                   {
                       // TITULO
                       col.Item()
                          .Text(Titulo)
                          .Bold()
                          .FontSize(15)
                          .FontColor("#1B2340");

                       // LINEA DORADA
                       col.Item()
                          .PaddingTop(4)
                          .Width(40)
                          .Height(3)
                          .Background("#C89D3D");

                       col.Item().Height(5);

                       // ITEMS
                       foreach (var item in Items)
                       {
                           if (esEquipo)
                           {
                               var partes = item.Split(':', 2);

                               var etiqueta = partes[0];
                               var valor = partes.Length > 1
                                   ? partes[1].Trim()
                                   : "";

                               col.Item()
                                  .PaddingBottom(4)
                                  .Row(r =>
                                  {
                                      r.ConstantItem(55)
                                       .Text($"{etiqueta}:")
                                       .FontSize(10)
                                       .FontColor("#666666");

                                      r.RelativeItem()
                                       .Text(valor)
                                       .FontSize(10)
                                       .SemiBold()
                                       .FontColor("#222222");
                                  });
                           }
                           else
                           {
                               col.Item()
                                  .PaddingTop(3)
                                  .Text($"{item}")
                                  .FontSize(10)
                                  .FontColor("#222222");
                           }
                       }
                   });
            });
        }
    }
}