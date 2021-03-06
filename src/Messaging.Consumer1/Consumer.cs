﻿using Messaging.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;

namespace Consumer1
{
    public class Consumer
    {
        public IConfiguration Configuration { get; }
        private readonly IQueueAccessor _queueAccessor;
        //public string _queueUrl = "https://sqs.us-east-1.amazonaws.com/271828920772/Widgets";
        //public string _queueUrl = "https://sqs.us-east-2.amazonaws.com/271828920772/widgets.fifo";
        public string _queueUrl = "https://sqs.us-east-2.amazonaws.com/271828920772/widgets";

        public Consumer(IConfiguration configuration,
            IQueueAccessor queueAccessor)
        {
            Configuration = configuration;
            _queueAccessor = queueAccessor;
        }

        public void Execute()
        {
            //var options = Configuration.GetAWSOptions();

            //Console.WriteLine($"Profile: {options.Profile}");
            //Console.WriteLine($"Region: {options.Region}");
            ListQueues();
            ConsumeMessages();

        }

        public void ConsumeMessages()
        {

            while (true)
            {
                _queueAccessor.HandleAndDeleteMessage(msg => Console.WriteLine($"Handling message: {msg}!"));
                Thread.Sleep(100);
            }
        }


        public void ListQueues()
        {
            Console.WriteLine("Queues:");

            var urls = _queueAccessor.ListQueueUrls();

            if (urls.Any())
            {
                Console.WriteLine("Queue URLs:");

                foreach (var url in urls)
                {
                    Console.WriteLine("  " + url);
                }
            }
            else
            {
                Console.WriteLine("No queues.");
            }
        }
    }
}
