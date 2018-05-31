using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using Plugin.DeviceInfo;

namespace RaccoonMsgr.Utils
{
    public static class Extensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerableList)
        {
            if (enumerableList != null)
            {
                // Create an emtpy observable collection object
                var observableCollection = new ObservableCollection<T>();

                // Loop through all the records and add to observable collection object
                foreach (var item in enumerableList)
                {
                    observableCollection.Add(item);
                }

                // Return the populated observable collection
                return observableCollection;
            }
            return null;
        }
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
    (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static string RemoveDiacriticsOnVowels(this string word)
        {

            StringBuilder sb = new StringBuilder();

            foreach (char c in word)
            {
                switch (c)
                {

                    case 'á':
                        sb.Append("a");
                        break;
                    case 'Á':
                        sb.Append("A");
                        break;
                    case 'é':
                        sb.Append("e");
                        break;
                    case 'É':
                        sb.Append("E");
                        break;
                    case 'í':
                        sb.Append("i");
                        break;
                    case 'Í':
                        sb.Append("I");
                        break;
                    case 'ó':
                        sb.Append("o");
                        break;
                    case 'Ó':
                        sb.Append("O");
                        break;
                    case 'ú':
                        sb.Append("u");
                        break;
                    case 'Ú':
                        sb.Append("U");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }

            return sb.ToString();

        }

    }
}
