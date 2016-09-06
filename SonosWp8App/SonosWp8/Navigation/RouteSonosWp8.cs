using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sonos.Models;
using SonosWp8.ViewModels;

namespace SonosWp8.Navigation
{
    public sealed class RouteSonosWp8 : IRouteMap
    {
        private static readonly IReadOnlyCollection<IRouteMap> Map;

        public static readonly RouteSonosWp8 Unknown = new RouteSonosWp8(null, "", "");

        public static readonly RouteSonosWp8 Base = new RouteSonosWp8(typeof (BaseViewModel<Container>),
            "object.container", "/Views/TitleOnlyView.xaml");

        public static readonly RouteSonosWp8 MusicAlbum = new RouteSonosWp8(typeof (PlaylistViewModel),
            "object.container.album.musicAlbum", "/Views/MusicAlbumView.xaml");

        public static readonly RouteSonosWp8 AlbumList = new RouteSonosWp8(typeof (AlbumListViewModel),
            "object.container.albumlist", "/Views/ImageArtistAndTitleView.xaml");

        public static readonly RouteSonosWp8 TrackList = new RouteSonosWp8(typeof (BaseViewModel<Container>),
            "object.container.tracklist", "/Views/TitleOnlyView.xaml");

        public static readonly RouteSonosWp8 Genre = new RouteSonosWp8(typeof (BaseViewModel<Container>),
            "object.container.genre.musicGenre", "/Views/TitleOnlyView.xaml");

        public static readonly RouteSonosWp8 Artist = new RouteSonosWp8(typeof (BaseViewModel<Container>),
            "object.container.person.musicArtist", "/Views/TitleOnlyView.xaml");

        public static readonly RouteSonosWp8 SameArtist = new RouteSonosWp8(typeof (SameArtistViewModel),
            "object.container.playlistContainer.sameArtist", "/Views/SameArtistView.xaml");

        public static readonly RouteSonosWp8 Playlist = new RouteSonosWp8(typeof (PlaylistViewModel),
            "object.container.playlistContainer", "/Views/ImageArtistAndTitleView.xaml");

        public static readonly RouteSonosWp8 Track = new RouteSonosWp8(typeof (BaseViewModel<Container>),
            "object.item.audioItem.musicTrack", "/Views/TitleOnlyView.xaml");

        public static readonly RouteSonosWp8 Queue = new RouteSonosWp8(typeof (QueueViewModel), "queue",
            "/Views/ImageArtistAndTitleView.xaml");

        // App items navigation
        public static readonly RouteSonosWp8 Settings = new RouteSonosWp8(null, "settings", "/Views/SettingsView.xaml");

        public static readonly RouteSonosWp8 Services = new RouteSonosWp8(typeof (ServicesViewModel), "MService",
            "/Views/ServicesView.xaml");

        public static readonly RouteSonosWp8 Radio = new RouteSonosWp8(typeof (ServicesViewModel), "Radio",
            "/Views/TitleOnlyView.xaml");

        public static readonly RouteSonosWp8 Alarms = new RouteSonosWp8(null, "alarms", "/Views/AlarmsView.xaml");

        public static readonly RouteSonosWp8 Library = new RouteSonosWp8(typeof (LibraryViewModel), "library",
            "/Views/TitleOnlyView.xaml");

        public static readonly RouteSonosWp8 Rooms = new RouteSonosWp8(null, "rooms",
            "/Views/RoomsView.xaml");

        public static readonly RouteSonosWp8 Music = new RouteSonosWp8(typeof (MusicViewModel), "music",
            "Views/MusicView.xaml");

        public static readonly RouteSonosWp8 Favorites = new RouteSonosWp8(typeof (FavoritesViewModel), "SonosFavorites",
            "/Views/ImageArtistAndTitleView.xaml");

        public static readonly RouteSonosWp8 SonosPlaylists = new RouteSonosWp8(typeof (BaseViewModel<Container>),
            "SonosPlaylists", "/Views/TitleOnlyView.xaml");

        public static readonly RouteSonosWp8 NowPlaying = new RouteSonosWp8(typeof (NowPlayingViewModel), "nowPlaying",
            "/Views/NowPlayingView.xaml");

        public static readonly RouteSonosWp8 Search = new RouteSonosWp8(typeof (SearchViewModel), "search",
            "/Views/SearchView.xaml");

        static RouteSonosWp8()
        {
            // create map.
            var map = typeof (RouteSonosWp8).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(f => f.FieldType == typeof (RouteSonosWp8))
                .Select(f => f.GetValue(null) as RouteSonosWp8);

            Map = map.ToList();
        }

        private RouteSonosWp8(Type type, string classId, string uri)
        {
            Id = classId;
            Type = type;
            Uri = uri;
        }

        public string Id { get; private set; }
        public string Uri { get; private set; }
        public Type Type { get; private set; }


        public IReadOnlyCollection<IRouteMap> GetRoutes()
        {
            return Map;
        }
    }
}