﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.Storage.Streams;

namespace InternetRadio
{
    internal enum PlaybackState
    {
        Error_MediaInvalid = 0,
        Error_LostConnection,

        Stopped = 100,
        Paused,
        Loading,
        Playing,
        Ended
    }

    internal struct VolumeChangedEventArgs
    {
        public double Volume;
    }

    internal struct PlaybackStateChangedEventArgs
    {
        public PlaybackState State;
    }

    delegate void VolumeChangedEventHandler(object sender, VolumeChangedEventArgs e);
    delegate void PlaybackStatusChangedEventHandler(object sender, PlaybackStateChangedEventArgs e);

    internal interface IPlaybackManager
    {
        event VolumeChangedEventHandler VolumeChanged;
        event PlaybackStatusChangedEventHandler PlaybackStateChanged;

        double Volume
        {
            get;
            set;
        }

        PlaybackState PlaybackState
        {
            get;
        }

        Task InitialzeAsync();

        [DefaultOverload]
        void Play(Uri mediaAddress);

        void Play(IRandomAccessStream stream);

        void Pause();

        void Stop();
    }
}
