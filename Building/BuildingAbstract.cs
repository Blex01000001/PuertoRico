using PuertoRicoSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PuertoRicoSpace
{
    public abstract class BuildingAbstract
    {
        //[JsonInclude]
        public string Name { get; protected set; }
        public string Industry { get; protected set; }
        public string Type { get; protected set; }
        public string Scale { get; protected set; }
        public int Worker { get; protected set; }
        public int MaxWorker { get; protected set; }
        public int Cost { get; protected set; }
        public int Score { get; protected set; }
        public int Level { get; protected set; }
        public int Priority { get; protected set; }
        public int Occupy { get; protected set; }
        protected BuildingAbstract()
        {
            Worker = 0; // 初始工作人數為 0
            Priority = 10;
        }
        public void SetJson
        (
            string name = null,
            string industry = null,
            string type = null,
            string scale = null,
            int worker = 0,
            int maxworker = 0,
            int cost = 0,
            int score = 0,
            int level = 0,
            int priority = 10,
            int occupy = 1
        )
        {
            Name = name;
            Industry = industry;
            Type = type;
            Scale = scale;
            Worker = worker;
            MaxWorker = maxworker;
            Cost = cost;
            Score = score;
            Level = level;
            Priority = priority;
            Occupy = occupy;
        }
        public void SetPriority(int priority) => Priority = priority;
        public void ResetWorker() => Worker = 0;
        public void IncreaseWorker(int qty) => Worker += qty;
    }
    //5種大型特殊建築物（占2個建築物空間）各一
    //12種小型特殊建物（占1個建築物空間）各二
    //20個生產用途建築物
    /// <summary>
    /// 空建築，當作PASS
    /// </summary>
    public class PassBuilding : BuildingAbstract
    {
        public PassBuilding()
        {
            Name = "PassBuilding";
        }
    }

}
