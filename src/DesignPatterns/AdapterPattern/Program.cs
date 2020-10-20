using CrystalDecisions.CrystalReports;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace AdapterPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Adapter Pattern!");

            MotorolaRadioTest();

            HyteriaRadioTest();

            // CrystalReportTest();
        }

        private static void CrystalReportTest()
        {
            ReportDocument rpt = new ReportDocument();
            rpt.Load("report1.rpt");

            ConnectionInfo connectInfo = new ConnectionInfo()
            {
                ServerName = "MyServer",
                DatabaseName = "MyDb",
                UserID = "myuser",
                Password = "mypassword"
            };

            foreach (Table table in rpt.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            rpt.ExportToDisk(ReportDocument.ExportFormatType.PortableDocFormat, "report1.pdf");
        }

        private static void MotorolaRadioTest()
        {
            MotorolaRadio radio = new MotorolaRadio();
            radio.PowerOn("1234");
            radio.SelectChannel(10);
            radio.Send("Hello World!");
            radio.PowerOff();
        }

        private static void HyteriaRadioTest()
        {
            HyteraRadio radio = new HyteraRadio();
            radio.Init();
            radio.SendMessage(10, "Hello World!");
            radio.Release();
        }

        private static void HyteriaRadioAdapterTest()
        {
            IRadio radio = new HyteraRadioAdapter();
            radio.Send("Hello World!", 10);
        }

        private static void MotorolaRadioAdapterTest()
        {
            IRadio radio = new MotorolaRadioAdapter("1234");
            radio.Send("Hello World!", 10);
        }

        private static void MotorolaRadioAdapterTest2()
        {
            var radio = new MotorolaRadioAdapter2("1234");
            radio.Send("Hello World!", 10);
        }

        private static void RadioFactoryTest()
        {
            IRadioFactory radioFactory = new RadioFactory();

            IRadio radio = radioFactory.Create("M");

            radio.Send("Hello World!", 10);
        }
    }
    

    public interface IRadioFactory
    {
        IRadio Create(string model);
    }


    public class RadioFactory : IRadioFactory
    {
        public IRadio Create(string json)
        {
            switch(json[0])
            {
                case 'M': return new MotorolaRadioAdapter("1234");
                case 'H': return new HyteraRadioAdapter();

                default: throw new NotSupportedException(json);
            }
        }
    }

    public class RadioFactory2
    {
        public IRadio Create(string json, int deviceId) => (json[0], deviceId) switch
        {
            ('M', 1) => new MotorolaRadioAdapter("1234"),
            ('M', 2) => new MotorolaRadioAdapter("7777"),
            ('H', _) => new HyteraRadioAdapter(),
            _ => throw new NotSupportedException(json),
        };
    }




}
