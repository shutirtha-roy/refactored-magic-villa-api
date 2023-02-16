﻿using Autofac;

namespace MagicVilla_Web.Model
{
    public class BaseModel
    {
        protected ILifetimeScope _scope;

        public BaseModel()
        {
        }

        public virtual void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
        }
    }
}