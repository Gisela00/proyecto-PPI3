using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Berum_WebAPI.PDF.Components
{
    public class ListaItemsComponent : IComponent
    {
        private readonly string Titulo;
        private readonly string Icono;
        private readonly IEnumerable<string> Items;

        public ListaItemsComponent(string titulo, string icono, IEnumerable<string> items)
        {
            Titulo = titulo;
            Icono = icono;
            Items = items;
        }

        public void Compose(IContainer container)
        {
            container
                .Border(1)
                .BorderColor(Colors.Grey.Lighten5)
                .CornerRadius(5)
                .Padding(10)
                .Column(column =>
                {
                    column.Spacing(5);

                    // Encabezado
                    column.Item()
                        .Component(new TituloIconoComponent(Titulo, Icono));

                    // Items
                    foreach (var item in Items)
                    {
                        column.Item()
                            .PaddingLeft(10)
                            .PaddingVertical(3)
                            .Row(row =>
                            {
                                row.ConstantItem(12)
                                    .AlignTop()
                                    .Text("•")
                                    .FontSize(12)
                                    .FontColor("#B18434");

                                row.ConstantItem(8);

                                row.RelativeItem()
                                    .AlignTop()
                                    .Text(item)
                                    .FontSize(11)
                                    .FontColor("#222222");
                            });
                    }
                });
        }
    }
}