﻿using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.ViewModels
{
    public class MessageInboxViewModel
    {
        public IEnumerable<Message> headMessages { get; set; }
        public int UnreadMessageCount { get; set; }
    }
}
