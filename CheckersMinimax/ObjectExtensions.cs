/**
    Source code is released under the MIT license.

    The MIT License (MIT)
    Copyright (c) 2014 Burtsev Alexey

    Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files 
    (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge,
    publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do
    so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
    OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
    LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
    IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

  */

using System.Collections.Generic;
using System.Reflection;
using System.ArrayExtensions;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace System
{
    //public static class ObjectExtensions
    //{
    //    private static readonly MethodInfo CloneMethod = typeof(Object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);

    //    public static bool IsPrimitive(this Type type)
    //    {
    //        if (type == typeof(String)) return true;
    //        return (type.IsValueType & type.IsPrimitive);
    //    }

    //    public static Object Copy(this Object originalObject)
    //    {
    //        return InternalCopy(originalObject, new Dictionary<Object, Object>(new ReferenceEqualityComparer()));
    //    }
    //    private static Object InternalCopy(Object originalObject, IDictionary<Object, Object> visited)
    //    {
    //        if (originalObject == null) return null;
    //        var typeToReflect = originalObject.GetType();
    //        if (IsPrimitive(typeToReflect)) return originalObject;
    //        if (visited.ContainsKey(originalObject)) return visited[originalObject];
    //        if (typeof(Delegate).IsAssignableFrom(typeToReflect)) return null;
    //        var cloneObject = CloneMethod.Invoke(originalObject, null);
    //        if (typeToReflect.IsArray)
    //        {
    //            var arrayType = typeToReflect.GetElementType();
    //            if (IsPrimitive(arrayType) == false)
    //            {
    //                Array clonedArray = (Array)cloneObject;
    //                clonedArray.ForEach((array, indices) => array.SetValue(InternalCopy(clonedArray.GetValue(indices), visited), indices));
    //            }

    //        }
    //        visited.Add(originalObject, cloneObject);
    //        CopyFields(originalObject, visited, cloneObject, typeToReflect);
    //        RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect);
    //        return cloneObject;
    //    }

    //    private static void RecursiveCopyBaseTypePrivateFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect)
    //    {
    //        if (typeToReflect.BaseType != null)
    //        {
    //            RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect.BaseType);
    //            CopyFields(originalObject, visited, cloneObject, typeToReflect.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, info => info.IsPrivate);
    //        }
    //    }

    //    private static void CopyFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool> filter = null)
    //    {
    //        foreach (FieldInfo fieldInfo in typeToReflect.GetFields(bindingFlags))
    //        {
    //            if (filter != null && filter(fieldInfo) == false) continue;
    //            if (IsPrimitive(fieldInfo.FieldType)) continue;
    //            var originalFieldValue = fieldInfo.GetValue(originalObject);
    //            var clonedFieldValue = InternalCopy(originalFieldValue, visited);
    //            fieldInfo.SetValue(cloneObject, clonedFieldValue);
    //        }
    //    }
    //    public static T Copy<T>(this T original)
    //    {
    //        return (T)Copy((Object)original);
    //    }

    //    // Deep clone
    //    public static T DeepClone<T>(this T a)
    //    {
    //        using (MemoryStream stream = new MemoryStream())
    //        {
    //            BinaryFormatter formatter = new BinaryFormatter();
    //            formatter.Serialize(stream, a);
    //            stream.Position = 0;
    //            return (T)formatter.Deserialize(stream);
    //        }
    //    }

    //    public static T DeepCopy<T>(this T object2Copy)
    //    {
    //        using (var stream = new MemoryStream())
    //        {
    //            var serializer = new XmlSerializer(typeof(T));

    //            serializer.Serialize(stream, object2Copy);
    //            stream.Position = 0;
    //            return (T)serializer.Deserialize(stream);
    //        }
    //    }

    //    public static object CloneObject(this object objSource)

    //    {

    //        //Get the type of source object and create a new instance of that type

    //        Type typeSource = objSource.GetType();

    //        object objTarget = Activator.CreateInstance(typeSource);



    //        //Get all the properties of source object type

    //        PropertyInfo[] propertyInfo = typeSource.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);



    //        //Assign all source property to taget object 's properties

    //        foreach (PropertyInfo property in propertyInfo)

    //        {

    //            //Check whether property can be written to

    //            if (property.CanWrite)

    //            {

    //                //check whether property type is value type, enum or string type

    //                if (property.PropertyType.IsValueType || property.PropertyType.IsEnum || property.PropertyType.Equals(typeof(System.String)))

    //                {

    //                    property.SetValue(objTarget, property.GetValue(objSource, null), null);

    //                }

    //                //else property type is object/complex types, so need to recursively call this method until the end of the tree is reached

    //                else

    //                {

    //                    object objPropertyValue = property.GetValue(objSource, null);

    //                    if (objPropertyValue == null)

    //                    {

    //                        property.SetValue(objTarget, null, null);

    //                    }

    //                    else

    //                    {

    //                        property.SetValue(objTarget, objPropertyValue.CloneObject(), null);

    //                    }

    //                }

    //            }

    //        }

    //        return objTarget;

    //    }
    //}

    //public class ReferenceEqualityComparer : EqualityComparer<Object>
    //{
    //    public override bool Equals(object x, object y)
    //    {
    //        return ReferenceEquals(x, y);
    //    }
    //    public override int GetHashCode(object obj)
    //    {
    //        if (obj == null) return 0;
    //        return obj.GetHashCode();
    //    }
    //}

    public static class ListExtension
    {
        /// <summary>
        /// Determines whether the collection is null or contains no elements.
        /// </summary>
        /// <typeparam name="T">The IEnumerable type.</typeparam>
        /// <param name="enumerable">The enumerable, which may be null or empty.</param>
        /// <returns>
        ///     <c>true</c> if the IEnumerable is null or empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                return true;
            }
            /* If this is a list, use the Count property for efficiency. 
             * The Count property is O(1) while IEnumerable.Count() is O(N). */
            var collection = enumerable as ICollection<T>;
            if (collection != null)
            {
                return collection.Count < 1;
            }
            return !enumerable.Any();
        }
    }

    namespace ArrayExtensions
    {
        public static class ArrayExtensions
        {
            public static void ForEach(this Array array, Action<Array, int[]> action)
            {
                if (array.LongLength == 0) return;
                ArrayTraverse walker = new ArrayTraverse(array);
                do action(array, walker.Position);
                while (walker.Step());
            }
        }

        internal class ArrayTraverse
        {
            public int[] Position;
            private int[] maxLengths;

            public ArrayTraverse(Array array)
            {
                maxLengths = new int[array.Rank];
                for (int i = 0; i < array.Rank; ++i)
                {
                    maxLengths[i] = array.GetLength(i) - 1;
                }
                Position = new int[array.Rank];
            }

            public bool Step()
            {
                for (int i = 0; i < Position.Length; ++i)
                {
                    if (Position[i] < maxLengths[i])
                    {
                        Position[i]++;
                        for (int j = 0; j < i; j++)
                        {
                            Position[j] = 0;
                        }
                        return true;
                    }
                }
                return false;
            }
        }
    }

}