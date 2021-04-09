using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace kakaoImti
{
    struct Value
    {
        public int dataIndex;
        public int listIndex;
        public int index;
    }

    class Trie
    {
        public Dictionary<char, Trie> children;
        public HashSet<Value> values;

        public Trie()
        {
            children = new Dictionary<char, Trie>();
            values = new HashSet<Value>();
        }

        
    }
    
    class KeywordAnalysis
    {
        Trie node;

        public KeywordAnalysis()
        {
            node = new Trie();   
        }

        public void insert(String key, Value value)
        {
            Trie root = node;

            //Console.WriteLine("key : {0}, value.index : {1}, value.listindex : {2}", key, value.index, value.listIndex);

            foreach (char c in key)
            {
                if (!root.children.ContainsKey(c))
                {
                    root.children[c] = new Trie();
                    root.values.Add(value);

                }
                root = root.children[c];
            }

            root.values.Add(value);

        }

        public HashSet<Value> findIndex(String str)
        {
            HashSet<Value> setList = new HashSet<Value>();
            Trie root = node;

            foreach (char c in str)
            {
                if (root.children.ContainsKey(c))
                    root = root.children[c];
                else
                    return setList;
            }

            setList = root.values;

            return setList;
        }

        public void AddData(int dataIndex, int listIndex, List<String> keywordTexts)
        {

            for(int i = 0; i < keywordTexts.Count; i++)
            {
                Value value = new Value();
                value.index = i;
                value.listIndex = listIndex;
                value.dataIndex = dataIndex;

                List<String> texts = keywordTexts[i].Split(',').ToList();

                foreach(String text in texts)
                {
                    insert(text, value);
                }

            }
        }



    }
}
