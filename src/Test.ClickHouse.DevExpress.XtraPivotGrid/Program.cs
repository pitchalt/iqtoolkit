using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using ClickHouse.Ado;
using Test;

namespace PivotForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var dbxpo = new NorthwindXpo();
            var dbiqt = new NorthwindIQT();
            var dbsb = new StarBench();

            foreach (var ord in dbiqt.OrderDetail.GroupBy(x => "g0").Select(x => new {P0 = x.Sum(elem => elem.UnitPrice)})) {
                System.Console.WriteLine(ord.P0);                
            }
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new FormNorthwindIQT(dbiqt));
            Application.Run(new FormStarBench(dbsb));

        }
    }
}
