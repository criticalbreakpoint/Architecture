using FluentNHibernate.Mapping;

namespace Architecture.Tests.Core.PersistenceSupport.TestObjects
{
    public class EntityObjectMap : ClassMap<EntityObject>
    {
        public EntityObjectMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
        }
    }
}