using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SLOBSRemoteControl.SLOBSAPI_Active_Scene;
using static SLOBSRemoteControl.SLOBSAPI_Audio;
using static SLOBSRemoteControl.SLOBSAPI_Scene;
using static SLOBSRemoteControl.SLOBSAPI_SceneItems;

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
                string audio = "";
                
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
                            item = arg.Substring(2);
                            break;
                        case "-I ":
                            item = arg.Substring(3);
                            break;
                        case "-A":
                            audio = arg.Substring(2);
                            break;
                        case "-A ":
                            audio = arg.Substring(3);
                            break;
                    }
                }

                //Client
                var client = new NamedPipeClientStream("slobs");
                client.Connect();
                StreamReader reader = new StreamReader(client);
                StreamWriter writer = new StreamWriter(client);

                string payload = "";
                string response = "";
                Scene json_response;

                switch (mode)
                {
                    case "help":
                        Form2 form = new Form2();
                        form.ShowDialog();
                        break;
                    case "gui":
                        Application.Run(new Form1());
                        break;
                    case "switch_scene":
                        payload = "{ \"jsonrpc\": \"2.0\",\"id\": 1,\"method\": \"getSceneByName\",\"params\": {\"resource\": \"ScenesService\",\"args\": [\"" + scene + "\"]}}";

                        writer.WriteLine(payload);
                        writer.Flush();
                        response = reader.ReadLine();
                        json_response = Newtonsoft.Json.JsonConvert.DeserializeObject<Scene>(response);

                        //Debug.WriteLine(response);
                        if (json_response.result != null)
                        {
                            string payload2 = "{ \"jsonrpc\": \"2.0\",\"id\": 1,\"method\": \"makeSceneActive\",\"params\": {\"resource\": \"ScenesService\",\"args\": [\"" + json_response.result.id + "\"]}}";
                            writer.WriteLine(payload2);
                            writer.Flush();
                            response = reader.ReadLine();
                        }
                        break;
                    case "toggle_source":
                        string scene_id = "";
                        if (scene.Length > 0)
                        {
                            payload = "{ \"jsonrpc\": \"2.0\",\"id\": 1,\"method\": \"getSceneByName\",\"params\": {\"resource\": \"ScenesService\",\"args\": [\"" + scene + "\"]}}";

                            writer.WriteLine(payload);
                            writer.Flush();
                            response = reader.ReadLine();
                            json_response = Newtonsoft.Json.JsonConvert.DeserializeObject<Scene>(response);

                            scene_id = json_response.result.id;
                        }
                        else
                        {
                            payload = "{ \"jsonrpc\": \"2.0\",\"id\": 1,\"method\": \"activeScene\",\"params\": {\"resource\": \"ScenesService\"}}";

                            writer.WriteLine(payload);
                            writer.Flush();
                            response = reader.ReadLine();
                            json_response = Newtonsoft.Json.JsonConvert.DeserializeObject<Scene>(response);
                            scene_id = json_response.result.id;
                        }
                        
                        //Debug.WriteLine(response);
                        if (scene_id.Length > 0)
                        {
                            string payload2 = "{ \"jsonrpc\": \"2.0\",\"id\": 1,\"method\": \"getItems\",\"params\": {\"resource\": \"Scene[\\\"" + scene_id + "\\\"]\"}}";
                            writer.WriteLine(payload2);
                            writer.Flush();
                            response = reader.ReadLine();
                            SceneItems_Root scene_items = Newtonsoft.Json.JsonConvert.DeserializeObject<SceneItems_Root>(response);

                            SceneItem scene_item =  scene_items.result.Where(i => i.name == item).FirstOrDefault();

                            if (scene_item != null)
                            {
                                string resource_id = scene_item.resourceId.Replace("\"", "\\\"");
                                payload2 = "{ \"jsonrpc\": \"2.0\",\"id\": 1,\"method\": \"setVisibility\",\"params\": {\"resource\": \"" + resource_id + "\",\"args\": [" + (!scene_item.visible).ToString().ToLower() + "]}}";
                                writer.WriteLine(payload2);
                                writer.Flush();
                                response = reader.ReadLine();
                            }
                        }
                        break;
                    case "toggle_audio":
                        payload = "{ \"jsonrpc\": \"2.0\",\"id\": 1,\"method\": \"getSourcesForCurrentScene\",\"params\": {\"resource\": \"AudioService\"}}";

                        writer.WriteLine(payload);
                        writer.Flush();
                        response = reader.ReadLine();
                        Audio_Sources audiosources = Newtonsoft.Json.JsonConvert.DeserializeObject<Audio_Sources>(response);

                        Audio_Source audiosource = audiosources.result.Where(i => i.name == audio).FirstOrDefault();
                        
                        if (audiosource != null)
                        {
                            string resource_id = audiosource.resourceId.Replace("\"", "\\\"");
                            string payload2 = "{ \"jsonrpc\": \"2.0\",\"id\": 1,\"method\": \"setMuted\",\"params\": {\"resource\": \"" + resource_id + "\",\"args\": [" + (!audiosource.muted).ToString().ToLower() + "]}}"; ;
                            writer.WriteLine(payload2);
                            writer.Flush();
                            response = reader.ReadLine();
                        }
                        break;
                }

                client.Dispose();
                client.Close();
            }
        }
    }
}
