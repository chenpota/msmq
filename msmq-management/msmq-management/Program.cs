using System;
using System.Collections;

namespace msmq_management
{
    class Program
    {
        public static ArrayList GetOutGoingQueueName()
        {
            ArrayList OutgoingQueuename = new ArrayList();
            try
            {
                MSMQ.MSMQApplication q = new MSMQ.MSMQApplication();
                object obj = q.ActiveQueues;

                Object[] oArray = (Object[])obj;
                for (int i = 0; i < oArray.Length; i++)
                {
                    if (oArray[i] == null)
                        continue;

                    if (oArray[i].ToString().IndexOf("DIRECT=HTTP") >= 0)
                    {
                        OutgoingQueuename.Add(oArray[i].ToString());
                    }
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee);
                throw;
            }
            return OutgoingQueuename;

        }

        public static int GetMessageCount(string queueName)
        {
            int count = 0;
            try
            {
                MSMQ.MSMQManagement mgmt = new MSMQ.MSMQManagement();
                MSMQ.MSMQOutgoingQueueManagement outgoing;
                String s = System.Environment.MachineName;
                Object ss = (Object)s;
                String pathName = queueName;
                Object pn = (Object)pathName;
                String format = null;
                Object f = (Object)format;

                mgmt.Init(ref ss, ref f, ref pn);

                outgoing = (MSMQ.MSMQOutgoingQueueManagement)mgmt;
                count = outgoing.MessageCount;
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee);
                throw;
            }
            return count;
        }

        public static void ReceiveMessage(string queueFormatName, int numOfMessagesToRead)
        {
            try
            {
                MSMQ.MSMQQueueInfo info = new MSMQ.MSMQQueueInfo();
                info.FormatName = queueFormatName;

                MSMQ.MSMQQueue mq = info.Open(
                    (int)(MSMQ.MQACCESS.MQ_ADMIN_ACCESS | MSMQ.MQACCESS.MQ_RECEIVE_ACCESS),
                    (int)MSMQ.MQSHARE.MQ_DENY_NONE);

                object wantdest = false;
                object tr = true;
                object num = 0;

                for (int i = 0; i < numOfMessagesToRead; i++)
                {
                    MSMQ.MSMQMessage msg = mq.ReceiveCurrent(ref wantdest,
                                           ref wantdest, ref tr, ref num, ref wantdest);
                    if (msg == null)
                        continue;
                }

            }
            catch (Exception ee)
            {
                Console.WriteLine(ee);
                throw;
            }
        }

        static void Main(string[] args)
        {
            var queueNames = GetOutGoingQueueName();

            foreach (var name in queueNames)
            {
                Console.WriteLine(name);
                Console.WriteLine(GetMessageCount((string)name));
                ReceiveMessage((string)name, 1);
                Console.WriteLine(GetMessageCount((string)name));
            }

            Console.ReadKey();
        }
    }
}
