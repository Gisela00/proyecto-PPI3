using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
namespace Berum_WebAPI.PDF.Componentes
{
    public class ClienteComponent : IComponent
    {
        private readonly string Title;
        private readonly string Nombre;
        private readonly string Telefono;
        private readonly string Email;

        public ClienteComponent(string title, string nombre, string telefono, string email)
        {
            Title = title;
            Nombre = nombre;
            Telefono = telefono;
            Email = email;
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
                            .Image(PdfAssets.User);

                        row.ConstantItem(5);

                        row.RelativeItem()
                            .Text(Title)
                            .FontColor("#0F1A2E")
                            .Bold();
                    });

                // Filas de información
                column.Item().Component(new InfoRowComponent(
                    PdfAssets.User2,
                    "Nombre:",
                    Nombre, true));

                column.Item().Component(new InfoRowComponent(
                    PdfAssets.TelefonoBlack,
                    "Teléfono:",
                    Telefono, true));

                column.Item().Component(new InfoRowComponent(
                    PdfAssets.Email,
                    "Email:",
                    Email, true));
            });
        }
    }
}
