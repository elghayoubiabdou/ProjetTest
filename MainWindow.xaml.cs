using System;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Net;
using System.Net.Http;
using Microsoft.VisualBasic;

namespace ProjetTest
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        static HttpClient client = new HttpClient();

        public string base_currency_id = "";
        public string quote_currency_id = "";
        public string amount = "";

        // private readonly string _apiBaseUrl = $"https://api.coinpaprika.com/v1/price-converter";


        public MainWindow()
        {
            InitializeComponent();

        }

        public void GetResult(string base_currency_id, string quote_currency_id, string amount)
        {
            string strurltest = String.Format("https://api.coinpaprika.com/v1/price-converter?base_currency_id=" + base_currency_id + "&quote_currency_id=" + quote_currency_id + "&amount=" + amount + "");
            WebRequest requestObjGet = WebRequest.Create(strurltest);
            requestObjGet.Method = "GET";
            HttpWebResponse responseObjGet = null;
            try
            {
                responseObjGet = (HttpWebResponse)requestObjGet.GetResponse();
                WebHeaderCollection header = responseObjGet.Headers;
                if ((int)responseObjGet.StatusCode == 200)
                {
                    string strresulttest = null;
                    using (Stream stream = responseObjGet.GetResponseStream())
                    {
                        StreamReader sr = new StreamReader(stream);
                        strresulttest = sr.ReadToEnd();
                        sr.Close();
                    }
                    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    Model model = (Model)serializer.Deserialize(strresulttest, typeof(Model));
                    L1.Content = amount+" " + Combo_sources.Text +" = "+ model.price.ToString() +" "+ Combo_destinations.Text;
                }
                else
                {


                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    int code_error = 0;

                    code_error = (int)httpResponse.StatusCode;
                    if (code_error == 429)
                    {
                        MessageBox.Show("Code is used when breaking a request rate limit", "Error 429");
                    }
                    else
                    {
                        if (code_error >= 400 && code_error < 500)
                        {
                            MessageBox.Show("Codes are used for invalid requests - the issue is on the sender's side", " Error : " + code_error);
                        } else
                        {
                            if (code_error >= 500)
                            {
                                MessageBox.Show("Codes are used for internal errors - the issue is on coinpaprika's side", " Error : " + code_error);
                            }
                            else
                            {
                                MessageBox.Show("Something Wrong", " Error");
                            }

                        }
                    }
                    //using (Stream data = response.GetResponseStream())
                    //using (var reader = new StreamReader(data))
                    //{
                    //    string text = reader.ReadToEnd();
                    //    Console.WriteLine(text);
                    //}
                }
            }

        }


        public void Get_Id()
        {
            string str = Combo_sources.Text;
            switch (str)
            {

                case "Bitcoin":
                    base_currency_id = "btc-bitcoin";
                    break;

                case "Euro":
                    base_currency_id = "eur-euro-token";
                    break;
                case "Neurochain":
                    base_currency_id = "ncc-neurochain";
                    break;

                default:
                    base_currency_id = "";
                    break;
            }

            string str1 = Combo_destinations.Text;
            switch (str1)
            {

                case "USD":
                    quote_currency_id = "usd-us-dollars";
                    break;

                case "Ethereum":
                    quote_currency_id = "eth-ethereum";
                    break;
                case "XRP":
                    quote_currency_id = "xrp-xrp";
                    break;

                default:
                    quote_currency_id = "";
                    break;
            }
        }

        public async void convert(object sender, RoutedEventArgs e)
        {
            Get_Id();

            var montant = Text_Montant.Text;
            Double n;
            bool isNumeric = Double.TryParse(montant, out n);
            L1.Content = isNumeric;
            if (isNumeric == true)
            {
                GetResult(base_currency_id, quote_currency_id, montant);
            }
            else
            {
                MessageBox.Show("Saisir un montant valide", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Asterisk);

            }


        }

        private void Text_Montant_TouchEnter(object sender, System.Windows.Input.TouchEventArgs e)
        {
            Get_Id();

            var montant = Text_Montant.Text;
            Double n;
            bool isNumeric = Double.TryParse(montant, out n);
            L1.Content = isNumeric;
            if (isNumeric == true)
            {
                GetResult(base_currency_id, quote_currency_id, montant);
            }
            else
            {
                MessageBox.Show("Saisir un montant valide", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Asterisk);

            }
        }
    }
}
