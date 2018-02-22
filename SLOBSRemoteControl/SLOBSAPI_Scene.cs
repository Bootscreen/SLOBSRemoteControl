using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLOBSRemoteControl
{
    class SLOBSAPI_Scene
    {
        public class Position
        {
            public float x { get; set; }
            public float y { get; set; }
        }

        public class Scale
        {
            public float x { get; set; }
            public float y { get; set; }
        }

        public class Crop
        {
            public float top { get; set; }
            public float bottom { get; set; }
            public float left { get; set; }
            public float right { get; set; }
        }

        public class Transform
        {
            public Position position { get; set; }
            public Scale scale { get; set; }
            public Crop crop { get; set; }
            public float rotation { get; set; }
        }

        public class Item
        {
            public string sceneItemId { get; set; }
            public string sourceId { get; set; }
            public int obsSceneItemId { get; set; }
            public Transform transform { get; set; }
            public bool visible { get; set; }
            public bool locked { get; set; }
        }

        public class Result
        {
            public string _type { get; set; }
            public string resourceId { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public List<Item> items { get; set; }
        }

        public class RootObject
        {
            public string jsonrpc { get; set; }
            public int id { get; set; }
            public Result result { get; set; }
        }
    }
}
