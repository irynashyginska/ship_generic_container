using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;

namespace ship
{
    
    class Container<T> where T: class, new()
    {
        private List<T> l;
        public Container()
        {
            this.l = new List<T>();
        }
        public List<T> create_list(StreamReader f)
        {
            int i = 0;
            string line;
            while ((line = f.ReadLine()) != null)
            {
                this.l.Add((T)Activator.CreateInstance(typeof(T), new object[] { line.Split() }));
                i++;
            }
            
            return l;
        }
        
        public void Sort1(FieldInfo f, IComparer<object> comparer = null)
        {

            comparer = comparer ?? Comparer<object>.Default;
            this.l.Sort((a, b) => comparer.Compare(f.GetValue(a), f.GetValue(b)));
        }
        public void find_obj(string word)
        {
            int m = 0;
            for (int i = 0; i < l.Count; i++)
            {
                Type myType = typeof(T);
                FieldInfo[] myField = myType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (FieldInfo field in myField)
                {
                    string f = field.GetValue(l[i]).ToString();
                    if (f.Contains(word))
                    {
                        Console.WriteLine(l[i].ToString());
                        m++;
                    }
                }
                
            }
            if (m == 0)
            { Console.WriteLine("There are no such elements in the list:("); }
        }
        public void print_all()
        {
            Console.WriteLine("\n");
            for (int i = 0; i < l.Count; i++)
            {
                Console.WriteLine(l[i].ToString());
            }
        }
        public void delete_by_first_field(string name)
        {
            int m = 0, i = 0;
            while (i < l.Count)
            {
                FieldInfo[] Fields = l[i].GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

                string f = Fields[0].GetValue(l[i]).ToString();
                if (f.Contains(name))
                {
                    l.RemoveAt(i);
                    m++;
                }

                i += 1;
            }
            if (m == 0)
            { Console.WriteLine("There are no such elements in the list:("); }
        }
        public void add_new()
        {
            Type myType = typeof(T);
            FieldInfo[] fields = myType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            int n = fields.Length;
            string[] a = new string[n];
            for(int i = 0;i<n;i++)
            {
                Console.WriteLine("enter {0} field ",i);
                a[i] = Console.ReadLine();
            }
            l.Add((T)Activator.CreateInstance(typeof(T), new object[] { a }));
        }
        public void edit_by_first_field(string str)
        {
            int m = 0;
            for (int i = 0; i < l.Count; i++)
            {
                FieldInfo[] Fields = l[i].GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

                string f = Fields[0].GetValue(l[i]).ToString();
                if (f.Contains(str))
                {
                    int n = Fields.Count();
                    string[] a = new string[n];
                    for (int j = 0; j < n; i++)
                    {
                        Console.WriteLine("enter {0} field ", j);
                        a[j] = Console.ReadLine();
                    }
                    l[i] = (T)Activator.CreateInstance(typeof(T), new object[] { a });
                    m++;
                }
            }
            if (m == 0)
            {
                Console.WriteLine("There are no such elements in the list:(");
            }
        }
        public void write_all_to_file(StreamWriter f)
        {
            for (int i = 0; i < l.Count; i++)
            {
                f.WriteLine(l[i].ToString());
            }
        }

    }
}
