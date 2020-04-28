/**
 * Autogenerated by Thrift Compiler (0.13.0)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using System.Runtime.Serialization;
using Thrift.Protocol;
using Thrift.Transport;

namespace Thrift
{
  public partial class ThriftObserver {
    public interface ISync {
      void seatsDecremented(Cursa cursa);
      void newBookings(List<Rezervare> rezervari);
    }

    public interface Iface : ISync {
      #if SILVERLIGHT
      IAsyncResult Begin_seatsDecremented(AsyncCallback callback, object state, Cursa cursa);
      void End_seatsDecremented(IAsyncResult asyncResult);
      #endif
      #if SILVERLIGHT
      IAsyncResult Begin_newBookings(AsyncCallback callback, object state, List<Rezervare> rezervari);
      void End_newBookings(IAsyncResult asyncResult);
      #endif
    }

    public class Client : IDisposable, Iface {
      public Client(TProtocol prot) : this(prot, prot)
      {
      }

      public Client(TProtocol iprot, TProtocol oprot)
      {
        iprot_ = iprot;
        oprot_ = oprot;
      }

      protected TProtocol iprot_;
      protected TProtocol oprot_;
      protected int seqid_;

      public TProtocol InputProtocol
      {
        get { return iprot_; }
      }
      public TProtocol OutputProtocol
      {
        get { return oprot_; }
      }


      #region " IDisposable Support "
      private bool _IsDisposed;

      // IDisposable
      public void Dispose()
      {
        Dispose(true);
      }
      

      protected virtual void Dispose(bool disposing)
      {
        if (!_IsDisposed)
        {
          if (disposing)
          {
            if (iprot_ != null)
            {
              ((IDisposable)iprot_).Dispose();
            }
            if (oprot_ != null)
            {
              ((IDisposable)oprot_).Dispose();
            }
          }
        }
        _IsDisposed = true;
      }
      #endregion


      
      #if SILVERLIGHT
      
      public IAsyncResult Begin_seatsDecremented(AsyncCallback callback, object state, Cursa cursa)
      {
        return send_seatsDecremented(callback, state, cursa);
      }

      public void End_seatsDecremented(IAsyncResult asyncResult)
      {
        oprot_.Transport.EndFlush(asyncResult);
        recv_seatsDecremented();
      }

      #endif

      public void seatsDecremented(Cursa cursa)
      {
        #if SILVERLIGHT
        var asyncResult = Begin_seatsDecremented(null, null, cursa);
        End_seatsDecremented(asyncResult);

        #else
        send_seatsDecremented(cursa);
        recv_seatsDecremented();

        #endif
      }
      #if SILVERLIGHT
      public IAsyncResult send_seatsDecremented(AsyncCallback callback, object state, Cursa cursa)
      {
        oprot_.WriteMessageBegin(new TMessage("seatsDecremented", TMessageType.Call, seqid_));
        seatsDecremented_args args = new seatsDecremented_args();
        args.Cursa = cursa;
        args.Write(oprot_);
        oprot_.WriteMessageEnd();
        return oprot_.Transport.BeginFlush(callback, state);
      }

      #else

      public void send_seatsDecremented(Cursa cursa)
      {
        oprot_.WriteMessageBegin(new TMessage("seatsDecremented", TMessageType.Call, seqid_));
        seatsDecremented_args args = new seatsDecremented_args();
        args.Cursa = cursa;
        args.Write(oprot_);
        oprot_.WriteMessageEnd();
        oprot_.Transport.Flush();
      }
      #endif

      public void recv_seatsDecremented()
      {
        TMessage msg = iprot_.ReadMessageBegin();
        if (msg.Type == TMessageType.Exception) {
          TApplicationException x = TApplicationException.Read(iprot_);
          iprot_.ReadMessageEnd();
          throw x;
        }
        seatsDecremented_result result = new seatsDecremented_result();
        result.Read(iprot_);
        iprot_.ReadMessageEnd();
        return;
      }

      
      #if SILVERLIGHT
      
      public IAsyncResult Begin_newBookings(AsyncCallback callback, object state, List<Rezervare> rezervari)
      {
        return send_newBookings(callback, state, rezervari);
      }

      public void End_newBookings(IAsyncResult asyncResult)
      {
        oprot_.Transport.EndFlush(asyncResult);
        recv_newBookings();
      }

      #endif

      public void newBookings(List<Rezervare> rezervari)
      {
        #if SILVERLIGHT
        var asyncResult = Begin_newBookings(null, null, rezervari);
        End_newBookings(asyncResult);

        #else
        send_newBookings(rezervari);
        recv_newBookings();

        #endif
      }
      #if SILVERLIGHT
      public IAsyncResult send_newBookings(AsyncCallback callback, object state, List<Rezervare> rezervari)
      {
        oprot_.WriteMessageBegin(new TMessage("newBookings", TMessageType.Call, seqid_));
        newBookings_args args = new newBookings_args();
        args.Rezervari = rezervari;
        args.Write(oprot_);
        oprot_.WriteMessageEnd();
        return oprot_.Transport.BeginFlush(callback, state);
      }

      #else

      public void send_newBookings(List<Rezervare> rezervari)
      {
        oprot_.WriteMessageBegin(new TMessage("newBookings", TMessageType.Call, seqid_));
        newBookings_args args = new newBookings_args();
        args.Rezervari = rezervari;
        args.Write(oprot_);
        oprot_.WriteMessageEnd();
        oprot_.Transport.Flush();
      }
      #endif

      public void recv_newBookings()
      {
        TMessage msg = iprot_.ReadMessageBegin();
        if (msg.Type == TMessageType.Exception) {
          TApplicationException x = TApplicationException.Read(iprot_);
          iprot_.ReadMessageEnd();
          throw x;
        }
        newBookings_result result = new newBookings_result();
        result.Read(iprot_);
        iprot_.ReadMessageEnd();
        return;
      }

    }
    public class Processor : TProcessor {
      public Processor(ISync iface)
      {
        iface_ = iface;
        processMap_["seatsDecremented"] = seatsDecremented_Process;
        processMap_["newBookings"] = newBookings_Process;
      }

      protected delegate void ProcessFunction(int seqid, TProtocol iprot, TProtocol oprot);
      private ISync iface_;
      protected Dictionary<string, ProcessFunction> processMap_ = new Dictionary<string, ProcessFunction>();

      public bool Process(TProtocol iprot, TProtocol oprot)
      {
        try
        {
          TMessage msg = iprot.ReadMessageBegin();
          ProcessFunction fn;
          processMap_.TryGetValue(msg.Name, out fn);
          if (fn == null) {
            TProtocolUtil.Skip(iprot, TType.Struct);
            iprot.ReadMessageEnd();
            TApplicationException x = new TApplicationException (TApplicationException.ExceptionType.UnknownMethod, "Invalid method name: '" + msg.Name + "'");
            oprot.WriteMessageBegin(new TMessage(msg.Name, TMessageType.Exception, msg.SeqID));
            x.Write(oprot);
            oprot.WriteMessageEnd();
            oprot.Transport.Flush();
            return true;
          }
          fn(msg.SeqID, iprot, oprot);
        }
        catch (IOException)
        {
          return false;
        }
        return true;
      }

      public void seatsDecremented_Process(int seqid, TProtocol iprot, TProtocol oprot)
      {
        seatsDecremented_args args = new seatsDecremented_args();
        args.Read(iprot);
        iprot.ReadMessageEnd();
        seatsDecremented_result result = new seatsDecremented_result();
        try
        {
          iface_.seatsDecremented(args.Cursa);
          oprot.WriteMessageBegin(new TMessage("seatsDecremented", TMessageType.Reply, seqid)); 
          result.Write(oprot);
        }
        catch (TTransportException)
        {
          throw;
        }
        catch (Exception ex)
        {
          Console.Error.WriteLine("Error occurred in processor:");
          Console.Error.WriteLine(ex.ToString());
          TApplicationException x = new TApplicationException        (TApplicationException.ExceptionType.InternalError," Internal error.");
          oprot.WriteMessageBegin(new TMessage("seatsDecremented", TMessageType.Exception, seqid));
          x.Write(oprot);
        }
        oprot.WriteMessageEnd();
        oprot.Transport.Flush();
      }

      public void newBookings_Process(int seqid, TProtocol iprot, TProtocol oprot)
      {
        newBookings_args args = new newBookings_args();
        args.Read(iprot);
        iprot.ReadMessageEnd();
        newBookings_result result = new newBookings_result();
        try
        {
          iface_.newBookings(args.Rezervari);
          oprot.WriteMessageBegin(new TMessage("newBookings", TMessageType.Reply, seqid)); 
          result.Write(oprot);
        }
        catch (TTransportException)
        {
          throw;
        }
        catch (Exception ex)
        {
          Console.Error.WriteLine("Error occurred in processor:");
          Console.Error.WriteLine(ex.ToString());
          TApplicationException x = new TApplicationException        (TApplicationException.ExceptionType.InternalError," Internal error.");
          oprot.WriteMessageBegin(new TMessage("newBookings", TMessageType.Exception, seqid));
          x.Write(oprot);
        }
        oprot.WriteMessageEnd();
        oprot.Transport.Flush();
      }

    }


    #if !SILVERLIGHT
    [Serializable]
    #endif
    public partial class seatsDecremented_args : TBase
    {
      private Cursa _cursa;

      public Cursa Cursa
      {
        get
        {
          return _cursa;
        }
        set
        {
          __isset.cursa = true;
          this._cursa = value;
        }
      }


      public Isset __isset;
      #if !SILVERLIGHT
      [Serializable]
      #endif
      public struct Isset {
        public bool cursa;
      }

      public seatsDecremented_args() {
      }

      public void Read (TProtocol iprot)
      {
        iprot.IncrementRecursionDepth();
        try
        {
          TField field;
          iprot.ReadStructBegin();
          while (true)
          {
            field = iprot.ReadFieldBegin();
            if (field.Type == TType.Stop) { 
              break;
            }
            switch (field.ID)
            {
              case 1:
                if (field.Type == TType.Struct) {
                  Cursa = new Cursa();
                  Cursa.Read(iprot);
                } else { 
                  TProtocolUtil.Skip(iprot, field.Type);
                }
                break;
              default: 
                TProtocolUtil.Skip(iprot, field.Type);
                break;
            }
            iprot.ReadFieldEnd();
          }
          iprot.ReadStructEnd();
        }
        finally
        {
          iprot.DecrementRecursionDepth();
        }
      }

      public void Write(TProtocol oprot) {
        oprot.IncrementRecursionDepth();
        try
        {
          TStruct struc = new TStruct("seatsDecremented_args");
          oprot.WriteStructBegin(struc);
          TField field = new TField();
          if (Cursa != null && __isset.cursa) {
            field.Name = "cursa";
            field.Type = TType.Struct;
            field.ID = 1;
            oprot.WriteFieldBegin(field);
            Cursa.Write(oprot);
            oprot.WriteFieldEnd();
          }
          oprot.WriteFieldStop();
          oprot.WriteStructEnd();
        }
        finally
        {
          oprot.DecrementRecursionDepth();
        }
      }

      public override string ToString() {
        StringBuilder __sb = new StringBuilder("seatsDecremented_args(");
        bool __first = true;
        if (Cursa != null && __isset.cursa) {
          if(!__first) { __sb.Append(", "); }
          __first = false;
          __sb.Append("Cursa: ");
          __sb.Append(Cursa== null ? "<null>" : Cursa.ToString());
        }
        __sb.Append(")");
        return __sb.ToString();
      }

    }


    #if !SILVERLIGHT
    [Serializable]
    #endif
    public partial class seatsDecremented_result : TBase
    {

      public seatsDecremented_result() {
      }

      public void Read (TProtocol iprot)
      {
        iprot.IncrementRecursionDepth();
        try
        {
          TField field;
          iprot.ReadStructBegin();
          while (true)
          {
            field = iprot.ReadFieldBegin();
            if (field.Type == TType.Stop) { 
              break;
            }
            switch (field.ID)
            {
              default: 
                TProtocolUtil.Skip(iprot, field.Type);
                break;
            }
            iprot.ReadFieldEnd();
          }
          iprot.ReadStructEnd();
        }
        finally
        {
          iprot.DecrementRecursionDepth();
        }
      }

      public void Write(TProtocol oprot) {
        oprot.IncrementRecursionDepth();
        try
        {
          TStruct struc = new TStruct("seatsDecremented_result");
          oprot.WriteStructBegin(struc);

          oprot.WriteFieldStop();
          oprot.WriteStructEnd();
        }
        finally
        {
          oprot.DecrementRecursionDepth();
        }
      }

      public override string ToString() {
        StringBuilder __sb = new StringBuilder("seatsDecremented_result(");
        __sb.Append(")");
        return __sb.ToString();
      }

    }


    #if !SILVERLIGHT
    [Serializable]
    #endif
    public partial class newBookings_args : TBase
    {
      private List<Rezervare> _rezervari;

      public List<Rezervare> Rezervari
      {
        get
        {
          return _rezervari;
        }
        set
        {
          __isset.rezervari = true;
          this._rezervari = value;
        }
      }


      public Isset __isset;
      #if !SILVERLIGHT
      [Serializable]
      #endif
      public struct Isset {
        public bool rezervari;
      }

      public newBookings_args() {
      }

      public void Read (TProtocol iprot)
      {
        iprot.IncrementRecursionDepth();
        try
        {
          TField field;
          iprot.ReadStructBegin();
          while (true)
          {
            field = iprot.ReadFieldBegin();
            if (field.Type == TType.Stop) { 
              break;
            }
            switch (field.ID)
            {
              case 1:
                if (field.Type == TType.List) {
                  {
                    Rezervari = new List<Rezervare>();
                    TList _list12 = iprot.ReadListBegin();
                    for( int _i13 = 0; _i13 < _list12.Count; ++_i13)
                    {
                      Rezervare _elem14;
                      _elem14 = new Rezervare();
                      _elem14.Read(iprot);
                      Rezervari.Add(_elem14);
                    }
                    iprot.ReadListEnd();
                  }
                } else { 
                  TProtocolUtil.Skip(iprot, field.Type);
                }
                break;
              default: 
                TProtocolUtil.Skip(iprot, field.Type);
                break;
            }
            iprot.ReadFieldEnd();
          }
          iprot.ReadStructEnd();
        }
        finally
        {
          iprot.DecrementRecursionDepth();
        }
      }

      public void Write(TProtocol oprot) {
        oprot.IncrementRecursionDepth();
        try
        {
          TStruct struc = new TStruct("newBookings_args");
          oprot.WriteStructBegin(struc);
          TField field = new TField();
          if (Rezervari != null && __isset.rezervari) {
            field.Name = "rezervari";
            field.Type = TType.List;
            field.ID = 1;
            oprot.WriteFieldBegin(field);
            {
              oprot.WriteListBegin(new TList(TType.Struct, Rezervari.Count));
              foreach (Rezervare _iter15 in Rezervari)
              {
                _iter15.Write(oprot);
              }
              oprot.WriteListEnd();
            }
            oprot.WriteFieldEnd();
          }
          oprot.WriteFieldStop();
          oprot.WriteStructEnd();
        }
        finally
        {
          oprot.DecrementRecursionDepth();
        }
      }

      public override string ToString() {
        StringBuilder __sb = new StringBuilder("newBookings_args(");
        bool __first = true;
        if (Rezervari != null && __isset.rezervari) {
          if(!__first) { __sb.Append(", "); }
          __first = false;
          __sb.Append("Rezervari: ");
          __sb.Append(Rezervari);
        }
        __sb.Append(")");
        return __sb.ToString();
      }

    }


    #if !SILVERLIGHT
    [Serializable]
    #endif
    public partial class newBookings_result : TBase
    {

      public newBookings_result() {
      }

      public void Read (TProtocol iprot)
      {
        iprot.IncrementRecursionDepth();
        try
        {
          TField field;
          iprot.ReadStructBegin();
          while (true)
          {
            field = iprot.ReadFieldBegin();
            if (field.Type == TType.Stop) { 
              break;
            }
            switch (field.ID)
            {
              default: 
                TProtocolUtil.Skip(iprot, field.Type);
                break;
            }
            iprot.ReadFieldEnd();
          }
          iprot.ReadStructEnd();
        }
        finally
        {
          iprot.DecrementRecursionDepth();
        }
      }

      public void Write(TProtocol oprot) {
        oprot.IncrementRecursionDepth();
        try
        {
          TStruct struc = new TStruct("newBookings_result");
          oprot.WriteStructBegin(struc);

          oprot.WriteFieldStop();
          oprot.WriteStructEnd();
        }
        finally
        {
          oprot.DecrementRecursionDepth();
        }
      }

      public override string ToString() {
        StringBuilder __sb = new StringBuilder("newBookings_result(");
        __sb.Append(")");
        return __sb.ToString();
      }

    }

  }
}
