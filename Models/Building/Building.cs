using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiniProjekt
{
    public abstract class Building
    {
        public string Name { get; set; }
        public virtual int Level { get; set; }

        public override string ToString()
        {
            return $"Name: {Name};" +
                $"Level: {Level};";
        }
    }
}
