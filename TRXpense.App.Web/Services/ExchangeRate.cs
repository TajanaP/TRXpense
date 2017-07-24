using System;
using System.IO;
using System.Net;
using System.Xml.Serialization;

namespace TRXpense.App.Web.Services
{
    public class ExchangeRate
    {
        [Serializable()]
        [XmlRoot("TECAJNA_LISTA")]
        public class TECAJNA_LISTA
        {
            [XmlElement("item")]
            public item[] items { get; set; }
        };

        public class item
        {
            public string broj_tecajnice { get; set; }
            public string datum { get; set; }
            public string drzava { get; set; }
            public string sifra_valute { get; set; }
            public string valuta { get; set; }
            public string jedinica { get; set; }
            public string kupovni_tecaj { get; set; }
            public string srednji_tecaj { get; set; }
            public string prodajni_tecaj { get; set; }
        }

        public int size = 0;

        private TECAJNA_LISTA exchangeRate = new TECAJNA_LISTA();

        public ExchangeRate(string date)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TECAJNA_LISTA));
            exchangeRate = (TECAJNA_LISTA)serializer.Deserialize(new StringReader(getXml(date)));
            size = exchangeRate.items.Length;

            if (size == 0)
                throw new Exception("Currency not found");
        }

        public item getExchangeRate(string currency)
        {
            foreach (item i in exchangeRate.items)
                if (i.valuta.Equals(currency))
                    return i;

            throw new Exception("Currency not found");
        }

        private string getXml(string date)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.hnb.hr/tecajn?datum=" + date + "&format=xml");

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}