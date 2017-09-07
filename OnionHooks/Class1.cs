using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;

namespace OnionHooks
{
    public class Config
    {
        public int Width                { get; set; }
        public int Height               { get; set; }
        public bool Debug               { get; set; }
        public bool FreeCamera          { get; set; }
        public float MaxCameraDistance  { get; set; }
        public bool LogSeed             { get; set; }
        public int WorldSeed            { get; set; }
        public int LayoutSeed           { get; set; }
        public int TerrainSeed          { get; set; }
        public int NoiseSeed            { get; set; }
    }

    public struct Dim
    {
        public int width;
        public int height;
    }

    public class Hooks
    {
        public static Config conf = null;

        public static void LoadConfig()
        {
            if (conf == null)
            {
                string data = System.IO.File.ReadAllText("onion.json");
                conf = JsonConvert.DeserializeObject<Config>(data);
            }
        }

        /*
        public static void OnInitRandom(ref int seed)
        {
            LoadConfig();
            if (conf.Seed >= 0)
            {
                seed = conf.Seed;
            }

            if (conf.LogSeed)
            {
                using (var sw = System.IO.File.AppendText("onionlog.txt"))
                {
                    sw.WriteLine(string.Format("[{0}] Size: {1}x{2} Seed: {3}", 
                        System.DateTime.Now.ToShortDateString(), 
                        conf.Width,
                        conf.Height,
                        seed));
                }
            }
        }
        */

        public static void OnInitRandom(ref int worldSeed, ref int layoutSeed, ref int terrainSeed, ref int noiseSeed)
        {
            worldSeed   = (conf.WorldSeed >= 0)     ?   conf.WorldSeed      : worldSeed;
            layoutSeed  = (conf.LayoutSeed >= 0)    ?   conf.LayoutSeed     : layoutSeed;
            terrainSeed = (conf.TerrainSeed >= 0)   ?   conf.TerrainSeed    : terrainSeed;
            noiseSeed   = (conf.NoiseSeed >= 0)     ?   conf.NoiseSeed      : noiseSeed;

            if (conf.LogSeed)
            {
                using (var sw = System.IO.File.AppendText("onionlog.txt"))
                {
                    sw.WriteLine(string.Format("[{0}] Size: {1}x{2} World Seed: {3} Layout Seed: {4} Terrain Seed: {5} Noise Seed: {6} ",
                        System.DateTime.Now.ToShortDateString(),
                        conf.Width,
                        conf.Height,
                        worldSeed,
                        layoutSeed,
                        terrainSeed,
                        noiseSeed));
                }
            }
        }

        /*
        public static void ILOnInitRandomCall(int worldSeed, int layoutSeed, int terrainSeed, int noiseSeed)
        {
            OnInitRandom(ref worldSeed, ref layoutSeed, ref terrainSeed, ref noiseSeed);
        }
        */

        public static void OnDoOfflineWorldGen(ref int width, ref int heigth)
        {
            /* Get Types */
            //Type ParentType = typeof(OfflineWorldGen);
            //Type KeyType    = ParentType.GetNestedType("ValidDimensions", BindingFlags.NonPublic | BindingFlags.Instance);
            LoadConfig();
            width   = conf.Width;
            heigth  = conf.Height;
        }

        public static bool GetDebugEnabled()
        {
            LoadConfig();
            return conf.Debug;
        }

        public static bool GetFreeCamera()
        {
            LoadConfig();
            return conf.FreeCamera;
        }

        public static float GetMaxCameraShow()
        {
            LoadConfig();
            return conf.MaxCameraDistance;
        }

        public static void WriteSeedToFile(int seed)
        {

        }

        /*
        public static void Other()
        {
            int seed = 8;
            OnInitRandom(ref seed);
            Console.WriteLine(seed);
        }

        public static void SomeOther()
        {
            Dim d;
            d.width = 234;
            d.height = 444;

            int w = d.width;
            int h = d.height;
            OnDoOfflineWorldGen(ref w, ref h);
        }

        
        public static void Foo()
        {
            bool enabled = false;
            bool camera  = false;
            enabled = GetDebugEnabled();
            camera  = GetFreeCamera();
            Console.WriteLine(enabled);
            Console.WriteLine(camera);
        }

        public static void Init(Bar b)
        {
            b.enabled = false;
        }
        */
    }
    /*
    public class Bar
    {
        public static bool Camera;
        public bool enabled;
        Bar()
        {
            this.enabled = System.IO.File.Exists(System.IO.Path.Combine("C:", "debug_enable.txt"));
            this.enabled = OnionHooks.Hooks.GetDebugEnabled();
            Bar.Camera = OnionHooks.Hooks.GetFreeCamera();
            OnionHooks.Hooks.Init(this);
        }
    }
    */
}
