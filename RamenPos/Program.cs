using System;
using System.Windows.Forms;
using SPIClient;
using SPIClient.Service;

namespace RamenPos
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RamenPos());
        }
    }

    /// <inheritdoc />
    /// <summary>
    /// Global SpiClient class
    /// </summary>
    public class RamenForm : Form
    {
        internal Spi SpiClient { get; set; }
        internal string PosId { get; set; }
        internal string EftposAddress { get; set; }
        internal Secrets Secrets { get; set; }
        internal string SerialNumber { get; set; }
        internal bool AutoAddressEnabled { get; set; }
    }
}
