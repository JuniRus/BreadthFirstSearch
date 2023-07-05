using Nito.Collections;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BreadthFirstSearch
{
    class Program
    {
        /// <summary>
        /// Возвращает true, если имя человека заканчивается на букву 'm'.
        /// </summary>
        static bool IsSellerMango(string name)
        {
            // Если имя заканчивается на букву 'm', то это продавец манго.
            if (name[name.Length - 1] == 'm')
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Добавляет имена из хеш-таблицы по ключу(имени) в конец дека.
        /// </summary>
        /// <returns>
        /// Дек с добавленными в конец элементами хеша.
        /// </returns>
        static Deque<string> AddNamesToDeque(Hashtable hashtable, Deque<string> deque, string name)
        {
            // Из хеша промещаются все друзья в строковой массив.
            string[] names = (string[])hashtable[name];

            // Друзья добавляются в конец дека.
            foreach (string person in names)
            {
                deque.AddToBack(person);
            }

            return deque;
        }

        /// <summary>
        /// Алгоритм поиска в ширину. Если продавец манго 
        /// имеется среди друзей, то выводится его имя, и метоод
        /// возвращает значение true, в противном случае - false.
        /// </summary>
        /// <returns>
        /// True если есть продавец манго в списке друзей; False -
        /// если продавца манго нет в списке друзей.
        /// </returns>
        static bool BreadthFirstSearch(Hashtable friends, string name)
        {
            Deque<string> search_deque = new Deque<string>();
            // Массив проверенных людей
            List<string> searched = new List<string>();

            search_deque = AddNamesToDeque(friends, search_deque, name);

            // Искать продавца манго пока очередь не опустеет.
            while(search_deque.Count > 0)
            {
                // Извлекается первый человек из дека.
                string person = search_deque.RemoveFromFront();

                // Если человека нет в списке проверенных...
                if(!searched.Contains(person))
                {
                    // и он продавец манго, то выводится его имя.
                    if(IsSellerMango(person))
                    {
                        Console.WriteLine("Продавец манго --> " + person + "!");
                        return true;
                    }
                    // и человек не продавец манго, то в дек добавляются его друзья,
                    // а сам он добавляется в список проверенных.
                    else
                    {
                        searched.Add(person);
                        search_deque = AddNamesToDeque(friends, search_deque, person);
                    }
                }
            }

            return false;
        }

        static void Main()
        {
            Hashtable friends = new Hashtable();

            friends.Add("you", new string[] { "alice", "bob", "claire" });
            friends.Add("bob", new string[] { "anuj", "peggy" });
            friends.Add("alice", new string[] { "peggy" });
            friends.Add("claire", new string[] { "thom", "jonny" });
            friends.Add("anuj", new string[] { });
            friends.Add("peggy", new string[] { });
            friends.Add("thom", new string[] { });
            friends.Add("jonny", new string[] { });

            BreadthFirstSearch(friends, "you");

            Console.ReadLine();
        }
    }
}
