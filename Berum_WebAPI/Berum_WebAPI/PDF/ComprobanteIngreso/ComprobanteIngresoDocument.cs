using Berum_WebAPI.PDF.Componentes;
using Berum_WebAPI.PDF.Components;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Berum_WebAPI.PDF.ComprobanteIngreso
{
    public class ComprobanteIngresoDocument : IDocument
    {
        public ComprobanteIngresoModel Model { get; }

        public ComprobanteIngresoDocument(ComprobanteIngresoModel model)
        {
            Model = model;
        }
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
                            Titulo = "COMPROBANTE DE",
                            Subtitulo = "INGRESO",
                            NumeroServicio = Model.NumeroServicio
                        }));
                });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(10).Column(column =>
            {
                column.Item()
                      .Component(new BienvenidaComponent(
                          Model.Cliente.Nombre,
                          Model.FechaIngreso));

                column.Item()
                    .PaddingHorizontal(20)
                    .PaddingTop(15)
                    .Height(1)
                    .Background(Colors.Grey.Lighten2);

                column.Item()
                     .PaddingHorizontal(20)
                     .PaddingTop(20)
                     .Row(row =>
                     {
                         var imagenIsometrico = Model.TipoEquipo switch
                         {
                             "Notebook" => PdfAssets.LaptopIsometrico,
                             "Celular" => PdfAssets.CelularIsometrico,
                             "PC Escritorio" => PdfAssets.PcIsometrico,
                             "Monitor" => PdfAssets.MonitorIsometrico,
                             "Impresora" => PdfAssets.ImpresoraIsometrico,
                             _ => PdfAssets.LaptopIsometrico
                         };
                         var imagen = Model.TipoEquipo switch
                         {
                             "Notebook" => PdfAssets.Laptop,
                             "Celular" => PdfAssets.Celular,
                             "PC Escritorio" => PdfAssets.PC,
                             "Monitor" => PdfAssets.Monitor,
                             "Impresora" => PdfAssets.Impresora,
                             _ => PdfAssets.Laptop
                         };
                         // EQUIPO RECIBIDO
                         row.RelativeItem()
                             .AlignTop()
                             .Column(leftCol =>
                             {
                                 leftCol.Spacing(10);

                                 //EQUIPO
                                 leftCol.Item()
                                     .Border(1)
                                     .BorderColor(Colors.Grey.Lighten2)
                                     .CornerRadius(5)
                                     .Padding(10)
                                     .Row(r =>
                                     {
                                         r.RelativeItem(6)
                                             .Column(col =>
                                             {
                                                 col.Spacing(5);
                                                 col.Item().Component(new TituloIconoComponent("EQUIPO RECIBIDO", imagen));
                                                 col.Item().Component(new InfoRowComponent("Tipo de equipo:", Model.TipoEquipo));
                                                 col.Item().Component(new InfoRowComponent("Marca:", Model.Marca));
                                                 col.Item().Component(new InfoRowComponent("Modelo:", Model.Modelo, false));
                                             });

                                         r.ConstantItem(15);
                                         r.RelativeItem(2)
                                             .PaddingTop(55) 
                                             .AlignCenter()
                                             .Image(imagenIsometrico)
                                             .FitArea();
                                     });

                                 // PROBLEMA INFORMADO
                                 leftCol.Item()
                                     .Component(new ProblemaInformadoComponent(Model.ProblemaInformado));

                                 // IMPORTANTE
                                 leftCol.Item()
                                     .Component(new ImportanteComponent(
                                         "La recepción del equipo no implica la aceptación automática de un presupuesto ni el inicio de la reparación."));
                             });
                         // Separación
                         row.ConstantItem(15);

  
                         // Columna derecha 
                         row.RelativeItem()
                             .Column(col =>
                             {
                                 col.Spacing(10);

                                 // DATOS DEL CLIENTE
                                 col.Item()
                                     .Border(1)
                                     .BorderColor(Colors.Grey.Lighten2)
                                     .CornerRadius(5)
                                     .Padding(10)
                                     .Column(c =>
                                     {
                                         c.Spacing(5);
                                         c.Item().Component(new TituloIconoComponent("DATOS DEL CLIENTE", PdfAssets.User3));
                                         c.Item().Component(new InfoRowComponent("Nombre:", Model.Cliente.Nombre));
                                         c.Item().Component(new InfoRowComponent("Teléfono:", Model.Cliente.Telefono, true));
                                         c.Item().Component(new InfoRowComponent("Email:", Model.Cliente.Email, false));
                                     });

                                 // ACCESORIOS ENTREGADOS
                                 col.Item()
                                     .Border(1)
                                     .BorderColor(Colors.Grey.Lighten2)
                                     .CornerRadius(5)
                                     .Component(new ListaItemsComponent("ACCESORIOS ENTREGADOS", PdfAssets.AccesoriosEntregados, Model.AccesoriosEntregados));

                                 // ESTADO INICIAL OBSERVADO
                                 col.Item()
                                    .Border(1)
                                    .BorderColor(Colors.Grey.Lighten2)
                                    .CornerRadius(5)
                                    .Component(new ListaItemsComponent(
                                         "ESTADO INICIAL OBSERVADO",
                                         PdfAssets.Observado,          
                                         Model.EstadoInicial));
                             });

                         // PRÓXIMOS PASOS
                         column.Item().PaddingHorizontal(20).PaddingTop(15).Border(1).CornerRadius(5).BorderColor(Colors.Grey.Lighten2)
                             .Component(new ProximosPasosComponent(new List<PasoItem>
                             {
                                new PasoItem(PdfAssets.ObservadoIsometrico,         "Se realizará un diagnóstico técnico."),
                                new PasoItem(PdfAssets.DocumentoIsometrico,    "Se generará un presupuesto que será enviado para tu aprobación."),
                                new PasoItem(PdfAssets.ReparacionIsometrico, "Una vez aprobado, se procederá con la reparación."),
                                new PasoItem(PdfAssets.ConsultarEstadoIsometrico,      "Podrás consultar el estado del servicio con el número de orden.")
                             }));
                     });

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
