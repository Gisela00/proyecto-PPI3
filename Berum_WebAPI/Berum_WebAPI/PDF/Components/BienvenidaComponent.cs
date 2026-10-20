using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Berum_WebAPI.PDF.Components
{
    public class BienvenidaComponent : IComponent
    {
        private readonly string Nombre;
        private readonly DateTime FechaIngreso;

        public BienvenidaComponent(string nombre, DateTime fechaIngreso)
        {
            Nombre = nombre;
            FechaIngreso = fechaIngreso;
        }

        public void Compose(IContainer container)
        {
            container
                .PaddingHorizontal(20)
                .Row(row =>
                {
                    // ── Saludo ──
                    row.RelativeItem()
                        .Column(col =>
                        {
                            col.Item()
                                .Text(text =>
                                {
                                    text.Span("Hola, ")
                                        .FontSize(16)
                                        .SemiBold()
                                        .FontColor("#101B2F");

                                    text.Span(Nombre)
                                        .FontSize(16)
                                        .ExtraBold()
                                        .FontColor("#B18434");
                                });

                            col.Item()
                                .PaddingTop(4)
                                .Text("Hemos recibido tu equipo para su diagnóstico y/o reparación.")
                                .FontSize(10)
                                .FontColor("#666666");
                        });

                    // ── Fecha ──
                    row.AutoItem()
                        .AlignMiddle()
                        .Column(col =>
                        {
                            col.Item()
                                .AlignRight()
                                .Row(r =>
                                {
                                    r.AutoItem()
                                        .AlignMiddle()
                                        .Width(22)
                                        .Height(22)
                                        .Image(PdfAssets.CalendarioIngreso);

                                    r.ConstantItem(8);

                                    r.AutoItem()
                                        .AlignMiddle()
                                        .Column(c =>
                                        {
                                            c.Item()
                                                .Text("FECHA DE INGRESO")
                                                .FontSize(8)
                                                .SemiBold()
                                                .FontColor("#666666");

                                            c.Item()
                                                .Text(FechaIngreso.ToString("dd/MM/yyyy - HH:mm") + " hs")
                                                .FontSize(12)
                                                .SemiBold()
                                                .FontColor("#101B2F");
                                        });
                                });
                        });
                });
        }
    }
}