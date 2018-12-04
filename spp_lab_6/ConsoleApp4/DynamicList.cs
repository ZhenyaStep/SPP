using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public class DynamicList<T> : IEnumerable<T>
    {
        public DynamicList()
        {
            this.count = 0;
            this.Arr = new T[0];
        }

        //public 
        public ConsoleApp4.DynamicList<T> Items { get; set; }

        private T[] Arr;

        private int count;

        public int Count
        {
            get
            {
                return this.count;
            }
        }



        public T this[int index]
        {
            get
            {
                try
                {
                    return this.Arr[index];
                }
                catch (IndexOutOfRangeException ex)
                {
                    throw new IndexOutOfRangeException(ex.Message);
                }
            }
            set
            {
                this.Arr[index] = value;
            }
        }

        public bool Add(T Element)
        {
            try
            {
                Array.Resize(ref this.Arr, this.count + 1);
                Arr[this.count++] = Element;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private T[] ShiftArray(T[] Arr, int start_index)
        {
            T[] Arr_shifted = new T[Arr.Length - 1];
            for (int i = 0; i < start_index; i++)
            {
                Arr_shifted[i] = Arr[i];
            }
            for(int i = start_index+1; i < Arr.Length; i++)
            {
                Arr_shifted[i-1] = Arr[i];
            }
            return Arr_shifted;
        }


        public bool Remove(T Element)
        {
            try
            {
                for(int i = 0; i < this.count; i++)
                {
                    if (this.Arr[i].Equals(Element))
                    {
                        this.Arr = this.ShiftArray(Arr, i);
                        this.count--;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool RemoveAt(int index)
        {
            try
            {
                this.Arr = this.ShiftArray(Arr, index);
                this.count--;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool Clear()
        {
            Array.Clear(this.Arr, 0, this.Arr.Length);
            Array.Resize(ref this.Arr, 0);
            this.count = 0;
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)Arr).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)Arr).GetEnumerator();
        }
    }
}
