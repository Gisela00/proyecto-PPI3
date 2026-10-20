using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
namespace Berum_WebAPI.PDF.Components
{
    public class HeaderComponent : IComponent
    {
        private readonly HeaderModel model;

        public HeaderComponent(HeaderModel headerModel)
        {
            model = headerModel;
        }

        public void Compose(IContainer container)
        {
            container.Column(column =>
            {
                // Encabezado
                column.Item()
                    .Background("#F8F8F8")
                    .Padding(20)
                    .Row(row =>
                    {
                        // Empresa
                        row.RelativeItem().Element(c =>
                        {
                            c.Padding(10)
                                .AlignCenter()
                                .Column(col =>
                                {
                                    col.Item()
                                        .Text(model.Empresa)
                                        .FontSize(32)
                                        .Bold()
                                        .FontColor("#101B2F");

                                    col.Item()
                                        .Text(model.Descripcion)
                                        .FontSize(10)
                                        .Bold()
                                        .FontColor("#B18434");
                                });
                        });

                        // Separador
                        row.ConstantItem(1)
                            .PaddingVertical(15)
                            .Background(Colors.Grey.Lighten2);

                        // Título del documento
                        row.RelativeItem().Element(c =>
                        {
                            c.Padding(10)
                                .AlignCenter()
                                .Column(col =>
                                {
                                    col.Item()
                                        .Text(model.Titulo)
                                        .FontSize(18)
                                        .Bold()
                                        .FontColor("#101B2F");

                                    col.Item()
                                        .Text(model.Subtitulo)
                                        .FontSize(20)
                                        .ExtraBold()
                                        .FontColor("#101B2F");
                                });
                        });

                        //número de servicio
                        row.AutoItem()
                          .AlignMiddle()
                          .Element(container =>
                          {
                              container
                                  .MinWidth(140)
                                  .Border(2)
                                  .BorderColor("#A57A1E")
                                  .CornerRadius(8)
                                  .Background("#CFA43E")
                                  .PaddingHorizontal(14) 
                                  .PaddingVertical(6)   
                                  .Column(column =>
                                  {
                                      column.Spacing(1);

                                      column.Item()
                                          .AlignCenter()
                                          .Text("ORDEN N°")
                                          .FontSize(7)
                                          .SemiBold()
                                          .FontColor("#1A1A1A");

                                      column.Item()
                                          .AlignCenter()
                                          .Text(model.NumeroServicio)
                                          .FontSize(18) 
                                          .ExtraBold()
                                          .FontColor("#0F1D38");
                                  });
                          });
                    });

                // Línea inferior
                column.Item()
                    .PaddingHorizontal(20)
                    .Height(1)
                    .Background(Colors.Grey.Lighten2);
            });
        }
    }
}
