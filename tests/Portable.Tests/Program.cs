using System;
using System.Reflection;
class Program {
 static int count;
 static void Check(bool value,string name) { if(!value) throw new Exception(name); count++; }
 static object Call(object target,string name,params object[] args) { return target.GetType().GetMethod(name,BindingFlags.Instance|BindingFlags.NonPublic).Invoke(target,args); }
 static void Main() { Test(); Console.WriteLine("PASS " + count + " source assertions"); }
 static void Test() { var config=new ARDrone2Client.Common.Configuration.DroneConfiguration(); var item=new Item();config.Items.Add(item.Key,item);
 Check(ARDrone2Client.Common.Configuration.ConfigurationPacketParser.TryUpdateLine(config,"general:name = test"),"valid line updates"); Check((string)item.Value=="test","value captured");
 foreach(var line in new string[]{null,"","garbage","unknown:key = x","general:name = reject"}) Check(!ARDrone2Client.Common.Configuration.ConfigurationPacketParser.TryUpdateLine(config,line),"invalid/unknown/rejected line");
 var p=new ARDrone2Client.Common.Configuration.ConfigurationPacket(); Check(!ARDrone2Client.Common.Configuration.ConfigurationPacketParser.TryUpdate(config,p),"null packet rejected");Check(!ARDrone2Client.Common.Configuration.ConfigurationPacketParser.TryUpdate2(config,p),"null packet2 rejected");
 p.Data=System.Text.Encoding.UTF8.GetBytes("unknown:key = x\ngeneral:name = final\n");Check(ARDrone2Client.Common.Configuration.ConfigurationPacketParser.TryUpdate2(config,p),"multiline updates");Check((string)item.Value=="final","multiline value");
 p.Data=new byte[0];Check(!ARDrone2Client.Common.Configuration.ConfigurationPacketParser.TryUpdate2(config,p),"empty packet not updated");
 } }