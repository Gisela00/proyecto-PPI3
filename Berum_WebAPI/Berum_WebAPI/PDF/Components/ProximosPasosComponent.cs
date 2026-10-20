using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Berum_WebAPI.PDF.Components
{
    public class PasoItem
    {
        public string Icono { get; }
        public string Descripcion { get; }

        public PasoItem(string icono, string descripcion)
        {
            Icono = icono;
            Descripcion = descripcion;
        }
    }

    public class ProximosPasosComponent : IComponent
    {
        private readonly IEnumerable<PasoItem> Pasos;

        public ProximosPasosComponent(IEnumerable<PasoItem> pasos)
        {
            Pasos = pasos;
        }

        public void Compose(IContainer container)
        {
            container
                .Border(1)
                .BorderColor(Colors.Grey.Lighten2)
                .CornerRadius(5)
                .Padding(15)
                .Column(column =>
                {
                    column.Spacing(15);

                    // Encabezado
                    column.Item()
                        .Component(new TituloIconoComponent(
                            "PRÓXIMOS PASOS",
                            PdfAssets.Clipboard2));

                    // Pasos
                    column.Item()
                        .Row(row =>
                        {
                            int numero = 1;
                            foreach (var paso in Pasos)
                            {
                                int n = numero;

                                row.RelativeItem()
                                    .AlignCenter()
                                    .Column(col =>
                                    {
                                        col.Spacing(6);

                                        // Número + Ícono
                                        col.Item()
                                            .AlignCenter()
                                            .Row(r =>
                                            {
                                                // Número circulado
                                                r.AutoItem()
                                                    .AlignBottom()
                                                    .Width(18)
                                                    .Height(18)
                                                    .Background("#0F2344")
                                                    .CornerRadius(9)
                                                    .AlignCenter()
                                                    .AlignMiddle()
                                                    .Text(n.ToString())
                                                    .FontSize(9)
                                                    .Bold()
                                                    .FontColor("#FFFFFF");

                                                r.ConstantItem(6);

                                                // Ícono
                                                r.AutoItem()
                                                    .Width(32)
                                                    .Height(32)
                                                    .Image(paso.Icono)
                                                    .FitArea();
                                            });

                                        // Descripción
                                        col.Item()
                                            .AlignCenter()
                                            .Text(paso.Descripcion)
                                            .FontSize(7)
                                            .FontColor("#444444")
                                            .AlignCenter();
                                    });

                                numero++;
                            }
                        });
                });
        }
    }
}