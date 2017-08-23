using System;
using System.Messaging;

namespace msmq_writer
{
    class Program
    {
        private const String QueueName = @".\Private$\my-test-queue";
        //private const String QueueName = @"FormatName:DIRECT=TCP:localhost\Private$\my-test-queue";
        //private const String QueueName = @"FormatName:DIRECT=HTTP://localhost/msmq/Private$\my-test-queue";
        private const String MyMsg = "test message...";

        static void Main(string[] args)
        {
            MessageQueue myQueue = new MessageQueue(QueueName);

            if (myQueue.Transactional == true)
            {
                MessageQueueTransaction myTransaction = new  MessageQueueTransaction();

                myTransaction.Begin();

                myQueue.Send(MyMsg, myTransaction);

                myTransaction.Commit();
            }
            else
            {
                myQueue.Send(MyMsg, "label");
            }

            myQueue.Close();
        }
    }
}
