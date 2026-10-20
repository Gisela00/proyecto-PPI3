using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Berum_WebAPI.PDF.Components
{
    public class ImportanteComponent : IComponent
    {
        private readonly string Texto;

        public ImportanteComponent(string texto)
        {
            Texto = texto;
        }

        public void Compose(IContainer container)
        {
            container
                .Background("#FDF6E3")
                .CornerRadius(5)
                .Padding(15)
                .Row(row =>
                {
                    // Ícono escudo
                    row.ConstantItem(45)
                        .AlignMiddle()
                        .Element(c =>
                        {
                            c.Width(38)
                             .Height(38)
                             .Background("#0F2344")
                             .CornerRadius(20)
                             .AlignCenter()
                             .AlignMiddle()
                             .Padding(8)
                             .Image(PdfAssets.Importante); 
                        });

                    row.ConstantItem(12);

                    row.RelativeItem()
                        .AlignMiddle()
                        .Column(col =>
                        {
                            col.Item()
                                .Text("IMPORTANTE")
                                .FontSize(12)
                                .Bold()
                                .FontColor("#101B2F");

                            col.Item()
                                .PaddingTop(3)
                                .Text(Texto)
                                .FontSize(10)
                                .FontColor("#444444");
                        });
                });
        }
    }
}