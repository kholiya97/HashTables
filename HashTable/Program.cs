using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    public class MyMapNode<K, V>//built key value pair hashtable
    {
        //define a member elements of this class
        private readonly int size;//size of hashtable use within this class we cannot update this only read
        private readonly LinkedList<keyValue<K, V>>[] items; // defining the linklist items with readonly access

        /// <summary>
        /// Defining a constructor
        /// </summary>
        /// <param name="size">size of hashtable is passed in it</param>
        public MyMapNode(int size)
        {
            this.size = size; 
            //array of linkedlist size is defined
            this.items = new LinkedList<keyValue<K, V>>[size]; // creating object of linklist with size pass to to function my map node
        }


        protected int GetArrayPosition(K key) //GetArrayPosition is method and passing key as parameter
        {
            //hashcode is generated which tells about where the particular key and value will be stored
            int position = key.GetHashCode() % size;//GetHashCode is predifine method,to identifey the hashcode of this particular key
            return Math.Abs(position);//Math.abs function To identify integer value and At what index this purticular keyvalue is present.
        }



        public V Get(K key)//creating generic method Get
        {

            int position = GetArrayPosition(key);//call method and giving the index no of key

            //linkedlist is accessed after getting the position from the bucket of hashtable
            //getlinkedlist method is called with position of linkedlist in bucket of hashtable

            LinkedList<keyValue<K, V>> linkedList = GetLinkedList(position);//identifying the position calling the getLinkedlist()
            //linkedlist contains all the key value pairs for which same hashcode was generated 
            //each key value pair in linkedlist is of data type keyValue (struct) defined at end of class.
            //foreach loop is used to access the key and values from linkedlist
            foreach (keyValue<K, V> item in linkedList) // iterating through linkist using for eachloop
            {
                //if key element matches with the key in linkedlist, value is retuned
                //otherwise loop is iterated.
                if (item.Key.Equals(key)) 
                {
                    return item.Value; // returning specific  item value  using key
                }
            }
            return default(V);// else returing default value of linklist
        }
        protected LinkedList<keyValue<K, V>> GetLinkedList(int position)//method and GetLinkedList having return type is Linkedlist keyvalue pair which is define in line no 44
        {
            //linkedlist contains a data type of keyvalue.
            //position helps to findout the linkedlist in which key value pair will be inserted
            
            LinkedList<keyValue<K, V>> linkedList = items[position];//position is passed into array of items and stored in variable linkedlist with data type as defined below.

            if (linkedList == null) // if linked list is null
            {
                linkedList = new LinkedList<keyValue<K, V>>(); 
                //adding a linkedlist in the given array position
                items[position] = linkedList;
               
            }
            //returning linked list.
            return linkedList;
        }



        public void Add(K key, V value)//used add method and passes 2 parameter pushing the data into hashtable using linkedlist opraton
        {

            int position = GetArrayPosition(key); ///call method and giving the index no of key


            LinkedList<keyValue<K, V>> linkedList = GetLinkedList(position);
            //for adding value in linkedlist. Struct object is defined, like a class and key and value obtained as a parameter
            //to this method are passed as parameter to object
            //keyValue struct is instatiated and stores the key and value, which is passed as one object to linkedlist.
            keyValue<K, V> item = new keyValue<K, V>() { Key = key, Value = value }; // creating a new keyvaluepair using getters and setters
            //keyvalue is added in the linkedlist.
            linkedList.AddLast(item);
        }



        public void Remove(K key)//rempving particular entery  from hashtable
        {
            //getting the  position of linkedlist in which key value pair is stored and is to be removed
            int position = GetArrayPosition(key);
            //calling the dictionary from which element is to be removed.
            LinkedList<keyValue<K, V>> linkedList = GetLinkedList(position);
            bool itemFound = false;
            //if key of item matches from iteration done in particular linked list, key value pairs are assigned to foundItem variable.
            keyValue<K, V> foundItem = default(keyValue<K, V>);
            //iterating loop in linkedlist to find out the key
            foreach (keyValue<K, V> item in linkedList)
            {
                if (item.Key.Equals(key))
                {
                    itemFound = true;
                    foundItem = item;
                }
            }
            //if item is found, it is removed
            if (itemFound)
            {
                linkedList.Remove(foundItem);
            }
        }

        public void Display()
        {

            foreach (var linkedList in items)
            {

                if (linkedList != null)
                    foreach (keyValue<K, V> keyvalue in linkedList)
                    {

                        Console.WriteLine(keyvalue.Key + "\t" + keyvalue.Value);
                    }
            }
        }

    }

    /// <summary>
    /// Defining a struct data type to store key and value
    /// struct is similar to class and used to hold values
    /// </summary>

    public struct keyValue<k, v> // creating a structure of key value 
    {

        public k Key { get; set; } // getter and stter for key $ value 
        public v Value { get; set; }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----Welcome To HashTable Program-----");

            MyMapNode<string, string> hash = new MyMapNode<string, string>(5);//creating object and Key and value datatype is string and size is 5
            // Adding values in hashtable.
            hash.Add("0", "To");
            hash.Add("1", "be");
            hash.Add("2", "or");
            hash.Add("3", "not");
            hash.Add("4", "To");
            hash.Add("5", "be");


            // getting the specific value from hashtable.
            string hash5 = hash.Get("5");
            Console.WriteLine("5th index value:" + hash5);
            // hash.Remove("0");
            hash.Display();
            Console.Read();
        }
    }
}
