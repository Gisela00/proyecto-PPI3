namespace Berum_WebAPI.PDF
{
    public static class PdfAssets
    {
        private static readonly string BasePath =
            Path.Combine(AppContext.BaseDirectory, "PDF", "Assets");
        // --- UI / Decorativos ---
        public static string AccesoriosEntregados => Path.Combine(BasePath, "AccesoriosEntregados.png");
        public static string Calendario => Path.Combine(BasePath, "Calendario.png");
        public static string CalendarioIngreso => Path.Combine(BasePath, "CalendarioIngreso.png");
        public static string Cinta => Path.Combine(BasePath, "Cinta.svg");
        public static string Clipboard => Path.Combine(BasePath, "Clipboard.png");
        public static string Clipboard2 => Path.Combine(BasePath, "Clipboard2.png");
        public static string Documento => Path.Combine(BasePath, "Documento.png");
        public static string Importante => Path.Combine(BasePath, "Importante.png");
        public static string Observado => Path.Combine(BasePath, "Observado.png");
        public static string ProximosPasos => Path.Combine(BasePath, "ProximosPasos.png");
        public static string ProblemaInformado => Path.Combine(BasePath, "ProblemaInformado.png");
        public static string Reparacion => Path.Combine(BasePath, "Reparacion.png");
        public static string ReparacionIsometrico => Path.Combine(BasePath, "ReparacionIsometrico.png");
        public static string Reloj => Path.Combine(BasePath, "Reloj.png");
        public static string DocumentoIsometrico => Path.Combine(BasePath, "DocumentoIsometrico.png");

        // --- Dispositivos ---
        public static string Celular => Path.Combine(BasePath, "Celular.png");
        public static string Computadora => Path.Combine(BasePath, "Computadora.png");
        public static string Impresora => Path.Combine(BasePath, "Impresora.png");
        public static string Laptop => Path.Combine(BasePath, "Laptop.png");
        public static string Monitor => Path.Combine(BasePath, "Monitor.png");
        public static string PC => Path.Combine(BasePath, "PC.png");
        public static string LaptopIsometrico => Path.Combine(BasePath, "LaptopIsometrico.png");
        public static string ImpresoraIsometrico => Path.Combine(BasePath, "ImpresoraIsometrico.png");
        public static string CelularIsometrico => Path.Combine(BasePath, "CelularIsometrico.png");
        public static string PcIsometrico => Path.Combine(BasePath, "PcIsometrico.png");
        public static string MonitorIsometrico => Path.Combine(BasePath, "MonitorIsometrico.png");
        // --- Usuarios / identidad ---
        public static string Instagram => Path.Combine(BasePath, "Instagram.png");
        public static string User => Path.Combine(BasePath, "User.png");
        public static string User2 => Path.Combine(BasePath, "User2.png");
        public static string User3 => Path.Combine(BasePath, "User3.png");

        // --- Estado / alertas ---
        public static string Alerta => Path.Combine(BasePath, "Alerta.png");
        public static string Aprobado => Path.Combine(BasePath, "Aprobado.png");
        public static string Falla => Path.Combine(BasePath, "Falla.png");

        // --- Contacto ---
        public static string Email => Path.Combine(BasePath, "Email.png");
        public static string Telefono => Path.Combine(BasePath, "Telefono.png");
        public static string TelefonoBlack => Path.Combine(BasePath, "TelefonoBlack.png");

        // --- Contexto / trabajo ---
        public static string Mundo => Path.Combine(BasePath, "Mundo.png");
        public static string Trabajo => Path.Combine(BasePath, "Trabajo.png");
        public static string Ubicacion => Path.Combine(BasePath, "Ubicacion.png");
        public static string ConsultarEstadoIsometrico => Path.Combine(BasePath, "ConsultarEstadoIsometrico.png");
        public static string ObservadoIsometrico => Path.Combine(BasePath, "ObservadoIsometrico.png");

    }
}
