﻿using DiscordRPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateOsuServerSwitcher
{
  public static class Discord
  {

    // The discord RPC client used for the rich presence
    // given the app id (see developer portal) and -1 as the pipe for automatic pipe scan
    private static DiscordRpcClient m_client = new DiscordRpcClient("770355757622755379", -1);

    // Determines if the presence has been initialized yet
    private static bool m_initialized = false;


    public static void SetPresenceServer(Server server)
    {
      if (!m_initialized)
      {
        m_client.Initialize();

        RichPresence rp = new RichPresence();
        rp.Timestamps = new Timestamps(DateTime.UtcNow);
        rp.State = "Currently playing on: " + server.ServerName;
        rp.Details = "Playing osu! on a 3rd-party server using the Ultimate Osu Server Switcher";
        rp.Assets = new Assets() { LargeImageKey = "uoss", LargeImageText = "Download on GitHub!\r\nminisbett/ultimate-osu-server-switcher" };
        m_client.SetPresence(rp);

        m_initialized = true;
      }
      else
      {
        m_client.UpdateState("Currently playing on: " + server.ServerName);
      }
    }

    public static void RemovePresence()
    {
      if (m_initialized)
        m_client.ClearPresence();
    }
  }
}