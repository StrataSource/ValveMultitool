using System;
using System.Collections.Generic;
using System.Text;
using Steamworks;
using ValveKeyValue;
using ValveKeyValue.Attributes;
using ValveMultitool.Models.Formats.Steam.Distribution.Apps.Video;
using ValveMultitool.Models.Formats.Steam.Distribution.Vr;

namespace ValveMultitool.Models.Formats.Steam.Distribution.Apps
{
    public class AppInfoCommon
    {
        /// <summary>
        /// Publically visible name of the app.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Dictionary with language names and boolean values representing if supported.
        /// </summary>
        public Dictionary<string, bool> Languages { get; set; }

        /// <summary>
        /// Type of the app, e.g. game, tool, video, etc.
        /// </summary>
        public string Type { get; set; } // Game, Config, Tool

        // Store Assets
        public string ClientIcon { get; set; }
        public string ClientTga { get; set; }
        public string Icon { get; set; }
        public string Logo { get; set; }
        [KvProperty("logo_small")]
        public string LogoSmall { get; set; }
        public string ClientIcns { get; set; }
        public string LinuxClientIcon { get; set; }
        [KvProperty("small_capsule")]
        public Dictionary<string, string> SmallCapsule { get; set; }
        [KvProperty("header_image")]
        public Dictionary<string, string> HeaderImage { get; set; }

        // Metacritic
        [KvProperty("metacritic_name")]
        public string MetacriticName { get; set; }
        [KvProperty("metacritic_score")]
        public string MetacriticScore { get; set; }
        [KvProperty("metacritic_url")]
        public string MetacriticUrl { get; set; }
        [KvProperty("metacritic_fullurl")]
        public string MetacriticFullUrl { get; set; }

        [KvProperty(CollectionType = KvCollectionType.CharSeparated, CollectionTypeSeparator = ',')]
        public string[] OsList { get; set; } // windows, macos, [linux]?
        public string OsArch { get; set; } // 32,64
        public string ReleaseState { get; set; } // unavailable, prerelease, released
        public string ReleaseStateOverride { get; set; }
        public string ReleaseStateOverrideCountries { get; set; }
        public bool ReleaseStateOverrideInverse { get; set; }
        [KvProperty("service_app")]
        public bool ServiceApp { get; set; } // Used for steamvr media player?
        public string ScriptSignature { get; set; }

        [KvProperty("section_type")]
        public string SectionType { get; set; } // ownersonly

        // Controller, VR, etc
        public ControllerVrConfig ControllerVr { get; set; }
        public PlayAreaVrConfig PlayAreaVr { get; set; }

        /// <summary>
        /// Whether this app supports VR only.
        /// </summary>
        public bool OnlyVrSupport { get; set; }
        /// <summary>
        /// What MIME types are allowed to be opened. Example: vr/game_theater
        /// </summary>
        [KvProperty("openvr_mime_types", CollectionType = KvCollectionType.CharSeparated, CollectionTypeSeparator = ',')]
        public string[] OpenVrMimeTypes { get; set; }
        public bool OpenVrSupport { get; set; }
        public bool OtherVrSupport { get; set; }
        [KvProperty("othervrsupport_rift_13")]
        public bool OtherVrSupportRift13 { get; set; }
        public bool OsVrSupport { get; set; }

        public bool RequiresKbMouse { get; set; }
        public bool KbMouseGame { get; set; }
        public string ControllerSupport { get; set; } // TODO: needs enum. full, common

        /// <summary>
        /// Whether to force the Steam overlay disabled.
        /// </summary>
        public bool DisableOverlay { get; set; }

        /// <summary>
        /// Whether the app is only visible on platforms it supports.
        /// </summary>
        public bool VisibleOnlyOnAvailablePlatforms { get; set; }

        // Steam Video
        public VideoCollection Collection { get; set; }
        public Dictionary<int, VideoSeason> Seasons { get; set; }

        // Community info
        [KvProperty("community_visible_stats")]
        public bool CommunityVisibleStats { get; set; }
        [KvProperty("community_hub_visible")]
        public bool CommunityHubVisible { get; set; }
        [KvProperty("workshop_visible")]
        public bool WorkshopVisible { get; set; }

        /// <summary>
        /// AppID of the parent app.
        /// </summary>
        public int Parent { get; set; }

        /// <summary>
        /// Whether this app is free and can be obtained on demand.
        /// </summary>
        public bool FreeOnDemand { get; set; }

        /// <summary>
        /// Equivalent to the AppID of the app.
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        /// Version of the driver being installed, if the app is a driver updater. Only used for AMD driver updaters.
        /// </summary>
        public string DriverVersion { get; set; }

        /// <summary>
        /// Whether to exclude from game library sharing.
        /// </summary>
        public bool ExfGls { get; set; }

        /// <summary>
        /// Whether purchasing this app is allowed from the restricted countries defined in <see cref="PurchaseRestrictedCountries"/>.
        /// </summary>
        public bool AllowPurchaseFromRestrictedCountries { get; set; }

        /// <summary>
        /// Array of country identifier strings that purchase is restricted to.
        /// </summary>
        [KvProperty(CollectionType = KvCollectionType.CharSeparated, CollectionTypeSeparator = ' ')]
        public string[] PurchaseRestrictedCountries { get; set; }

        // Store info
        /// <summary>
        /// Tags visible on the Steam storefront.
        /// </summary>
        [KvProperty("store_tags")]
        public List<int> StoreTags { get; set; }

        /// <summary>
        /// Provides a custom name override to sort this app by.
        /// </summary>
        public string SortAs { get; set; }

        /// <summary>
        /// Whether the game contains adult content and requires age verification.
        /// </summary>
        [KvProperty("has_adult_content")]
        public bool HasAdultContent { get; set; }

        /// <summary>
        /// Whether this app is a SteamVR plugin.
        /// </summary>
        public bool IsPlugin { get; set; }

        public bool DepotDeltaPatches { get; set; }
    }
}
