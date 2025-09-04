using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS
{
    public struct Entity
    {
        int id;
    }
    public struct Array<T> where T : struct
    {
        public T[] array;
        public int size;
        public int capacity;

        public Array(int capacity)
        {
            array = new T[capacity];
            size = 0;
            this.capacity = capacity;
        }
        public void Swapback(int index)
        {
            array[index] = array[size - 1];
        }
    };
}
