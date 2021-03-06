﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Aark.Netatmo.SDK.Energy
{
    /// <summary>
    /// Thermostat of Netatmo.
    /// </summary>
    public class Thermostat : ICommonEnergyDevice
    {
        /// <summary>
        /// Id of the device.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Name of the device.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Date on which the device was setted. 
        /// </summary>
        public DateTime SetupDate { get; set; }
        /// <summary>
        /// Id of the room in which the thermostat is located.
        /// </summary>
        public long? RoomId { get; set; }
    }
}
