using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SLOBSRemoteControl.SLOBSAPI_Scene;

namespace SLOBSRemoteControl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.named_pipe = textBox1.Text;
            Properties.Settings.Default.hide_on_start = checkBox1.Checked;
            Properties.Settings.Default.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.named_pipe;
            checkBox1.Checked = Properties.Settings.Default.hide_on_start;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string payload = "{ \"jsonrpc\": \"2.0\",\"id\": 1,\"method\": \"getSceneByName\",\"params\": {\"resource\": \"ScenesService\",\"args\": [\"" + textBox2.Text + "\"]}}";

            //Client 
            var client = new NamedPipeClientStream("slobs");
            client.Connect();
            StreamReader reader = new StreamReader(client);
            StreamWriter writer = new StreamWriter(client);

            writer.WriteLine(payload);
            writer.Flush();
            string response = reader.ReadLine();
            RootObject json_response = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(response);

            Debug.WriteLine(response);
            if (json_response.result != null)
            {
                string payload2 = "{ \"jsonrpc\": \"2.0\",\"id\": 1,\"method\": \"makeSceneActive\",\"params\": {\"resource\": \"ScenesService\",\"args\": [\"" + json_response.result.id + "\"]}}";
                writer.WriteLine(payload2);
                writer.Flush();
                response = reader.ReadLine();
            }
            Debug.WriteLine(response);
            Debug.WriteLine("");
        }
    }
}
