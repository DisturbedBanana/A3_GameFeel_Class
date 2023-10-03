using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using IIMEngine.Entities;
using UnityEngine;

//TODO: Rename to EntityTarget
namespace IIMEngine.Entities.Target
{
    [Serializable]
    public class EntityTarget
    {
        [SerializeField] private EntityTargetType _targetType = EntityTargetType.GameObjectReference;
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private string _entityID;
        [SerializeField] private string _entityGroup;
        [SerializeField] private EntityTargetGroupScope _entityGroupScope = EntityTargetGroupScope.AllTargets;

        public T FindFirstResult<T>() where T : class
        {
            T[] results = FindResults<T>();
            if (results.Length == 0) return null;
            return results[0];
        }

        public T[] FindResults<T>() where T : class
        {
            List<T> targets = new List<T>();

            switch (_targetType) {
                case EntityTargetType.GameObjectReference:
                {
                    if (_gameObject != null) {
                        T result = _gameObject.GetComponent<T>();
                        if (result != null) {
                            targets.Add(result);
                        }
                    }

                    break;
                }

                case EntityTargetType.EntityID:
                {
                    Entity entity = EntitiesGlobal.GetEntityByID(_entityID);
                    if (entity != null) {
                        T result = entity.GetComponent<T>();
                        if (result != null) {
                            targets.Add(result);
                        }
                    }

                    break;
                }

                case EntityTargetType.EntityGroup:
                {
                    ReadOnlyCollection<Entity> entities = EntitiesGlobal.GetEntitiesByGroup(_entityGroup);
                    if (_entityGroupScope == EntityTargetGroupScope.FirstTarget && entities.Count > 0) {
                        Entity entity = entities[0];
                        T result = entity.GetComponent<T>();
                        if (result != null) {
                            targets.Add(result);
                        }
                    } else {
                        foreach (Entity entity in entities) {
                            T result = entity.GetComponent<T>();
                            if (result != null) {
                                targets.Add(result);
                            }
                        }
                    }

                    break;
                }
            }

            return targets.ToArray();
        }
    }
}