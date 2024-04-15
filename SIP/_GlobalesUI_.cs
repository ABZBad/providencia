using System.Windows.Forms;


/// <summary>
    /// Esta clase tiene como propósito almacenar todas aquellas variables que deban ser globales
    /// y que siempre deban estar disponibles
    /// </summary>
    public class GlobalesUI
    {
        public static Form MainForm { set; get; }
        public static string nombreEquipo = System.Environment.MachineName;
        public static AutoCompleteStringCollection autoCompleteLogosCollection = new AutoCompleteStringCollection();
    }

