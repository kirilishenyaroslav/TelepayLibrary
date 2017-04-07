using System;
using System.IO;
using TelepayLib.Base;
using TelepayLib.Betfor;

namespace TelepayLib
{
    public class Telepay
    {
        public string ParseDocument(string path)
        {
            
            //"../../../sample2013.TPY";
            //Telepay foreign.txt
            string fileName = Path.GetFileName(path);

            var file = File.Open(path, FileMode.Open);
            StreamReader rdr = new StreamReader(file);
            var origContent = rdr.ReadToEnd();

            var batch = new BatchBase();
            TelepayParser.ParseObject(origContent, batch);
            var batchDoc = batch.ToString();

            file.Close();
            return batchDoc;
        }

        public InternationalBatch createInternationalBatch()
        {
            var b = new InternationalBatch();

            b.Init(1, 1, 996132986, DateTime.Now);

            b.AddPayment(18223848113,                           
                         DateTime.Now,
                         "C12 2015 FROM 01 JANUARY 2015",
                         TextCode.Salary, 
                         TransferCode.L, 
                         220000,
                         "26002050015712",
                         "KIRILISHEN IAROSLAV VIKTOROVICH", 
                         "PBANUA2X", 
                         "Lenina str.14 Dnepropetrovsk,49000,Ukraine",
                         "PBANUA2X",
                         "PJSC CB 'PRIVATBANK'", 
                         "UA",
                         "DNEPROPETROVSK, 49000, UKRAINE",
                         "Invoice №1234 from 01.01.2015",
                         ISOCurrencySymbols.USD,
                         Charges.BEN,
                         Charges.OUR);

            b.AddPayment(18223848113,
                         DateTime.Now,
                         "C10 2015 FROM 01 JANUARY 2015",
                         TextCode.Salary,
                         TransferCode.L,
                         270000,
                         "26002050015718",
                         "KUZNETSOV VLAD",
                         "PBANUA2X",
                         "Lenina str.14 Dnepropetrovsk,49000,Ukraine",
                         "PBANUA2X",
                         "PJSC CB 'PRIVATBANK'",
                         "UA",
                         "DNEPROPETROVSK, 49000, UKRAINE",
                         "Invoice №1234 from 01.01.2015",
                         ISOCurrencySymbols.USD,
                         Charges.BEN,
                         Charges.OUR);

            return b;
        }

        public DomesticBatch createDomesticBatch()
        {
            var b = new DomesticBatch();

            b.Init(1, 1, 996132986, new DateTime(2016, 2, 10));
            
            b.AddPayment(18223848113, 
                         new DateTime(2016, 2, 10), 
                         "VEDR.FAKTURA NR. 54421", 
                         TextCode.TransferWithAdvice, 
                         TransferCode.F, 
                         107288,
                         60110577578, 
                         "SPORTY BUDSERVICE AS");
            
            return b;
        }
    }
}