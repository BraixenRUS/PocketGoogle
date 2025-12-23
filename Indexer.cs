using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketGoogle
{
    public class Indexer
    {
        private static readonly char[] separators = { ' ', '.', ',', '!', '?', ':', '-', '\r', '\n' };
        private readonly Dictionary<int, Dictionary<string, List<int>>> index = new Dictionary<int, Dictionary<string, List<int>>>();

        public void Add(int id, string documentText)
        {
            var words = documentText.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            var wordPositions = new Dictionary<string, List<int>>();

            var position = 0;
            foreach (var word in words)
            {
                if (!wordPositions.ContainsKey(word))
                {
                    wordPositions[word] = new List<int>();
                }
                wordPositions[word].Add(position);
                position += word.Length + 1;
            }

            index[id] = wordPositions;
        }

        public List<int> GetIds(string word)
        {
            var result = new List<int>();
            foreach (var entry in index)
            {
                if (entry.Value.ContainsKey(word))
                {
                    result.Add(entry.Key);
                }
            }
            return result;
        }

        public List<int> GetPositions(int id, string word)
        {
            if (index.ContainsKey(id) && index[id].ContainsKey(word))
            {
                return index[id][word];
            }
            return new List<int>();
        }

        public void Remove(int id)
        {
            index.Remove(id);
        }
    }
}