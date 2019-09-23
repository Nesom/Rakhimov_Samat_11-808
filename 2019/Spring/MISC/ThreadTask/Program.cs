using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ThreadTask
{

    public class JsonComment
    {
        public int postId;
        public int id;
        public string name;
        public string email;
        public string body;
    }
    class Program
    {
        static void LettersCount(JsonComment x)
        {
            string body = x.body;
            int number = 0;
            for (int i =0;i<body.Length; i++)
                if (char.IsLetter(body[i])) number++;
            Console.WriteLine("id is "+x.id+", count is "+number);
        }
        static void Main(string[] args)
        {
            var comments = JsonConvert.DeserializeObject<List<JsonComment>>(File.ReadAllText("allComments.txt"));
            Parallel.ForEach(comments.Where(x => x.id % 2 == 0), x => LettersCount(x));
        }
    }
}
