using CacheManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memecache
{
    class Program
    {

        static List<string> list = new List<string>() { "123", "456", "789" };

        static void Main(string[] args)
        {
            //var manager = CacheFactory.FromConfiguration<object>("myCache");

            //manager.ClearRegion("list");
            //manager.Remove("list");
            //manager.
            // var val = manager.Get("list") as List<string>;
            //manager.Add("list", list);

            //var val = manager.Get("list") as List<string>;
            //if (val == null)
            //{
            //    val = list;
            //    manager.Add("list", val);
            //}

            //foreach (var s in val)
            //{
            //    Console.WriteLine(s);
            //}

            //if (!list.Contains("20000"))
            //{
            //    list.Add("20000");
            //}

            //manager.Update("list", v => list);

            //Console.WriteLine("---------------邪恶分割线------------------");

            //var _list = manager.Get("list") as List<string>;

            //foreach (var ss in _list)
            //{
            //    Console.WriteLine(ss);
            //}

            //manager.Remove("list");
            //manager.ClearRegion("list");




            /*************
             * 
             * 
             * 多缓存测试，缓存同步测试
             * 
             * 
             * ************/

            var manager = CacheFactory.Build("StartCahce", setting =>
            {
                setting.WithSystemRuntimeCacheHandle("handleName")
                .And
                .WithMemcachedCacheHandle("memcachedHandle")
                .WithExpiration(ExpirationMode.Sliding, TimeSpan.FromHours(1));
            });


            //var val = manager.Get("list") as List<string>;

            //if (val == null)
            //{
                //val = list;
                manager.Add("list", list);
            //}

            foreach (var s in list)
            {
                Console.WriteLine(s);
            }

            list.Add("20000");

            manager.Update("list", v => list);
            Console.WriteLine("---------------邪恶分割线------------------");

            var _list = manager.Get("list") as List<string>;

            foreach (var ss in _list)
            {
                Console.WriteLine(ss);
            }


             


            Console.ReadKey();
        }
    }
}
