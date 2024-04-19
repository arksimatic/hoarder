using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoarder.Scripts
{
    public partial class SignalManager : Node2D
    {
        /// <summary>
        /// CallingObjectPath - path to the object that emits the signal
        /// RecivingObjectPath - path to the object that receives the signal
        /// SignalName - name of the signal
        /// 
        /// Prerequisite:
        /// CallingObject must have Signal called SignalNameEventHandler
        /// RecievingObject must have a method called CallingObjectName_SignalName
        /// </summary>
        internal class PreSignal
        {
            public String CallingObjectPath { get; set; }
            public String RecivingObjectPath { get; set; }
            public String SignalName { get; set; }
        }

        public override void _Ready()
        {
            List<PreSignal> preSignals = new List<PreSignal>
            {
                new PreSignal
                {
                    CallingObjectPath = "Player/EQ",
                    RecivingObjectPath = "MapItems",
                    SignalName = "Test"
                }
            };

            foreach (PreSignal preSignal in preSignals)
            {
                ConnectSignal(preSignal);
            }
        }

        private void ConnectSignal(PreSignal preSignal)
        {
            Node callingNode = GetParent().GetNode(preSignal.CallingObjectPath);
            Node recivingNode = GetParent().GetNode(preSignal.RecivingObjectPath);
            Callable callable = new Callable(recivingNode, $"{callingNode.Name}_{preSignal.SignalName}");
            callingNode.Connect(preSignal.SignalName, callable);
        }
    }
}
