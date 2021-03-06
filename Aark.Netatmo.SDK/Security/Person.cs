﻿using System;

namespace Aark.Netatmo.SDK.Security
{
    /// <summary>
    /// Defines a person.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Identifier of the person.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Pseudonymous of the person if identified.
        /// </summary>
        public string Pseudo { get; set; }
        /// <summary>
        /// Face of the person.
        /// </summary>
        public Snapshot Face { get; set; }
        /// <summary>
        /// Last time the person was seen.
        /// </summary>
        public DateTime LastSeen { get; set; }
        /// <summary>
        /// Indicates whether the person is considered absent because he or she has not seen for some time.
        /// </summary>
        public bool OutOfSight { get; set; }
    }
}
