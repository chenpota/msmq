using System;
using System.Messaging;

namespace msmq_reader
{
    class Program
    {
        private const String QueueName = @".\Private$\my-test-queue";

        static void Main(string[] args)
        {
            MessageQueue myQueue;
            if (MessageQueue.Exists(QueueName))
                myQueue = new MessageQueue(QueueName);
            else
                myQueue = MessageQueue.Create(QueueName, false);

            myQueue.Formatter = new XmlMessageFormatter(
                new Type[]
                {
                    typeof(String)
                });

            Message myMsg = myQueue.Receive();

            Console.WriteLine((String)myMsg.Body);

            myQueue.Close();

            Console.ReadKey();
        }
    }
}
