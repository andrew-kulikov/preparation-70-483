using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Common;

namespace Debugging.Examples
{
    [DataContract]
    public class Person
    {
        [DataMember] public string Name { get; set; }
    }

    public class JsonExample : Example
    {
        private Person _vasya;

        public JsonExample() : base("Json example", "3.1")
        {
            _vasya = new Person { Name = "vasya" };
        }

        public override void Execute()
        {
            var dataContractVasya = SerializeUsingDataContract();

            Console.WriteLine(dataContractVasya);
        }

        public string SerializeUsingDataContract()
        {
            var serializer = new DataContractJsonSerializer(typeof(Person));

            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, _vasya);

                stream.Position = 0;

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}