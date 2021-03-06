﻿using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.IO;
using System;
using Aark.Netatmo.SDK.Security;
using Aark.Netatmo.SDK.Helpers;
using System.Collections.Generic;

namespace Aark.Netatmo.SDK.Test
{
    [TestClass]
    public class SecurityStationTest
    {
        private readonly NetatmoManager netatmoManager;

        public SecurityStationTest()
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string clientId = configuration.GetConnectionString("ClientId");
            string clientSecret = configuration.GetConnectionString("ClientSecret");
            string netatmoAccount = configuration.GetConnectionString("NetatmoAccount");
            string netatmoPassword = configuration.GetConnectionString("NetatmoPassword");
            netatmoManager = new NetatmoManager(clientId, clientSecret, netatmoAccount, netatmoPassword);
        }

        [TestMethod]
        public async Task TestLoadSecurityDataAsync()
        {
            bool result = await netatmoManager.LoadSecurityDataAsync();
            Assert.IsTrue(result, netatmoManager.GetLastError());
        }

        [TestMethod]
        public async Task TestGetCameraPicture()
        {
            await netatmoManager.LoadSecurityDataAsync();
            foreach (Home home in netatmoManager.SecurityStation.Homes)
            {
                if (home.Persons != null)
                {
                    Uri result = NetatmoManager.GetCameraPicture(home.Persons[0].Face.Id, home.Persons[0].Face.Key);
                    Assert.IsNotNull(result, netatmoManager.GetLastError());
                    return;
                }
                else
                    Assert.IsTrue(false, "No person available for this test.");
            }
        }

        [TestMethod]
        public async Task TestGetLiveStream()
        {
            string cameraId;
            await netatmoManager.LoadSecurityDataAsync();
            foreach (Home home in netatmoManager.SecurityStation.Homes)
            {
                if (home.Cameras != null && home.Cameras.Count > 0)
                {
                    cameraId = home.Cameras[0].Id;
                    Uri result = await netatmoManager.GetLiveStream(cameraId);
                    Assert.IsNotNull(result, netatmoManager.GetLastError());
                    return;
                }
            }
            Assert.IsTrue(false, "No camera available for this test.");
        }

        [TestMethod]
        public async Task TestGetVodStream()
        {
            string homeId;
            string cameraId;
            string videoId;
            string eventId = "";
            await netatmoManager.LoadSecurityDataAsync();
            foreach (Home home in netatmoManager.SecurityStation.Homes)
            {
                if (home.Cameras != null && home.Cameras.Count > 0 && home.Events != null && home.Events.Count > 0)
                {
                    homeId = home.Id;
                    cameraId = home.Cameras[0].Id;
                    foreach (SecurityEvent securityEvent in home.Events)
                    {
                        if (securityEvent.VideoStatus == VideoStatus.Available)
                        {
                            videoId = securityEvent.VideoId;
                            Uri result = await netatmoManager.GetVodStream(cameraId, videoId);
                            Assert.IsNotNull(result, netatmoManager.GetLastError());
                            return;
                        }
                        eventId = securityEvent.Id;
                    }
                    while (await netatmoManager.GetNextSecurityEvents(homeId, eventId))
                    {
                        foreach (SecurityEvent securityEvent in home.Events)
                        {
                            if (securityEvent.VideoStatus == VideoStatus.Available)
                            {
                                videoId = securityEvent.VideoId;
                                Uri result = await netatmoManager.GetVodStream(cameraId, videoId);
                                Assert.IsNotNull(result, netatmoManager.GetLastError());
                                return;
                            }
                            eventId = securityEvent.Id;
                        }
                    }
                    Assert.IsTrue(false, "No video available for this test.");
                }
            }
            Assert.IsTrue(false, "No camera available for this test.");
        }

        [TestMethod]
        public async Task TestSetPersonsAtHome()
        {
            await netatmoManager.LoadSecurityDataAsync();
            foreach (Home home in netatmoManager.SecurityStation.Homes)
            {
                if (home.Persons != null)
                {
                    List<string> personIds = new List<string>();
                    foreach (Person person in home.Persons)
                    {
                        personIds.Add(person.Id);
                    }
                    bool result = await netatmoManager.SetPersonsAtHome(home.Id, personIds);
                    Assert.IsTrue(result, netatmoManager.GetLastError());
                    return;
                }
            }
        }

        [TestMethod]
        public async Task TestSetPersonAwayFromHome()
        {
            await netatmoManager.LoadSecurityDataAsync();
            foreach (Home home in netatmoManager.SecurityStation.Homes)
            {
                if (home.Persons != null)
                {
                    bool result = await netatmoManager.SetPersonAwayFromHome(home.Id, home.Persons[0].Id);
                    Assert.IsTrue(result, netatmoManager.GetLastError());
                    return;
                }
            }
        }

        [TestMethod]
        public async Task TestSetHomeEmpty()
        {
            await netatmoManager.LoadSecurityDataAsync();
            foreach (Home home in netatmoManager.SecurityStation.Homes)
            {
                if (home.Persons != null)
                {
                    bool result = await netatmoManager.SetHomeEmpty(home.Id);
                    Assert.IsTrue(result, netatmoManager.GetLastError());
                    return;
                }
            }
        }

        [TestMethod]
        public async Task TestRegisterWebHook()
        {
            bool result = await netatmoManager.RegisterWebHook(new Uri("https://www.aarklendoia.com"));
            Assert.IsTrue(result, netatmoManager.GetLastError());
        }

        [TestMethod]
        public async Task TestUnregisterWebHook()
        {
            bool result = await netatmoManager.UnregisterWebHook();
            Assert.IsTrue(result, netatmoManager.GetLastError());
        }
    }
}