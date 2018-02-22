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

        public class Scene
        {
            public string jsonrpc { get; set; }
            public int id { get; set; }
            public Result result { get; set; }
        }
    }

    class SLOBSAPI_Active_Scene
    {
        public class Active_Scene_ID
        {
            public string jsonrpc { get; set; }
            public int id { get; set; }
            public string result { get; set; }
        }
    }

    class SLOBSAPI_SceneItems
    {
        public class Position
        {
            public double x { get; set; }
            public double y { get; set; }
        }

        public class Scale
        {
            public double x { get; set; }
            public double y { get; set; }
        }

        public class Crop
        {
            public int top { get; set; }
            public int bottom { get; set; }
            public int left { get; set; }
            public int right { get; set; }
        }

        public class Transform
        {
            public Position position { get; set; }
            public Scale scale { get; set; }
            public Crop crop { get; set; }
            public int rotation { get; set; }
        }

        public class SceneItem
        {
            public string _type { get; set; }
            public string resourceId { get; set; }
            public string sceneItemId { get; set; }
            public string sourceId { get; set; }
            public int obsSceneItemId { get; set; }
            public Transform transform { get; set; }
            public bool visible { get; set; }
            public bool locked { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public bool audio { get; set; }
            public bool video { get; set; }
            public bool doNotDuplicate { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public bool muted { get; set; }
            public string id { get; set; }
        }

        public class SceneItems_Root
        {
            public string jsonrpc { get; set; }
            public int id { get; set; }
            public List<SceneItem> result { get; set; }
        }
    }

    class SLOBSAPI_Audio
    {
        public class Fader
        {
            public double db { get; set; }
            public double deflection { get; set; }
            public double mul { get; set; }
        }

        public class Audio_Source
        {
            public string _type { get; set; }
            public string resourceId { get; set; }
            public string sourceId { get; set; }
            public Fader fader { get; set; }
            public int audioMixers { get; set; }
            public int monitoringType { get; set; }
            public bool forceMono { get; set; }
            public int syncOffset { get; set; }
            public bool muted { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public bool audio { get; set; }
            public bool video { get; set; }
            public bool doNotDuplicate { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string id { get; set; }
        }

        public class Audio_Sources
        {
            public string jsonrpc { get; set; }
            public int id { get; set; }
            public List<Audio_Source> result { get; set; }
        }
    }
}
