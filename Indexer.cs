using System;
using System.Collections.Generic;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        public Dictionary<int, List<string>> IdsDoc = new Dictionary<int, List<string>>();
        Dictionary<int, List<int>> pos = new Dictionary<int,List<int>>();
        public void Add(int id, string documentText)
        {
            List<string> words = new List<string>();
            string word = "";
           List<int> posit = new List<int>();
            for (int a = 0; a < documentText.Length; a++)
            {
                if (char.IsLetter(documentText[a])) word += documentText[a];
                else if (word.Length > 0)
                {
                    words.Add(word);
                    posit.Add(a-word.Length);
                    word = "";
                }
                if (a == documentText.Length - 1)
                {
                    words.Add(word);
                    posit.Add(documentText.Length - word.Length);
                }
            }
            IdsDoc.Add(id, words);
            pos.Add(id, posit);

        }

        public List<int> GetIds(string word)
        {
            List<int> ids = new List<int>();
            foreach (var a in IdsDoc.Keys)
            {
                if (IdsDoc[a].Contains(word))
                    ids.Add(a);
            }
            return ids;
        }

        public List<int> GetPositions(int id, string word)
        {
            List<int> posit = new List<int>();
            int i = 0;
            foreach (var a in IdsDoc[id]) {
                if (a == word)
                    posit.Add(pos[id][i]);
                i++;
            }
            return posit;

        }

        public void Remove(int id)
        {
            IdsDoc.Remove(id);
            pos.Remove(id);
        }
    }
}
