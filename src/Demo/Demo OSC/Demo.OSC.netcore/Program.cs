﻿using StarDust.CasparCG.net.Connection;
using StarDust.CasparCG.net.OSC;
using System;
using Unity;

namespace Demo.OSC.netcore
{
    class Program
    {

        static IUnityContainer _container;

        static void ConfigureIOC()
        {
            _container = new UnityContainer();
            _container.RegisterType<IOscListener, OscListener>();
            _container.RegisterInstance<IServerConnection>(new ServerConnection(new CasparCGConnectionSettings("127.0.0.1")));
        }


        static void Main(string[] args)
        {
            ConfigureIOC();


            OscStart();

            while (true)
            {
                var input = Console.ReadLine();
                if (input.Equals("osc start", StringComparison.InvariantCultureIgnoreCase))
                    OscStart();
                if (input.Equals("osc stop", StringComparison.InvariantCultureIgnoreCase))
                    OscStop();
            }
        }


        private static void OscStop()
        {
            var oscListener = _container.Resolve<OscListener>();
            oscListener.StopListening();
            _container.Resolve<IServerConnection>().Disconnect();
            Console.WriteLine("Osc listener stopped");

        }

        private static async void OscStart()
        {
            _container.Resolve<IServerConnection>().Connect();
            var oscListener = _container.Resolve<IOscListener>();
            //oscListener.RegisterMethod("/channel/1/stage/layer/1/file/time");
            oscListener.AddToAddressBlackList("/channel/[0-9]/output/consume_time");
            oscListener.AddToAddressBlackList("/channel/1/stage/layer/1/profiler/time");

            oscListener.OscMessageReceived += OscListener_OscMessageReceived;
            oscListener.StartListening("127.0.0.1", 6250);
            Console.WriteLine("Osc listener strarted");
        }



        private static void OscListener_OscMessageReceived(object sender, OscMessageEventArgs e)
        {
            Console.WriteLine($"Osc message received: {e.OscPacket.ToString()}");
        }

    }
}