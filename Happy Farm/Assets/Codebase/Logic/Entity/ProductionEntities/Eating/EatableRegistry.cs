using System.Collections.Generic;

namespace Codebase.Logic.Entity.ProductionEntities.Eating
{
    public class EatableRegistry 
    {
        public List<Eatable> Eatables { get; } = new List<Eatable>();
        
        public void Register(Eatable eatable)
        {
            Eatables.Add(eatable);
        }
        
        public void Unregister(Eatable eatable)
        {
            Eatables.Remove(eatable);
        }
    }
}