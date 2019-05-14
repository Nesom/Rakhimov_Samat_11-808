using System;
using Newtonsoft.Json;
using System.IO;

namespace ThreadTask
{
    class JSONComments
    {
        public Comment[] Comments { get; set; }
    }

    public class Comment
    {
        public int Postid { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string path = string.Empty;
            var t = File.ReadAllLines(path);
            var trtr = new JSONComments
            {

            };
            string serialized = JsonConvert.SerializeObject(trtr);
            JSONComments comments = JsonConvert.DeserializeObject<JSONComments>(serialized);;
        }
    }
}
