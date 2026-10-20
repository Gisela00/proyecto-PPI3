using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
namespace Berum_WebAPI.PDF.Components
{
    public class TituloIconoComponent : IComponent
    {
        private readonly string Titulo;
        private readonly string Image;
        public TituloIconoComponent(string titulo, string image)
        {
            Titulo = titulo;
            Image = image;
        }
        public void Compose(IContainer container)
        {
            container.Row(row =>
            {
                // icono
                row.ConstantItem(40).AlignMiddle().Element(c =>
                {
                    c.Width(35)
                     .Height(35)
                     .Background("#0F2344")
                     .CornerRadius(25)
                     .AlignCenter()
                     .AlignMiddle()
                     .Padding(8)
                     .Image(Image);
                });

                // TITULO + LINEA
                row.RelativeItem().AlignMiddle().PaddingLeft(5).Column(col =>
                {
                    col.Item()
                        .Text(Titulo)
                        .Bold()
                        .FontSize(13)
                        .FontColor("#1B2340");

                    col.Item()
                        .PaddingTop(4)
                        .Element(l =>
                        {
                            l.Width(40)
                             .Height(3)
                             .Background("#C89D3D");
                        });
                });
            });
        }
    }
}
