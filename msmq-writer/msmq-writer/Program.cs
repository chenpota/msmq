using System;
using System.Messaging;

namespace msmq_writer
{
    class Program
    {
        //private const String QueueName = @".\Private$\my-test-queue";
        //private const String QueueName = @"FormatName:DIRECT=TCP:192.168.56.102\Private$\my-test-queue";
        private const String QueueName = @"FormatName:DIRECT=HTTP://192.168.56.102/msmq/Private$\my-test-queue";
        private const String MyMsg = "test message...";

        static void Main(string[] args)
        {
            MessageQueue myQueue = new MessageQueue(QueueName);

            /*if (myQueue.Transactional == true)
            {
                MessageQueueTransaction myTransaction = new  MessageQueueTransaction();

                myTransaction.Begin();

                myQueue.Send(MyMsg, myTransaction);

                myTransaction.Commit();
            }
            else*/
            {
                myQueue.Send(MyMsg);
            }

            myQueue.Close();
        }
    }
}
