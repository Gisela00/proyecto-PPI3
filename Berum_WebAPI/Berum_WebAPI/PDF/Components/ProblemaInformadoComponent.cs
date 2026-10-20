using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Berum_WebAPI.PDF.Components
{
    public class ProblemaInformadoComponent : IComponent
    {
        private readonly string Texto;

        public ProblemaInformadoComponent(string texto)
        {
            Texto = texto;
        }

        public void Compose(IContainer container)
        {
            container
                .Border(1)
                .BorderColor(Colors.Grey.Lighten2)
                .CornerRadius(5)
                .Padding(10)
                .Column(column =>
                {
                    column.Spacing(8);

                    column.Item()
                        .Component(new TituloIconoComponent(
                            "PROBLEMA INFORMADO",
                            PdfAssets.ProblemaInformado)); 

                    column.Item()
                        .Background("#F8F8F8")
                        .CornerRadius(5)
                        .Padding(15)
                        .Row(row =>
                        {
                            row.ConstantItem(25)
                                .AlignTop()
                                .Text("❝")
                                .FontSize(28)
                                .FontColor(Colors.Grey.Lighten1);

                            row.RelativeItem()
                                .AlignMiddle()
                                .Text(Texto)
                                .FontSize(11)
                                .FontColor("#444444")
                                .Italic();

                            row.ConstantItem(25)
                                .AlignBottom()
                                .Text("❞")
                                .FontSize(28)
                                .FontColor(Colors.Grey.Lighten1);
                        });
                });
        }
    }
}