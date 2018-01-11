using MongoDB.Bson;
using System;
using System.Linq;

namespace MongoDBDataHandler.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoDBDataHandler db = new MongoDBDataHandler("localhost", "messaging");

            // Create a new Recipient Object
            Recipient r = new Recipient()
            {
                Name = "Richard Eodice",
                Mobile = "7329215276",
                Email = "rich@eodice.com",
                Active = true
            };

            // Write the object to the DB
            ObjectId oid = (ObjectId)db.WriteObject("recipients", r);
            string id = oid.ToString();

            // Read the object back from the DB and write it to the console
            var recip = db.GetObjects<Recipient>("recipients")
                .Where(x => x.Id == oid)
                .FirstOrDefault();

            Console.WriteLine(recip.ToString());

            r.Mobile = "7329215277";

            db.UpdateObject("recipients", id, r);

            var recipnew = db.GetObjects<Recipient>("recipients")
                .Where(x => x.Id == oid)
                .FirstOrDefault();

            Console.WriteLine(recipnew.ToString());

            foreach (Recipient recipient in db.GetObjects<Recipient>("recipients"))
            {
                Console.WriteLine(recipient.Name + " - " + recipient.Mobile + " - " + recipient.Email);
                db.DeleteObject("recipients", recipient.Id.ToString());
            }
        }
    }
}
