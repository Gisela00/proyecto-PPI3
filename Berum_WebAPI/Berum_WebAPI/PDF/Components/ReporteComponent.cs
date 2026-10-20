using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
namespace Berum_WebAPI.PDF.Componentes
{
    public class ReporteComponent : IComponent
    {
        private readonly string Titulo;
        private readonly List<string> Items;
        private readonly string Image;
        public ReporteComponent(string titulo, List<string> items, string image)
        {
            Titulo = titulo;
            Items = items;
            Image = image;
        }

        public void Compose(IContainer container)
        {
            container.Row(row =>
            {
                // Icono
                row.ConstantItem(55).AlignMiddle().Element(c =>
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

                // Línea vertical
                row.ConstantItem(8).Element(c =>
                {
                    c.Width(2)
                     .MinHeight(50)
                     .Background("#C89D3D");
                });

                // Contenido
                row.RelativeItem().PaddingLeft(10)
                .Column(col =>
                {
                    // Título
                    col.Item().Row(r =>
                    {
                        // Título
                        col.Item()
                            .Text(Titulo)
                            .Bold()
                            .FontSize(13)
                            .FontColor("#1B2340");

                        // Línea horizontal debajo
                        col.Item()
                            .PaddingTop(4)
                            .Element(l =>
                            {
                                l.Width(40)
                                 .Height(3)
                                 .Background("#C89D3D");
                            });
                    });
                    col.Item().Height(5);

                    // Lista
                    foreach (var item in Items)
                    {
                        col.Item()
                            .PaddingTop(3)
                            .Text($"{item}")
                            .FontSize(10);
                    }
                });
            });
        }
    }
}

