using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
namespace Berum_WebAPI.PDF.Componentes
{
    public class PresupuestoDetalleComponent : IComponent
    {
        private readonly string Title;
        private readonly DateTime FechaEmision;
        private readonly string Validez;


        public PresupuestoDetalleComponent(string title, DateTime fechaEmision, string validez)
        {
            Title = title;
            FechaEmision = fechaEmision;
            Validez = validez;
        }

        public void Compose(IContainer container)
        {
            container.Column(column =>
            {
                column.Spacing(5);

                // Encabezado
                column.Item()
                    .BorderBottom(1)
                    .BorderColor("#B18434")
                    .PaddingBottom(5)
                    .Row(row =>
                    {
                        row.ConstantItem(16)
                            .Image(PdfAssets.Clipboard);

                        row.ConstantItem(5);

                        row.RelativeItem()
                            .Text(Title)
                            .FontColor("#0F1A2E")
                            .Bold();
                    });

                // Filas
                column.Item().Component(new InfoRowComponent(
                    PdfAssets.Calendario,
                    "Fecha de emisión:",
                    FechaEmision.ToString("dd/MM/yyyy")));

                column.Item().Component(new InfoRowComponent(
                    PdfAssets.Reloj,
                    "Validez:",
                    "14 días"));

            });
        }

    }
}
