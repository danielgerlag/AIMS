﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace AIMS.Services.IoC
{
    public class Container
    {
        public static IContainer IOCContainer { get; set; }

        public static T Resolve<T>()
        {
            return IOCContainer.Resolve<T>();
        }

        public static object Resolve(Type t)
        {
            return IOCContainer.Resolve(t);
        }

    }
}
