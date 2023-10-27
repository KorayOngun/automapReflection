using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace automapReflection
{
    public static  class Mapper
    {
        public static T Map<T>(object obj)
        {
            Type type = typeof(T);
            var item = Activator.CreateInstance(type);
            foreach (var property in obj.GetType().GetProperties())
            {
                item.GetType().GetProperty(property.Name)?.SetValue(item, property.GetValue(obj));
            }
            return (T)item;
        }

        public static T Mapp<T>(IEnumerable obj)
        {
            // obj == new List<User>(){...}
            // ---> T == IEnumerable<UserResponse> 

            Type listType = typeof(T).GetGenericArguments().First();
            //Type listType = typeof(UserResponse);

            Type listConstructor = typeof(List<>).MakeGenericType(listType);
            //Type listConstructor = typeof(List<UserResponse>);

            IList items = (IList)Activator.CreateInstance(listConstructor);
            // IList items = new List<UserResponse>(); 

            MethodInfo addedMethod = listConstructor.GetMethod("Add");
            //MethodInfo addedMethod = typeof(List<UserResponse>).GetMethod("Add");

            foreach (var o in obj)
            {
                var item = Activator.CreateInstance(listType);
                //var item = new UserResponse();

                foreach (var prop in o.GetType().GetProperties())
                {
                    var value = o.GetType().GetProperty(prop.Name).GetValue(o);
                    //var value = o.Name, o.LastName

                    listType.GetProperty(prop.Name)?.SetValue(item, value);
                    //item.name = value, item.LastName = value
                }

                addedMethod?.Invoke(items, new object[] { item });
                // items.Add(item);
            }

            return (T)items;
        }


        public static T Map<T>(IEnumerable obj)
        {
            // obj == new List<User>(){...}
            // ---> T == IEnumerable<UserResponse> 

            Type listType = typeof(T).GetGenericArguments().First();
            //Type listType = typeof(UserResponse);

            Type listConstructor = typeof(List<>).MakeGenericType(listType);
            //Type listConstructor = typeof(List<UserResponse>);

            IList items = (IList)Activator.CreateInstance(listConstructor);
            // IList items = new List<UserResponse>(); 

            MethodInfo addedMethod = listConstructor.GetMethod("Add");
            //MethodInfo addedMethod = typeof(List<UserResponse>).GetMethod("Add");

            var mapper = typeof(Mapper);

            MethodInfo mapperMap = mapper.GetMethod("Map", new[] {listType}).MakeGenericMethod(listType);

            foreach (var o in obj)
            {
                var item = mapperMap.Invoke(null, new[] { o });
                //var item = new UserResponse();

                addedMethod?.Invoke(items, new object[] { item });
                // items.Add(item);
            }

            return (T)items;
        }




    }
}
