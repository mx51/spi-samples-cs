using SPIClient;
using System;
using System.Windows.Forms;

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
            Application.Run(new MainForm());
        }
    }

    /// <inheritdoc />
    /// <summary>
    /// Global SpiClient class
    /// </summary>
    public class RamenForm : Form
    {
        internal static Spi SpiClient { get; set; }
        internal static string PosId { get; set; }
        internal static string EftposAddress { get; set; }
        internal static string TenantCode { get; set; } = "";
        internal static string TenantName { get; set; } = "";
        internal static Secrets Secrets { get; set; }
        internal static string SerialNumber { get; set; }
        internal static bool AutoAddressEnabled { get; set; }
        internal static TransactionOptions Options { get; set; }
        internal static MainForm MainForm { get; set; }
        internal static TransactionForm TransactionForm { get; set; }
        internal static ActionsForm ActionsForm { get; set; }
        internal static PaymentProviderForm PaymentProviderForm { get; set; }
    }
}
