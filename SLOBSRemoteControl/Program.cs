using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SLOBSRemoteControl.SLOBSAPI_Scene;

namespace SLOBSRemoteControl
{
    static class Program
    {
        class Options
        {
            [Option('m', "mode",
                HelpText = "select what to do, currently only switch_scene and show_gui.")]
            public string mode { get; set; }

            [Option('s', "scene",
                HelpText = "scene name for switch_scene.")]
            public string scene { get; set; }
        }
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!Properties.Settings.Default.hide_on_start)
            {
                Application.Run(new Form1());
            }
            else
            {
                string mode = "";
                string scene = "";
                string item = "";
                foreach (string arg in args)
                {
                    switch (arg.Substring(0, 2).ToUpper())
                    {
                        case "-H":
                            mode = "help";
                            break;
                        case "-G":
                            mode = "gui";
                            break;
                        case "-M":
                            mode = arg.Substring(2);
                            break;
                        case "-M ":
                            mode = arg.Substring(3);
                            break;
                        case "-S":
                            scene = arg.Substring(2);
                            break;
                        case "-S ":
                            scene = arg.Substring(3);
                            break;
                        case "-I":
                            scene = arg.Substring(2);
                            break;
                        case "-I ":
                            scene = arg.Substring(3);
                            break;
                    }
                }

                switch (mode)
                {
                    case "help":
                        //TODO
                        break;
                    case "gui":
                        Application.Run(new Form1());
                        break;
                    case "switch_scene":
                        string payload = "{ \"jsonrpc\": \"2.0\",\"id\": 1,\"method\": \"getSceneByName\",\"params\": {\"resource\": \"ScenesService\",\"args\": [\"" + scene + "\"]}}";

                        //Client
                        var client = new NamedPipeClientStream("slobs");
                        client.Connect();
                        StreamReader reader = new StreamReader(client);
                        StreamWriter writer = new StreamWriter(client);

                        writer.WriteLine(payload);
                        writer.Flush();
                        string response = reader.ReadLine();
                        RootObject json_response = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(response);

                        //Debug.WriteLine(response);
                        if(json_response.result != null)
                        { 
                            string payload2 = "{ \"jsonrpc\": \"2.0\",\"id\": 1,\"method\": \"makeSceneActive\",\"params\": {\"resource\": \"ScenesService\",\"args\": [\"" + json_response.result.id + "\"]}}";
                            writer.WriteLine(payload2);
                            writer.Flush();
                            response = reader.ReadLine();
                        }
                        break;
                }
            }
        }
    }
}
