using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Xamarin.Forms;

using XamarinApproov;

namespace XamarinApproovDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        /*
         * OnHello() demonstrates an unprotected API call to the demo server. 
         * 
         * No Approov token is required. 
         * 
         * A successful response is 'Hello Woorld!"
         */

        private const string helloUrl = "https://demo-server.approovr.io/hello";

        private readonly HttpClient helloClient = new HttpClient();

        private async void OnHello(object sender, EventArgs e)
        {
            var response = await helloClient.GetAsync(helloUrl);
            string message;
            string imageSource;
            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                message = content;
                imageSource = "hello.png";
            }
            else
            {
                message = response.ReasonPhrase;
                imageSource = "confused.png";
            }

            statusLabel.Text = message;
            statusImage.Source = imageSource;
        }
 
        /*
         * OnShape() demonstrates an Approov-protected API call to the demo server. 
         * 
         * An Approov interceptor is used to automatically fetch an Approov token
         * and add it to the Approov-Token header for every API call made by the
         * shapeClient. 
         * 
         * A successful response is a random shape name.
         */

        private const string shapeUrl = "https://demo-server.approovr.io/shapes";

        private readonly HttpClient shapeClient = new HttpClient(new ApproovInterceptor(null));
 
        private async void OnShape(object sender, EventArgs e)
        {
            var response = await shapeClient.GetAsync(shapeUrl);
            string message;
            string imageSource;
            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                message = content;
                imageSource = content.ToLower() + ".png";
            }
            else
            {
                message = response.ReasonPhrase;
                imageSource = "confused.png";
            }

            statusLabel.Text = message;
            statusImage.Source = imageSource;
        }

    }
}
