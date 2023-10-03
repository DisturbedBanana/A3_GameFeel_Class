using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IIMEngine.Entities
{
    public class EntitiesGlobal
    {
        private static List<Entity> _allEntities = new List<Entity>();
        
        public static ReadOnlyCollection<Entity> AllEntities => _allEntities.AsReadOnly();

        private static Dictionary<string, Entity> _entitiesByID = new Dictionary<string, Entity>();
        private static Dictionary<string, List<Entity>> _entitiesByGroup = new Dictionary<string, List<Entity>>();

        public static void RegisterEntity(Entity entity)
        {
            _allEntities.Add(entity);
            if (entity.HasUniqueID) {
                RegisterEntityID(entity);
            }
            if (entity.HasGroups) {
                RegisterEntityGroups(entity);
            }
        }

        public static void UnRegisterEntity(Entity entity)
        {
            _allEntities.Remove(entity);
            if (entity.HasUniqueID) {
                UnRegisterEntityID(entity);
            }
            if (entity.HasGroups) {
                UnRegisterEntityGroups(entity);
            }
        }

        public static void RegisterEntityID(Entity entity)
        {
            _entitiesByID[entity.EntityID] = entity;
        }

        public static void UnRegisterEntityID(Entity entity)
        {
            _entitiesByID.Remove(entity.EntityID);
        }
        
        public static Entity GetEntityByID(string entityID)
        {
            _entitiesByID.TryGetValue(entityID, out Entity entity);
            return entity;
        }

        public static void RegisterEntityGroups(Entity entity)
        {
            foreach (string group in entity.Groups) {
                _entitiesByGroup.TryGetValue(group, out List<Entity> entities);
                if (entities == null) {
                    entities = new List<Entity>();
                    _entitiesByGroup[group] = entities;
                }
                entities.Add(entity);
            }
        }
        
        public static void UnRegisterEntityGroups(Entity entity)
        {
            foreach (string group in entity.Groups) {
                _entitiesByGroup.TryGetValue(group, out List<Entity> entities);
                if (entities == null) continue;
                entities.Remove(entity);
            }
        }

        public static ReadOnlyCollection<Entity> GetEntitiesByGroup(string group)
        {
            _entitiesByGroup.TryGetValue(group, out List<Entity> entities);
            if (entities == null) return new List<Entity>().AsReadOnly();
            return entities.AsReadOnly();
        }
    }
}