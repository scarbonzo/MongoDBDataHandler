using System;

namespace MongoDBDataHandler.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoDBDataHandler db = new MongoDBDataHandler("localhost", "messaging");

            Recipient r = new Recipient()
            {
                Name = "Richard Eodice",
                Mobile = "7329215276",
                Email = "rich@eodice.com",
                Active = true
            };

            Console.WriteLine(db.WriteObject("recipients", r).ToString());

            foreach(Recipient recipient in db.GetObjects<Recipient>("recipients"))
            {
                Console.WriteLine(recipient.Name + " - " + recipient.Mobile + " - " + recipient.Email);
            }
        }
    }
}
