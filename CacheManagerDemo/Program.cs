using CacheManager.Core;
using CacheManager.Memcached;
//using CacheManager.MicrosoftCachingMemory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheManagerDemo
{

    class Program
    {
        static string key = "list";
        static void Main(string[] args)
        {
            var _list = Customer.Instance.GetAll(key);

            foreach (var s in _list)
            {

                Console.WriteLine(s);
            }


            Console.WriteLine("--------------------------------");

            Customer.Instance.insert(key, "1000");

            var _list1 = Customer.Instance.GetAll(key);

            foreach (var s in _list1)
            {

                Console.WriteLine(s);
            }


        }
    }


    public class Customer
    {
        public static Customer m_customer = null;

        private static ICacheManager<object> manager = null;

        //初始化列表
        private static List<string> list = new List<string>() { "123", "456", "789" };

        public static Customer Instance
        {
            get
            {
                if (m_customer == null)
                {
                    m_customer = new Customer();
                }
                if (manager == null)
                {
                    //系统cache
                    //manager = CacheFactory.Build("getStartedCache", settings =>
                    //{
                    //    settings.WithSystemRuntimeCacheHandle("handlename");
                    //});

                    manager = CacheFactory.FromConfiguration<object>("myCache");


                }

                return m_customer;
            }
        }


        public List<string> GetAll(string key)
        {
            var val = manager.Get(key) as List<string>;

            if (val == null)
            {
                val = list;
                manager.Add(key, list);

                Debug.WriteLine("初始化列表");
            }
            else
            {
                Debug.WriteLine("访问缓存获取:{0}", DateTime.Now);
            }

            return val;
        }


        public bool insert(string key, string _cur)
        {
            if (!list.Contains(_cur))
            {
                list.Add(_cur);
            }

            //更新缓存
            manager.Update(key, v => list);
            return true;
        }

        public bool Delete(string key, string _val)
        {
            if (list.Contains(_val))
            {
                list.Remove(_val);
            }

            //update cache
            manager.Update(key, v => list);

            return true;
        }

    }



}
