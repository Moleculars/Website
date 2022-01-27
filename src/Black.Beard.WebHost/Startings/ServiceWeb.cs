//using System.Collections.Generic;

//namespace Bb.CommonHost.Startings
//{
//    public class ServiceWeb
//    {

//        public ServiceWeb()
//        {
//            WebSocket = new WebSocketEndPointModel();
//        }

//        /// <summary>
//        /// root that contains web application
//        /// </summary>
//        public string RootStaticFilePath { get; set; }

//        public bool UseSwagger { get; set; }

//        public WebSocketEndPointModel WebSocket { get; set; }

//    }

//    public class WebSocketEndPointModel
//    {
//        public bool Enabled { get; set; } = true;

//        public string Path { get; set; } = null;

//        public int ReceiveBufferSize { get; set; } = 4096;

//        public int KeepAliveInterval { get; set; } = 120;

//        public List<string> AllowedOrigins { get; set; }
//    }

//}
