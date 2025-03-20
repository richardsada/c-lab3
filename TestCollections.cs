using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
// pochistit kommenti  i GenerateNonExistentKey()    
namespace Lab3
{
    // Универсальный делегат для генерации элементов
    public delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);

    public class TestCollections<TKey, TValue>
    {
        // Коллекции для тестирования
        private List<TKey> _listTKey;
        private List<string> _listString;
        private Dictionary<TKey, TValue> _dictTKeyTValue;
        private Dictionary<string, TValue> _dictStringTValue;
        private readonly GenerateElement<TKey, TValue> _generateElement;

        public TestCollections(int count, GenerateElement<TKey, TValue> generator)
        {
            if (count <= 0)
                throw new ArgumentException("Количество элементов должно быть больше 0.");

            _generateElement = generator;
            InitializeCollections(count);
        }
        private void InitializeCollections(int count)
        {
            _listTKey = new List<TKey>();
            _listString = new List<string>();
            _dictTKeyTValue = new Dictionary<TKey, TValue>();
            _dictStringTValue = new Dictionary<string, TValue>();

            for (int j = 0; j < count; j++)
            {
                var element = _generateElement(j);
                _listTKey.Add(element.Key);
                _listString.Add(element.Key.ToString());
                _dictTKeyTValue[element.Key] = element.Value;
                _dictStringTValue[element.Key.ToString()] = element.Value;
            }
        }

        public void MeasureSearchTime()
        {
            var elementsToSearch = new[]
            {
                _listTKey[0],                   // Первый элемент
                _listTKey[_listTKey.Count / 2], // Центральный элемент
                _listTKey[^1],                  // Последний элемент
                _generateElement(1000).Key      // Несуществующий элемент
            };

            foreach (var key in elementsToSearch)
            {
                MeasureListSearch(key);
                MeasureDictSearch(key);
                MeasureValueSearch(key);
                Console.WriteLine(new string('-', 50));
            }
        }

        private void MeasureListSearch(TKey key)
        {
            var sw = Stopwatch.StartNew();
            bool foundInTKey = _listTKey.Contains(key);
            sw.Stop();
            if (foundInTKey) { Console.WriteLine($"List<TKey> поиск [{key}]: {sw.ElapsedTicks} тиков"); }
            else { Console.WriteLine($"List<TKey> поиск не прошел  {sw.ElapsedTicks} тиков"); }


            string strKey = key.ToString();
            sw.Restart();
            bool foundInString = _listString.Contains(strKey);
           
            sw.Stop();
            if (foundInString) { Console.WriteLine($"List<string> поиск [{strKey}]: {sw.ElapsedTicks} тиков"); }
            else { Console.WriteLine($"List<string> поиск не прошел за {sw.ElapsedTicks} тиков"); };

        }

        private void MeasureDictSearch(TKey key)
        {
            var sw = Stopwatch.StartNew();
            bool foundKeyInDict = _dictTKeyTValue.ContainsKey(key);
            sw.Stop();
            Console.WriteLine($"Dictionary<TKey, TValue> по ключу [{key}]: {sw.ElapsedTicks} тиков");

            string strKey = key.ToString();
            sw.Restart();
            bool foundStringInDict = _dictStringTValue.ContainsKey(strKey);
            sw.Stop();
            if (foundKeyInDict) { Console.WriteLine($"Dictionary<string, TValue> по ключу [{strKey}]: {sw.ElapsedTicks} тиков"); }
            else { Console.WriteLine($"Dictionary<string, TValue> по ключу не прошел за {sw.ElapsedTicks} тиков"); }
        }

        private void MeasureValueSearch(TKey key)
        {
            TValue value;
            bool keyExists = _dictTKeyTValue.TryGetValue(key, out value);

            

            var sw = Stopwatch.StartNew();
            bool containsValue = _dictTKeyTValue.ContainsValue(value);
            sw.Stop();
            if (containsValue) { Console.WriteLine($"Dictionary<TKey, TValue> поиск значения [{value.ToString().Remove(60)}]: {sw.ElapsedTicks} тиков");}
            else { Console.WriteLine($"Dictionary<TKey, TValue> поиск значения не прошел за {sw.ElapsedTicks} тиков"); }
            
        }

       
     }

        
 }
