using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
namespace Berum_WebAPI.PDF.Componentes
{
    public class FooterContactoComponent : IComponent
    {
        public string Telefono { get; }
        public string Instagram { get; }
        public string Web { get; }
        public string Ubicacion { get; }

        public FooterContactoComponent(string telefono, string instagram, string web, string ubicacion)
        {
            Telefono = telefono;
            Instagram = instagram;
            Web = web;
            Ubicacion = ubicacion;
        }

        public void Compose(IContainer container)
        {
            container
                .Background("#101B2F")
                .CornerRadius(10)
                .PaddingVertical(10)
                .PaddingHorizontal(15)
                .Row(row =>
                {
                    row.RelativeItem().AlignMiddle().Element(BuildGarantia);

                    row.ConstantItem(1).Background("#304060");

                    row.RelativeItem().AlignMiddle().Element(BuildContacto);

                    row.ConstantItem(1).Background("#304060");

                    row.RelativeItem().AlignMiddle().Element(BuildUbicacion);
                });
        }

        private void BuildGarantia(IContainer container)
        {
            container.PaddingRight(15).Row(row =>
            {
                row.ConstantItem(30)
                    .AlignMiddle()
                    .Image(PdfAssets.Aprobado)
                    .FitArea();

                row.RelativeItem().AlignMiddle()
                    .PaddingLeft(10)
                    .Column(column =>
                    {
                        column.Item().Text("GARANTÍA DE SERVICIO")
                            .FontSize(9.5f)
                            .Bold()
                            .FontColor("#D9A33F")
                            .LetterSpacing(0.04f);

                        column.Item().PaddingTop(3).Text("Trabajamos con repuestos de calidad y garantía en cada reparación.")
                            .FontSize(7)
                            .FontColor("#B7BFCC")
                            .LineHeight(1.3f);
                    });
            });
        }

        private void BuildContacto(IContainer container)
        {
            container.PaddingHorizontal(15).Column(column =>
            {
                column.Item().Row(row =>
                {
                    row.ConstantItem(18).AlignMiddle().Image(PdfAssets.Telefono).FitArea();
                    row.RelativeItem().AlignMiddle().PaddingLeft(8)
                        .Text("+54 9 11 2320 3212")
                        .FontSize(9.5f)
                        .FontColor(Colors.White);
                });

                column.Item().PaddingTop(8).Row(row =>
                {
                    row.ConstantItem(18).AlignMiddle().Image(PdfAssets.Instagram).FitArea();
                    row.RelativeItem().AlignMiddle().PaddingLeft(8)
                        .Text("@RepairShop")
                        .FontSize(9.5f)
                        .FontColor(Colors.White);
                });
            });
        }

        private void BuildUbicacion(IContainer container)
        {
            container.PaddingLeft(15).Column(column =>
            {
                column.Item().Row(row =>
                {
                    row.ConstantItem(18).AlignMiddle().Image(PdfAssets.Ubicacion).FitArea();
                    row.RelativeItem().AlignMiddle().PaddingLeft(8)
                        .Text("Av. Corrientes 1234, CABA")
                        .FontSize(9.5f)
                        .FontColor(Colors.White);
                });

                column.Item().PaddingTop(8).Row(row =>
                {
                    row.ConstantItem(18).AlignMiddle().Image(PdfAssets.Mundo).FitArea();
                    row.RelativeItem().AlignMiddle().PaddingLeft(8)
                        .Text("www.RepairShop.com.ar")
                        .FontSize(9.5f)
                        .FontColor(Colors.White);
                });
            });
        }
    }
}
