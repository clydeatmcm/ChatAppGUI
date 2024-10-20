namespace ChatAppGUI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // You can choose to start either the server or client
            var serverForm = new ServerForm();
            serverForm.Show();

            var clientForm1 = new ClientForm();
            clientForm1.Show();

            var clientForm2 = new ClientForm();
            clientForm2.Show();

            Application.Run();
        }
    }
}