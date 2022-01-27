using Bb.ComponentModel.Attributes;
using Bb.WebHost.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Bb.WebHost.Startings
{

    public class TypesToInject : IEnumerable<TypeToInject>
    {

        public TypesToInject()
        {
            this._items = new List<TypeToInject>();
            this._types = new HashSet<Type>();

        }

        public bool Add(IocScopeEnum lifeCycle, Type exposedType, Type implementationType)
        {

            if (exposedType == null)
                throw new InvalidIocConfigurationException($"argument {nameof(exposedType)} can't be null");

            if (implementationType == null)
                throw new InvalidIocConfigurationException($"argument {nameof(implementationType)} can't be null");

            if (this._types.Add(exposedType))
            {
                var instance = new TypeToInject()
                {
                    LifeCycle = lifeCycle,
                    Type = exposedType,
                    ImplementationType = implementationType,
                };

                _items.Add(instance);

                return true;

            }
            
            return false;


        }

        public bool Add(IocScopeEnum lifeCycle, Type exposedType, Func<IServiceProvider, object> func)
        {

            if (exposedType == null)
                throw new InvalidIocConfigurationException($"argument {nameof(exposedType)} can't be null");

            if (func == null)
                throw new InvalidIocConfigurationException($"argument {nameof(func)} can't be null");

            if (this._types.Add(exposedType))
            {

                var instance = new TypeToInject()
                {
                    LifeCycle = lifeCycle,
                    Type = exposedType,
                    Function = func,
                };

                _items.Add(instance);

                return true;

            }

            return false;


        }

        public bool Add(IocScopeEnum lifeCycle, Type exposedType, object instance)
        {

            if (exposedType == null)
                throw new InvalidIocConfigurationException($"argument {nameof(exposedType)} can't be null");

            if (instance == null)
                throw new InvalidIocConfigurationException($"argument {nameof(instance)} can't be null");

            if (this._types.Add(exposedType))
            {

                var _instance = new TypeToInject()
                {
                    LifeCycle = lifeCycle,
                    Type = exposedType,
                    Instance = instance,
                };

                _items.Add(_instance);

                return true;

            }

            return false;


        }

        public IEnumerator<TypeToInject> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }


        private readonly List<TypeToInject> _items;
        private readonly HashSet<Type> _types;

    }


}
