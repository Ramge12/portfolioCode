﻿




// Generated by PIDL compiler.
// Do not modify this file, but modify the source .pidl file.

using System;
using System.Net;

namespace SocialC25
{
	internal class Proxy:Nettention.Proud.RmiProxy
	{
public bool RequestLogon(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, String villeName, bool isNewVille)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.RequestLogon;
		__msg.Write(__msgid);
		SngClient.Marshaler.Write(__msg, villeName);
		SngClient.Marshaler.Write(__msg, isNewVille);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_RequestLogon, Common.RequestLogon);
}

public bool RequestLogon(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, String villeName, bool isNewVille)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.RequestLogon;
__msg.Write(__msgid);
SngClient.Marshaler.Write(__msg, villeName);
SngClient.Marshaler.Write(__msg, isNewVille);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_RequestLogon, Common.RequestLogon);
}
public bool RequestAddTree(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, UnityEngine.Vector3 position)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.RequestAddTree;
		__msg.Write(__msgid);
		SngClient.Marshaler.Write(__msg, position);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_RequestAddTree, Common.RequestAddTree);
}

public bool RequestAddTree(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, UnityEngine.Vector3 position)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.RequestAddTree;
__msg.Write(__msgid);
SngClient.Marshaler.Write(__msg, position);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_RequestAddTree, Common.RequestAddTree);
}
public bool RequestRemoveTree(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int treeID)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.RequestRemoveTree;
		__msg.Write(__msgid);
		SngClient.Marshaler.Write(__msg, treeID);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_RequestRemoveTree, Common.RequestRemoveTree);
}

public bool RequestRemoveTree(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, int treeID)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.RequestRemoveTree;
__msg.Write(__msgid);
SngClient.Marshaler.Write(__msg, treeID);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_RequestRemoveTree, Common.RequestRemoveTree);
}
#if USE_RMI_NAME_STRING
// RMI name declaration.
// It is the unique pointer that indicates RMI name such as RMI profiler.
public const string RmiName_RequestLogon="RequestLogon";
public const string RmiName_RequestAddTree="RequestAddTree";
public const string RmiName_RequestRemoveTree="RequestRemoveTree";
       
public const string RmiName_First = RmiName_RequestLogon;
#else
// RMI name declaration.
// It is the unique pointer that indicates RMI name such as RMI profiler.
public const string RmiName_RequestLogon="";
public const string RmiName_RequestAddTree="";
public const string RmiName_RequestRemoveTree="";
       
public const string RmiName_First = "";
#endif
		public override Nettention.Proud.RmiID[] GetRmiIDList() { return Common.RmiIDList; } 
	}
}
namespace SocailS2C
{
	internal class Proxy:Nettention.Proud.RmiProxy
	{
public bool ReplyLogon(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int result, String comment)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.ReplyLogon;
		__msg.Write(__msgid);
		SngClient.Marshaler.Write(__msg, result);
		SngClient.Marshaler.Write(__msg, comment);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_ReplyLogon, Common.ReplyLogon);
}

public bool ReplyLogon(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, int result, String comment)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.ReplyLogon;
__msg.Write(__msgid);
SngClient.Marshaler.Write(__msg, result);
SngClient.Marshaler.Write(__msg, comment);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_ReplyLogon, Common.ReplyLogon);
}
public bool NotifyAddTree(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int treeID, UnityEngine.Vector3 position)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.NotifyAddTree;
		__msg.Write(__msgid);
		SngClient.Marshaler.Write(__msg, treeID);
		SngClient.Marshaler.Write(__msg, position);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_NotifyAddTree, Common.NotifyAddTree);
}

public bool NotifyAddTree(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, int treeID, UnityEngine.Vector3 position)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.NotifyAddTree;
__msg.Write(__msgid);
SngClient.Marshaler.Write(__msg, treeID);
SngClient.Marshaler.Write(__msg, position);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_NotifyAddTree, Common.NotifyAddTree);
}
public bool NotifyRemoveTree(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int treeID)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.NotifyRemoveTree;
		__msg.Write(__msgid);
		SngClient.Marshaler.Write(__msg, treeID);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_NotifyRemoveTree, Common.NotifyRemoveTree);
}

public bool NotifyRemoveTree(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, int treeID)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.NotifyRemoveTree;
__msg.Write(__msgid);
SngClient.Marshaler.Write(__msg, treeID);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_NotifyRemoveTree, Common.NotifyRemoveTree);
}
#if USE_RMI_NAME_STRING
// RMI name declaration.
// It is the unique pointer that indicates RMI name such as RMI profiler.
public const string RmiName_ReplyLogon="ReplyLogon";
public const string RmiName_NotifyAddTree="NotifyAddTree";
public const string RmiName_NotifyRemoveTree="NotifyRemoveTree";
       
public const string RmiName_First = RmiName_ReplyLogon;
#else
// RMI name declaration.
// It is the unique pointer that indicates RMI name such as RMI profiler.
public const string RmiName_ReplyLogon="";
public const string RmiName_NotifyAddTree="";
public const string RmiName_NotifyRemoveTree="";
       
public const string RmiName_First = "";
#endif
		public override Nettention.Proud.RmiID[] GetRmiIDList() { return Common.RmiIDList; } 
	}
}
namespace SocialC2C
{
	internal class Proxy:Nettention.Proud.RmiProxy
	{
public bool ScribblePoint(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, UnityEngine.Vector3 point)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.ScribblePoint;
		__msg.Write(__msgid);
		SngClient.Marshaler.Write(__msg, point);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_ScribblePoint, Common.ScribblePoint);
}

public bool ScribblePoint(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, UnityEngine.Vector3 point)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.ScribblePoint;
__msg.Write(__msgid);
SngClient.Marshaler.Write(__msg, point);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_ScribblePoint, Common.ScribblePoint);
}
public bool JoyStick_Horizontal_PS1(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, float Horizontal)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.JoyStick_Horizontal_PS1;
		__msg.Write(__msgid);
		SngClient.Marshaler.Write(__msg, Horizontal);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_JoyStick_Horizontal_PS1, Common.JoyStick_Horizontal_PS1);
}

public bool JoyStick_Horizontal_PS1(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, float Horizontal)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.JoyStick_Horizontal_PS1;
__msg.Write(__msgid);
SngClient.Marshaler.Write(__msg, Horizontal);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_JoyStick_Horizontal_PS1, Common.JoyStick_Horizontal_PS1);
}
public bool JoyStick_Vertical_PS1(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, float Vertical)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.JoyStick_Vertical_PS1;
		__msg.Write(__msgid);
		SngClient.Marshaler.Write(__msg, Vertical);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_JoyStick_Vertical_PS1, Common.JoyStick_Vertical_PS1);
}

public bool JoyStick_Vertical_PS1(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, float Vertical)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.JoyStick_Vertical_PS1;
__msg.Write(__msgid);
SngClient.Marshaler.Write(__msg, Vertical);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_JoyStick_Vertical_PS1, Common.JoyStick_Vertical_PS1);
}
public bool JoyStick_Horizontal_PS2(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, float Horizontal)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.JoyStick_Horizontal_PS2;
		__msg.Write(__msgid);
		SngClient.Marshaler.Write(__msg, Horizontal);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_JoyStick_Horizontal_PS2, Common.JoyStick_Horizontal_PS2);
}

public bool JoyStick_Horizontal_PS2(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, float Horizontal)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.JoyStick_Horizontal_PS2;
__msg.Write(__msgid);
SngClient.Marshaler.Write(__msg, Horizontal);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_JoyStick_Horizontal_PS2, Common.JoyStick_Horizontal_PS2);
}
public bool JoyStick_Vertical_PS2(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, float Vertical)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.JoyStick_Vertical_PS2;
		__msg.Write(__msgid);
		SngClient.Marshaler.Write(__msg, Vertical);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_JoyStick_Vertical_PS2, Common.JoyStick_Vertical_PS2);
}

public bool JoyStick_Vertical_PS2(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, float Vertical)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.JoyStick_Vertical_PS2;
__msg.Write(__msgid);
SngClient.Marshaler.Write(__msg, Vertical);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_JoyStick_Vertical_PS2, Common.JoyStick_Vertical_PS2);
}
public bool Player_1Point(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, UnityEngine.Vector3 point, float rotation, int run)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.Player_1Point;
		__msg.Write(__msgid);
		SngClient.Marshaler.Write(__msg, point);
		SngClient.Marshaler.Write(__msg, rotation);
		SngClient.Marshaler.Write(__msg, run);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_Player_1Point, Common.Player_1Point);
}

public bool Player_1Point(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, UnityEngine.Vector3 point, float rotation, int run)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.Player_1Point;
__msg.Write(__msgid);
SngClient.Marshaler.Write(__msg, point);
SngClient.Marshaler.Write(__msg, rotation);
SngClient.Marshaler.Write(__msg, run);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_Player_1Point, Common.Player_1Point);
}
public bool Player_2Point(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, UnityEngine.Vector3 point, float rotation, int run)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.Player_2Point;
		__msg.Write(__msgid);
		SngClient.Marshaler.Write(__msg, point);
		SngClient.Marshaler.Write(__msg, rotation);
		SngClient.Marshaler.Write(__msg, run);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_Player_2Point, Common.Player_2Point);
}

public bool Player_2Point(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, UnityEngine.Vector3 point, float rotation, int run)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.Player_2Point;
__msg.Write(__msgid);
SngClient.Marshaler.Write(__msg, point);
SngClient.Marshaler.Write(__msg, rotation);
SngClient.Marshaler.Write(__msg, run);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_Player_2Point, Common.Player_2Point);
}
#if USE_RMI_NAME_STRING
// RMI name declaration.
// It is the unique pointer that indicates RMI name such as RMI profiler.
public const string RmiName_ScribblePoint="ScribblePoint";
public const string RmiName_JoyStick_Horizontal_PS1="JoyStick_Horizontal_PS1";
public const string RmiName_JoyStick_Vertical_PS1="JoyStick_Vertical_PS1";
public const string RmiName_JoyStick_Horizontal_PS2="JoyStick_Horizontal_PS2";
public const string RmiName_JoyStick_Vertical_PS2="JoyStick_Vertical_PS2";
public const string RmiName_Player_1Point="Player_1Point";
public const string RmiName_Player_2Point="Player_2Point";
       
public const string RmiName_First = RmiName_ScribblePoint;
#else
// RMI name declaration.
// It is the unique pointer that indicates RMI name such as RMI profiler.
public const string RmiName_ScribblePoint="";
public const string RmiName_JoyStick_Horizontal_PS1="";
public const string RmiName_JoyStick_Vertical_PS1="";
public const string RmiName_JoyStick_Horizontal_PS2="";
public const string RmiName_JoyStick_Vertical_PS2="";
public const string RmiName_Player_1Point="";
public const string RmiName_Player_2Point="";
       
public const string RmiName_First = "";
#endif
		public override Nettention.Proud.RmiID[] GetRmiIDList() { return Common.RmiIDList; } 
	}
}

