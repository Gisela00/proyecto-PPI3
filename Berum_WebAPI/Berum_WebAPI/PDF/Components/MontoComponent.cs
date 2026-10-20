using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
namespace Berum_WebAPI.PDF.Componentes
{
    public class MontoComponent : IComponent
    {
        private readonly string Title;
        private readonly decimal Monto;

        public MontoComponent(string title, decimal monto)
        {
            Title = title;
            Monto = monto;
        }
        public void Compose(IContainer container)
        {
            container.Column(column =>
            {
                column.Item().Border(1, Colors.Black)
                    .Background("#0F1C32")
                    .CornerRadius(10)
                    .Padding(25)
                    .Column(inner =>
                    {
                        // Título
                        inner.Item().Text(Title)
                            .FontColor("#B68838")
                            .Bold()
                            .FontSize(16);

                        // Monto debajo del título
                        inner.Item().Text($"${Monto:F2}")
                            .Bold()
                            .FontSize(32)
                            .FontColor(Colors.White);
                    });
            });

        }
    }
}
