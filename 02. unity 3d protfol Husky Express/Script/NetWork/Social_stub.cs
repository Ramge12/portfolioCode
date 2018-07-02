﻿




// Generated by PIDL compiler.
// Do not modify this file, but modify the source .pidl file.

using System;
using System.Net;	     

namespace SocialC25
{
	internal class Stub:Nettention.Proud.RmiStub
	{
public AfterRmiInvocationDelegate AfterRmiInvocation = delegate(Nettention.Proud.AfterRmiSummary summary) {};
public BeforeRmiInvocationDelegate BeforeRmiInvocation = delegate(Nettention.Proud.BeforeRmiSummary summary) {};

		public delegate bool RequestLogonDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, String villeName, bool isNewVille);  
		public RequestLogonDelegate RequestLogon = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, String villeName, bool isNewVille)
		{ 
			return false;
		};
		public delegate bool RequestAddTreeDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, UnityEngine.Vector3 position);  
		public RequestAddTreeDelegate RequestAddTree = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, UnityEngine.Vector3 position)
		{ 
			return false;
		};
		public delegate bool RequestRemoveTreeDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int treeID);  
		public RequestRemoveTreeDelegate RequestRemoveTree = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int treeID)
		{ 
			return false;
		};
	public override bool ProcessReceivedMessage(Nettention.Proud.ReceivedMessage pa, Object hostTag) 
	{
		Nettention.Proud.HostID remote=pa.RemoteHostID;
		if(remote==Nettention.Proud.HostID.HostID_None)
		{
			ShowUnknownHostIDWarning(remote);
		}

		Nettention.Proud.Message __msg=pa.ReadOnlyMessage;
		int orgReadOffset = __msg.ReadOffset;
        Nettention.Proud.RmiID __rmiID = Nettention.Proud.RmiID.RmiID_None;
        if (!__msg.Read( out __rmiID))
            goto __fail;
					
		switch(__rmiID)
		{
        case Common.RequestLogon:
            ProcessReceivedMessage_RequestLogon(__msg, pa, hostTag, remote);
            break;
        case Common.RequestAddTree:
            ProcessReceivedMessage_RequestAddTree(__msg, pa, hostTag, remote);
            break;
        case Common.RequestRemoveTree:
            ProcessReceivedMessage_RequestRemoveTree(__msg, pa, hostTag, remote);
            break;
		default:
			 goto __fail;
		}
		return true;
__fail:
	  {
			__msg.ReadOffset = orgReadOffset;
			return false;
	  }
	}
    void ProcessReceivedMessage_RequestLogon(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        String villeName; SngClient.Marshaler.Read(__msg,out villeName);	
bool isNewVille; SngClient.Marshaler.Read(__msg,out isNewVille);	
core.PostCheckReadMessage(__msg, RmiName_RequestLogon);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=villeName.ToString()+",";
parameterString+=isNewVille.ToString()+",";
        NotifyCallFromStub(Common.RequestLogon, RmiName_RequestLogon,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.RequestLogon;
        summary.rmiName = RmiName_RequestLogon;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =RequestLogon (remote,ctx , villeName, isNewVille );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_RequestLogon);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.RequestLogon;
        summary.rmiName = RmiName_RequestLogon;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_RequestAddTree(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        UnityEngine.Vector3 position; SngClient.Marshaler.Read(__msg,out position);	
core.PostCheckReadMessage(__msg, RmiName_RequestAddTree);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=position.ToString()+",";
        NotifyCallFromStub(Common.RequestAddTree, RmiName_RequestAddTree,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.RequestAddTree;
        summary.rmiName = RmiName_RequestAddTree;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =RequestAddTree (remote,ctx , position );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_RequestAddTree);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.RequestAddTree;
        summary.rmiName = RmiName_RequestAddTree;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_RequestRemoveTree(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        int treeID; SngClient.Marshaler.Read(__msg,out treeID);	
core.PostCheckReadMessage(__msg, RmiName_RequestRemoveTree);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=treeID.ToString()+",";
        NotifyCallFromStub(Common.RequestRemoveTree, RmiName_RequestRemoveTree,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.RequestRemoveTree;
        summary.rmiName = RmiName_RequestRemoveTree;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =RequestRemoveTree (remote,ctx , treeID );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_RequestRemoveTree);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.RequestRemoveTree;
        summary.rmiName = RmiName_RequestRemoveTree;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
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
		public override Nettention.Proud.RmiID[] GetRmiIDList { get{return Common.RmiIDList;} }
		
	}
}
namespace SocailS2C
{
	internal class Stub:Nettention.Proud.RmiStub
	{
public AfterRmiInvocationDelegate AfterRmiInvocation = delegate(Nettention.Proud.AfterRmiSummary summary) {};
public BeforeRmiInvocationDelegate BeforeRmiInvocation = delegate(Nettention.Proud.BeforeRmiSummary summary) {};

		public delegate bool ReplyLogonDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int result, String comment);  
		public ReplyLogonDelegate ReplyLogon = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int result, String comment)
		{ 
			return false;
		};
		public delegate bool NotifyAddTreeDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int treeID, UnityEngine.Vector3 position);  
		public NotifyAddTreeDelegate NotifyAddTree = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int treeID, UnityEngine.Vector3 position)
		{ 
			return false;
		};
		public delegate bool NotifyRemoveTreeDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int treeID);  
		public NotifyRemoveTreeDelegate NotifyRemoveTree = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int treeID)
		{ 
			return false;
		};
	public override bool ProcessReceivedMessage(Nettention.Proud.ReceivedMessage pa, Object hostTag) 
	{
		Nettention.Proud.HostID remote=pa.RemoteHostID;
		if(remote==Nettention.Proud.HostID.HostID_None)
		{
			ShowUnknownHostIDWarning(remote);
		}

		Nettention.Proud.Message __msg=pa.ReadOnlyMessage;
		int orgReadOffset = __msg.ReadOffset;
        Nettention.Proud.RmiID __rmiID = Nettention.Proud.RmiID.RmiID_None;
        if (!__msg.Read( out __rmiID))
            goto __fail;
					
		switch(__rmiID)
		{
        case Common.ReplyLogon:
            ProcessReceivedMessage_ReplyLogon(__msg, pa, hostTag, remote);
            break;
        case Common.NotifyAddTree:
            ProcessReceivedMessage_NotifyAddTree(__msg, pa, hostTag, remote);
            break;
        case Common.NotifyRemoveTree:
            ProcessReceivedMessage_NotifyRemoveTree(__msg, pa, hostTag, remote);
            break;
		default:
			 goto __fail;
		}
		return true;
__fail:
	  {
			__msg.ReadOffset = orgReadOffset;
			return false;
	  }
	}
    void ProcessReceivedMessage_ReplyLogon(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        int result; SngClient.Marshaler.Read(__msg,out result);	
String comment; SngClient.Marshaler.Read(__msg,out comment);	
core.PostCheckReadMessage(__msg, RmiName_ReplyLogon);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=result.ToString()+",";
parameterString+=comment.ToString()+",";
        NotifyCallFromStub(Common.ReplyLogon, RmiName_ReplyLogon,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.ReplyLogon;
        summary.rmiName = RmiName_ReplyLogon;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =ReplyLogon (remote,ctx , result, comment );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_ReplyLogon);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.ReplyLogon;
        summary.rmiName = RmiName_ReplyLogon;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_NotifyAddTree(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        int treeID; SngClient.Marshaler.Read(__msg,out treeID);	
UnityEngine.Vector3 position; SngClient.Marshaler.Read(__msg,out position);	
core.PostCheckReadMessage(__msg, RmiName_NotifyAddTree);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=treeID.ToString()+",";
parameterString+=position.ToString()+",";
        NotifyCallFromStub(Common.NotifyAddTree, RmiName_NotifyAddTree,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.NotifyAddTree;
        summary.rmiName = RmiName_NotifyAddTree;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =NotifyAddTree (remote,ctx , treeID, position );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_NotifyAddTree);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.NotifyAddTree;
        summary.rmiName = RmiName_NotifyAddTree;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_NotifyRemoveTree(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        int treeID; SngClient.Marshaler.Read(__msg,out treeID);	
core.PostCheckReadMessage(__msg, RmiName_NotifyRemoveTree);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=treeID.ToString()+",";
        NotifyCallFromStub(Common.NotifyRemoveTree, RmiName_NotifyRemoveTree,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.NotifyRemoveTree;
        summary.rmiName = RmiName_NotifyRemoveTree;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =NotifyRemoveTree (remote,ctx , treeID );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_NotifyRemoveTree);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.NotifyRemoveTree;
        summary.rmiName = RmiName_NotifyRemoveTree;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
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
		public override Nettention.Proud.RmiID[] GetRmiIDList { get{return Common.RmiIDList;} }
		
	}
}
namespace SocialC2C
{
	internal class Stub:Nettention.Proud.RmiStub
	{
public AfterRmiInvocationDelegate AfterRmiInvocation = delegate(Nettention.Proud.AfterRmiSummary summary) {};
public BeforeRmiInvocationDelegate BeforeRmiInvocation = delegate(Nettention.Proud.BeforeRmiSummary summary) {};

		public delegate bool ScribblePointDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, UnityEngine.Vector3 point);  
		public ScribblePointDelegate ScribblePoint = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, UnityEngine.Vector3 point)
		{ 
			return false;
		};
		public delegate bool JoyStick_Horizontal_PS1Delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, float Horizontal);  
		public JoyStick_Horizontal_PS1Delegate JoyStick_Horizontal_PS1 = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, float Horizontal)
		{ 
			return false;
		};
		public delegate bool JoyStick_Vertical_PS1Delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, float Vertical);  
		public JoyStick_Vertical_PS1Delegate JoyStick_Vertical_PS1 = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, float Vertical)
		{ 
			return false;
		};
		public delegate bool JoyStick_Horizontal_PS2Delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, float Horizontal);  
		public JoyStick_Horizontal_PS2Delegate JoyStick_Horizontal_PS2 = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, float Horizontal)
		{ 
			return false;
		};
		public delegate bool JoyStick_Vertical_PS2Delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, float Vertical);  
		public JoyStick_Vertical_PS2Delegate JoyStick_Vertical_PS2 = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, float Vertical)
		{ 
			return false;
		};
		public delegate bool Player_1PointDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, UnityEngine.Vector3 point, float rotation, int run);  
		public Player_1PointDelegate Player_1Point = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, UnityEngine.Vector3 point, float rotation, int run)
		{ 
			return false;
		};
		public delegate bool Player_2PointDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, UnityEngine.Vector3 point, float rotation, int run);  
		public Player_2PointDelegate Player_2Point = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, UnityEngine.Vector3 point, float rotation, int run)
		{ 
			return false;
		};
	public override bool ProcessReceivedMessage(Nettention.Proud.ReceivedMessage pa, Object hostTag) 
	{
		Nettention.Proud.HostID remote=pa.RemoteHostID;
		if(remote==Nettention.Proud.HostID.HostID_None)
		{
			ShowUnknownHostIDWarning(remote);
		}

		Nettention.Proud.Message __msg=pa.ReadOnlyMessage;
		int orgReadOffset = __msg.ReadOffset;
        Nettention.Proud.RmiID __rmiID = Nettention.Proud.RmiID.RmiID_None;
        if (!__msg.Read( out __rmiID))
            goto __fail;
					
		switch(__rmiID)
		{
        case Common.ScribblePoint:
            ProcessReceivedMessage_ScribblePoint(__msg, pa, hostTag, remote);
            break;
        case Common.JoyStick_Horizontal_PS1:
            ProcessReceivedMessage_JoyStick_Horizontal_PS1(__msg, pa, hostTag, remote);
            break;
        case Common.JoyStick_Vertical_PS1:
            ProcessReceivedMessage_JoyStick_Vertical_PS1(__msg, pa, hostTag, remote);
            break;
        case Common.JoyStick_Horizontal_PS2:
            ProcessReceivedMessage_JoyStick_Horizontal_PS2(__msg, pa, hostTag, remote);
            break;
        case Common.JoyStick_Vertical_PS2:
            ProcessReceivedMessage_JoyStick_Vertical_PS2(__msg, pa, hostTag, remote);
            break;
        case Common.Player_1Point:
            ProcessReceivedMessage_Player_1Point(__msg, pa, hostTag, remote);
            break;
        case Common.Player_2Point:
            ProcessReceivedMessage_Player_2Point(__msg, pa, hostTag, remote);
            break;
		default:
			 goto __fail;
		}
		return true;
__fail:
	  {
			__msg.ReadOffset = orgReadOffset;
			return false;
	  }
	}
    void ProcessReceivedMessage_ScribblePoint(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        UnityEngine.Vector3 point; SngClient.Marshaler.Read(__msg,out point);	
core.PostCheckReadMessage(__msg, RmiName_ScribblePoint);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=point.ToString()+",";
        NotifyCallFromStub(Common.ScribblePoint, RmiName_ScribblePoint,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.ScribblePoint;
        summary.rmiName = RmiName_ScribblePoint;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =ScribblePoint (remote,ctx , point );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_ScribblePoint);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.ScribblePoint;
        summary.rmiName = RmiName_ScribblePoint;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_JoyStick_Horizontal_PS1(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        float Horizontal; SngClient.Marshaler.Read(__msg,out Horizontal);	
core.PostCheckReadMessage(__msg, RmiName_JoyStick_Horizontal_PS1);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=Horizontal.ToString()+",";
        NotifyCallFromStub(Common.JoyStick_Horizontal_PS1, RmiName_JoyStick_Horizontal_PS1,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.JoyStick_Horizontal_PS1;
        summary.rmiName = RmiName_JoyStick_Horizontal_PS1;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =JoyStick_Horizontal_PS1 (remote,ctx , Horizontal );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_JoyStick_Horizontal_PS1);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.JoyStick_Horizontal_PS1;
        summary.rmiName = RmiName_JoyStick_Horizontal_PS1;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_JoyStick_Vertical_PS1(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        float Vertical; SngClient.Marshaler.Read(__msg,out Vertical);	
core.PostCheckReadMessage(__msg, RmiName_JoyStick_Vertical_PS1);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=Vertical.ToString()+",";
        NotifyCallFromStub(Common.JoyStick_Vertical_PS1, RmiName_JoyStick_Vertical_PS1,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.JoyStick_Vertical_PS1;
        summary.rmiName = RmiName_JoyStick_Vertical_PS1;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =JoyStick_Vertical_PS1 (remote,ctx , Vertical );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_JoyStick_Vertical_PS1);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.JoyStick_Vertical_PS1;
        summary.rmiName = RmiName_JoyStick_Vertical_PS1;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_JoyStick_Horizontal_PS2(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        float Horizontal; SngClient.Marshaler.Read(__msg,out Horizontal);	
core.PostCheckReadMessage(__msg, RmiName_JoyStick_Horizontal_PS2);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=Horizontal.ToString()+",";
        NotifyCallFromStub(Common.JoyStick_Horizontal_PS2, RmiName_JoyStick_Horizontal_PS2,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.JoyStick_Horizontal_PS2;
        summary.rmiName = RmiName_JoyStick_Horizontal_PS2;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =JoyStick_Horizontal_PS2 (remote,ctx , Horizontal );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_JoyStick_Horizontal_PS2);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.JoyStick_Horizontal_PS2;
        summary.rmiName = RmiName_JoyStick_Horizontal_PS2;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_JoyStick_Vertical_PS2(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        float Vertical; SngClient.Marshaler.Read(__msg,out Vertical);	
core.PostCheckReadMessage(__msg, RmiName_JoyStick_Vertical_PS2);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=Vertical.ToString()+",";
        NotifyCallFromStub(Common.JoyStick_Vertical_PS2, RmiName_JoyStick_Vertical_PS2,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.JoyStick_Vertical_PS2;
        summary.rmiName = RmiName_JoyStick_Vertical_PS2;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =JoyStick_Vertical_PS2 (remote,ctx , Vertical );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_JoyStick_Vertical_PS2);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.JoyStick_Vertical_PS2;
        summary.rmiName = RmiName_JoyStick_Vertical_PS2;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_Player_1Point(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        UnityEngine.Vector3 point; SngClient.Marshaler.Read(__msg,out point);	
float rotation; SngClient.Marshaler.Read(__msg,out rotation);	
int run; SngClient.Marshaler.Read(__msg,out run);	
core.PostCheckReadMessage(__msg, RmiName_Player_1Point);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=point.ToString()+",";
parameterString+=rotation.ToString()+",";
parameterString+=run.ToString()+",";
        NotifyCallFromStub(Common.Player_1Point, RmiName_Player_1Point,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.Player_1Point;
        summary.rmiName = RmiName_Player_1Point;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =Player_1Point (remote,ctx , point, rotation, run );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_Player_1Point);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.Player_1Point;
        summary.rmiName = RmiName_Player_1Point;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_Player_2Point(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        UnityEngine.Vector3 point; SngClient.Marshaler.Read(__msg,out point);	
float rotation; SngClient.Marshaler.Read(__msg,out rotation);	
int run; SngClient.Marshaler.Read(__msg,out run);	
core.PostCheckReadMessage(__msg, RmiName_Player_2Point);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=point.ToString()+",";
parameterString+=rotation.ToString()+",";
parameterString+=run.ToString()+",";
        NotifyCallFromStub(Common.Player_2Point, RmiName_Player_2Point,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.Player_2Point;
        summary.rmiName = RmiName_Player_2Point;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =Player_2Point (remote,ctx , point, rotation, run );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_Player_2Point);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.Player_2Point;
        summary.rmiName = RmiName_Player_2Point;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
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
		public override Nettention.Proud.RmiID[] GetRmiIDList { get{return Common.RmiIDList;} }
		
	}
}
