using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
namespace Berum_WebAPI.PDF.Componentes
{
    public class InfoRowComponent : IComponent
    {
        public string Icon { get; }
        public string Label { get; }
        public string Value { get; }
        public bool MostrarLinea { get; }

        public InfoRowComponent(string label, string value, bool mostrarLinea = true)
        {
            Icon = null;
            Label = label;
            Value = value;
            MostrarLinea = mostrarLinea;
        }

        public InfoRowComponent(string icon, string label, string value, bool mostrarLinea = true)
        {
            Icon = icon;
            Label = label;
            Value = value;
            MostrarLinea = mostrarLinea;
        }

        public void Compose(IContainer container)
        {
            container.Column(column =>
            {
                column.Item()
                    .PaddingVertical(6)
                    .Row(row =>
                    {
                        if (!string.IsNullOrWhiteSpace(Icon))
                        {
                            row.ConstantItem(18)
                                .AlignMiddle()
                                .Height(18)
                                .Image(Icon);

                            row.ConstantItem(10);
                        }

                        row.ConstantItem(110)
                            .AlignMiddle()
                            .Text(Label)
                            .FontSize(11)
                            .FontColor("#666666");

                        row.RelativeItem(3)
                            .AlignMiddle()
                            .Text(Value)
                            .FontSize(8)
                            .SemiBold()
                            .FontColor("#222222");
                    });

                if (MostrarLinea)
                {
                    column.Item()
                        .PaddingTop(3)
                        .LineHorizontal(1)
                        .LineColor(Colors.Grey.Lighten2);
                }
            });
        }
    }
}
