using NetFwTypeLib;
using System;

namespace jlab.FileTransferServer.libs
{
    public static class FirewallHelper
    {
        public static bool IsPortOpen(int port, NET_FW_IP_PROTOCOL_ protocol)
        {
            try
            {
                INetFwOpenPorts ports;
                Type netFwMgrType = Type.GetTypeFromProgID("HNetCfg.FwMgr", false);
                INetFwMgr mgr = (INetFwMgr)Activator.CreateInstance(netFwMgrType);
                ports = (INetFwOpenPorts)mgr.LocalPolicy.CurrentProfile.GloballyOpenPorts;
                return ports.Item(port, protocol).Enabled;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static void AddPort(int port, string applicationName = "MyApplication", NET_FW_IP_PROTOCOL_ protocol = NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY)
        {
            if (!IsPortOpen(port, protocol))
            {
                Type netFwMgrType = Type.GetTypeFromProgID("HNetCfg.FwMgr", false);
                INetFwMgr mgr = (INetFwMgr)Activator.CreateInstance(netFwMgrType);
                INetFwOpenPorts ports = (INetFwOpenPorts)mgr.LocalPolicy.CurrentProfile.GloballyOpenPorts;
                Type portType = Type.GetTypeFromProgID("HNetCfg.FwOpenPort", false);
                INetFwOpenPort iport = (INetFwOpenPort) Activator.CreateInstance(portType);
                iport.Port = port;
                iport.Name = applicationName;
                iport.Enabled = true;
                ports.Add(iport);
            }
        }

        public static void RemovePort(int port, NET_FW_IP_PROTOCOL_ protocol, string applicationName = "MyApplication")
        {
            INetFwOpenPorts ports;
            Type NetFwMgrType = Type.GetTypeFromProgID("HNetCfg.FwMgr", false);
            INetFwMgr mgr = (INetFwMgr)Activator.CreateInstance(NetFwMgrType);
            ports = (INetFwOpenPorts)mgr.LocalPolicy.CurrentProfile.GloballyOpenPorts;
            ports.Remove(port, protocol);

        }
    }
}