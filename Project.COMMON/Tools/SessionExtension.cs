using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.COMMON.Tools
{
    public static class SessionExtension
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            string objectString = JsonConvert.SerializeObject(value);
            session.SetString(key, objectString);
        }
        public static T GetObject<T>(this ISession session, string key) where T : class
        {
            string objectString = session.GetString(key);
            if (string.IsNullOrEmpty(objectString)) return null;
            T deserializedObject = JsonConvert.DeserializeObject<T>(objectString);

            return deserializedObject;
        }

        /*
        Yukarıdaki Session methodlarını kullanmak için;
        
            SetObject için => SessionExtension.SetObject(HttpContext.Session,"key",value)
            GetObject için => SessionExtension.GetObject<T>(HttpContext.Session,"Key,)
         
        Farklı bir Session kullanımı için ise;

            HttpContext.Session.SetString("key", value)
            HttpContext.Session.GetString("key")

        Yukarıda ki kullanım şekli sadece string değerler için geçerlidir. Herhangi bir nesne (mesela AppUser) aktarmak istediğimiz de sıkıntı çıkaracaktır.
        Nesne aktarımı için ise;
            
            SetObject ve GetObject<T> kullanılmalıdır.
         
        */

    }
}
