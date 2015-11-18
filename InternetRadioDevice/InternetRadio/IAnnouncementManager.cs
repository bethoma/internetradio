using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetRadio
{
    interface IAnnouncementManager
    {
        Task Initialize(IPlaybackManager radioPlaybackManager, string announcementPrefix);
        Task MakeAnnouncement(string annoucementContent);
    }
}
