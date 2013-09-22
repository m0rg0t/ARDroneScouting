using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.Drone2.Sample.W8.ViewModel
{
    public class MessageItem: ViewModelBase
    {
        public MessageItem()
        {
        }

        private string _message;
        /// <summary>
        /// 
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private long _id;
        /// <summary>
        /// message id
        /// </summary>
        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }
        

        private string _channelId;
        /// <summary>
        /// Channel id number
        /// </summary>
        public string ChannelId
        {
            get { return _channelId; }
            set { _channelId = value; }
        }

        private double _lat;
        /// <summary>
        /// 
        /// </summary>
        public double Lat
        {
            get { return _lat; }
            set { _lat = value; }
        }

        private double _lon;
        /// <summary>
        /// 
        /// </summary>
        public double Lon
        {
            get { return _lon; }
            set { _lon = value; }
        }
        
        
    }
}
