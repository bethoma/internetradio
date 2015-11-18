using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;

namespace InternetRadio
{
    internal class AnnouncementManager : IAnnouncementManager
    {
        private IPlaybackManager announcementPlaybackManager;
        private IPlaybackManager radioPlaybackManager;
        private SpeechSynthesizer speechSynthesizer;
        private double previousVolume;
        private string announcementPrefix;

        public async Task Initialize(IPlaybackManager radioPlaybackManager, string announcementPrefix)
        {
            this.radioPlaybackManager = radioPlaybackManager;
            this.previousVolume = this.radioPlaybackManager.Volume;
            this.speechSynthesizer = new SpeechSynthesizer();
            this.announcementPlaybackManager = new MediaEnginePlaybackManager();
            await this.announcementPlaybackManager.InitialzeAsync();
            this.announcementPlaybackManager.PlaybackStateChanged += AnnouncementPlaybackManager_PlaybackStateChanged;
            this.announcementPrefix = announcementPrefix;
        }

        private void AnnouncementPlaybackManager_PlaybackStateChanged(object sender, PlaybackStateChangedEventArgs e)
        {
            switch(e.State)
            {
                case PlaybackState.Playing:
                    this.previousVolume = this.radioPlaybackManager.Volume;
                    this.radioPlaybackManager.Volume = 0;
                    break;
                case PlaybackState.Ended:
                    this.radioPlaybackManager.Volume = this.previousVolume;
                    break;
            }
        }

        public async Task MakeAnnouncement(string annoucementContent)
        {
            var announceSsml = @"<speak version='1.0' " +
            "xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='en-US'>" +
            this.announcementPrefix +
            "<break />" +
            annoucementContent +
            "</speak>";
            SpeechSynthesisStream stream = await this.speechSynthesizer.SynthesizeSsmlToStreamAsync(announceSsml);
            this.announcementPlaybackManager.Play(stream);
        }
    }
}
