using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace WinFormHub
{
    public partial class Form1 : Form
    {
        public HubConnection Connection { get; set; }
        private const string UrlLocal = "https://localhost:44323";
        public Form1()
        {
            InitializeComponent();
            Connection = new HubConnectionBuilder().WithUrl($"{UrlLocal}/mainhub", options => 
            {
                options.Headers.Add("API-KEY", "A3Y3B5P0O0S6KEYDessertpos.com");
            }).Build();
            if (Connection.State != HubConnectionState.Connected)
            {
                StartHub();
            }
        }

        private async void StartHub()
        {
            try
            {
                await Connection.StartAsync();
                if (Connection.State == HubConnectionState.Connected)
                {
                    Debug.WriteLine("::::::::::::::::::: CONNECTED :::::::::::::::::::::::::::");
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string path = @"C:\Users\Carlos\Documents\JSONFiles\testOrderKitchen.json";
                JObject o1 = JObject.Parse(File.ReadAllText(path));
                string json = o1.ToString(Formatting.None);
                await Connection.InvokeAsync("SendOrderToKitchen",json,"0000000003");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}