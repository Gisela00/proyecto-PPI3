using Berum_WebAPI.PDF.Componentes;
using Berum_WebAPI.PDF.Components;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
namespace Berum_WebAPI.PDF.Presupuesto
{
    public class PresupuestoDocument : IDocument
    {
        public PresupuestoModel Model { get; }

        public PresupuestoDocument(PresupuestoModel model)
        {
            Model = model;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Content().Element(ComposeContent);

                    page.Footer().PaddingHorizontal(20).PaddingBottom(5)
                      .Column(column =>
                      {
                          column.Item()
                              .PaddingBottom(10)
                              .Element(c => BuildSeparadorTitulo(c, "Tu equipo en las mejores manos"));

                          column.Item().Component(
                              new FooterContactoComponent(
                                  "+54 9 11 2320 3212",
                                  "@RepairShop",
                                  "www.RepairShop.com.ar",
                                  "Buenos Aires, CABA"
                              ));
                      });
                    page.Header().Component(new HeaderComponent(
                        new HeaderModel
                        {
                            Empresa = "RepairShop",
                            Descripcion = "SERVICIO TÉCNICO PROFESIONAL",
                            Titulo = "PRESUPUESTO DE",
                            Subtitulo = "REPARACIÓN",
                            NumeroServicio = Model.NumeroServicio
                        }));
                });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(20).Column(column =>
            {
                column.Item().PaddingHorizontal(50).Row(row =>
                {
                    
                    row.RelativeItem().Component(
                        new ClienteComponent(
                            "DATOS DEL CLIENTE",
                            Model.Cliente.Nombre,
                            Model.Cliente.Telefono,
                            Model.Cliente.Email));
                    row.ConstantItem(50);
                    row.RelativeItem().Component(
                        new PresupuestoDetalleComponent(
                            "DETALLES DE LA ORDEN",
                            Model.FechaEmision,
                            "10 días"));
                });
                column.Item().Height(15);
                column.Item().PaddingHorizontal(20).Component(new MontoComponent("MONTO ESTIMADO ──", Model.Monto));
                column.Item()
                      .PaddingHorizontal(20)
                      .PaddingTop(20)
                      .Row(row =>
                      {
                        var imagen = Model.TipoEquipo switch
                        {
                            "Notebook" => PdfAssets.Laptop,
                            "Celular" => PdfAssets.Celular,
                            "PC Escritorio" => PdfAssets.PC,
                            "Monitor" => PdfAssets.Monitor,
                            "Impresora" => PdfAssets.Impresora
                        };

                        row.RelativeItem(6)
                            .PaddingRight(10)
                            .Component(
                                new ReporteCompactoComponent(
                                    "DESCRIPCIÓN DEL TRABAJO",
                                    Model.DescripcionTrabajo,
                                    PdfAssets.Trabajo));

                        row.ConstantItem(1)
                           .PaddingVertical(15)
                           .Background(Colors.Grey.Lighten2);
                        row.RelativeItem(4)
                            .PaddingLeft(10)
                            .Component(
                                new ReporteCompactoComponent(
                                    "EQUIPO",
                                    new List<string>
                                    {
                                        $"Tipo: {Model.TipoEquipo}",
                                        $"Marca: {Model.Marca}",
                                        $"Modelo: {Model.Modelo}"
                                    },
                                    imagen));
                      });

                column.Item().PaddingHorizontal(20)
                      .PaddingTop(15)
                      .PaddingHorizontal(20)
                      .Height(1)
                      .Background(Colors.Grey.Lighten2);

                column.Item().PaddingHorizontal(20)

                    .PaddingTop(20)
                    .Component(
                        new ReporteComponent(
                            "FALLAS DETECTADAS",
                            Model.FallasDetectadas, PdfAssets.Falla));

                column.Item().PaddingHorizontal(20)
                      .PaddingTop(15)
                      .Height(1)
                      .Background(Colors.Grey.Lighten2);

                column.Item().PaddingHorizontal(20)
                    .PaddingTop(20)
                    .Component(
                        new ReporteComponent(
                            "OBSERVACIONES DEL TÉCNICO",
                            Model.ObservacionesTecnico, PdfAssets.Clipboard2));

                column.Item().PaddingHorizontal(20)
                      .PaddingTop(15)
                      .Height(1)
                      .Background(Colors.Grey.Lighten2);
            });
        }

        private void BuildSeparadorTitulo(IContainer container, string texto)
        {
            container.Row(row =>
            {
                row.RelativeItem().AlignMiddle()
                    .Height(1)
                    .Background("#D9A33F");

                row.AutoItem().PaddingHorizontal(15)
                    .Text(texto.ToUpper())
                    .FontSize(9)
                    .FontColor("#D9A33F")
                    .LetterSpacing(0.15f);

                row.RelativeItem().AlignMiddle()
                    .Height(1)
                    .Background("#D9A33F");
            });
        }
    }

}
